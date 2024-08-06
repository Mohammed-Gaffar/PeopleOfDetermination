//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////

using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleOfDetermination.UI.Dto;
using PeopleOfDetermination.UI.Extentions;
using PeopleOfDetermination.UI.Models;
using System.Diagnostics;

namespace PeopleOfDetermination.UI.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    private readonly IServices _services;
    private readonly IUser _user;

    public HomeController(ILogger<HomeController> logger, IServices services, IUser user)
    {
        _logger = logger;
        _services = services;
        _user = user;
    }

    public IActionResult Index()
    {

        if (User.IsInRole("Super_Admin") || User.IsInRole("Admin"))
        {
            return RedirectToAction("Admin");
        }
        else
        {
            return RedirectToAction("services");
        }
    }

    public IActionResult services()
    {

        IEnumerable<Service> services;
        if (User.IsInRole("Admin") || User.IsInRole("Super_Admin"))
        {
            services = _services.GetAll();
        }
        else
        {
            services = _services.GetAllUser();
        }

        return View("services", services);
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    public async Task<IActionResult> Admin()
    {
        ServicesDTO info = new()
        {
            ActiveServices = await _services.ActiveServices(),
            DisableServices = await _services.DeactiveServices(),
            TotalUsers = await _user.TotalUsers(),
            totoalServices = await _services.totalServices(),
        };

        return View(info);
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Service service)
    {
        User createduser;
        if (User.Identity.Name != null)
        {
            createduser = await _user.GetByName(User.Identity.Name);
        }
        else
        {
            return RedirectToAction(nameof(services));
        }

        if (ModelState.IsValid)
        {
            service.Create_At = DateTime.Now;
            service.Created_by = createduser.ID;
            BaseResponse res = await _services.Create(service);
            if (res.IsSuccess == true)
            {
                BasicNotification("تم اضافة البيانات بنجاح", NotificationType.Success);
                return RedirectToAction(nameof(services));
            }
            else
            {
                BasicNotification(res.Message ?? "", NotificationType.Error);
                return RedirectToAction(nameof(Create), service);
            }
        }
        else
        {
            return View(service);
        }

    }

    [Authorize(Roles = "Super_Admin,Admin")]
    public IActionResult Active(Guid Id)
    {
        _services.Activate(Id);

        BasicNotification("تم تفعيل الخدمة ", NotificationType.Success);
        return RedirectToAction(nameof(services));
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    public IActionResult DeActive(Guid Id)
    {
        _services.DeActivate(Id);
        BasicNotification("تم الغاء تفعيل الخدمة ", NotificationType.Warning);
        return RedirectToAction(nameof(services));
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    public async Task<IActionResult> Edit(Guid Id)
    {
        Service service = await _services.GetById(Id);
        return View(service);
    }

    [Authorize(Roles = "Super_Admin,Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(Service service)
    {
        User createduser;
        if (User.Identity.Name != null)
        {
            createduser = await _user.GetByName(User.Identity.Name);
        }
        else
        {
            return RedirectToAction(nameof(services));
        }

        if (ModelState.IsValid)
        {
            service.Update_At = DateTime.Now;
            service.Updated_by = createduser.ID;
            BaseResponse res = await _services.Update(service);
            if (res.IsSuccess == true)
            {
                BasicNotification("تم اضافة البيانات بنجاح", NotificationType.Success);
                return RedirectToAction(nameof(services));
            }
            else
            {
                BasicNotification(res.Message ?? "", NotificationType.Error);
                return RedirectToAction(nameof(Create), service);
            }
        }
        return View(service);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
