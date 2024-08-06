//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB Guidegrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////


using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ServicesRepository : IServices
    {
        private readonly DbConn _conn;

        public ServicesRepository(DbConn dbConn)
        {
            _conn = dbConn;
        }

        public async Task<BaseResponse> Activate(Guid id)
        {
            try
            {
                var DbService = _conn.Services.Find(id);
                DbService.IsActive = true;
                await _conn.SaveChangesAsync();
                return new BaseResponse { IsSuccess = true};
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false , Message = ex.ToString() };
            }
            
        }

        public async Task<BaseResponse> Create(Service service)
        {
            await _conn.Services.AddAsync(service);
            await _conn.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        public async Task<BaseResponse> DeActivate(Guid id)
        {
            try
            {
                var DbService = _conn.Services.Find(id);
                DbService.IsActive = false;
                await _conn.SaveChangesAsync();
                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false , Message = ex.ToString()};
            }
        }

        public IEnumerable<Service> GetAll()
        {
           return _conn.Services;
        }

        public IEnumerable<Service> GetAllUser()
        {
            return _conn.Services.Where(m => m.IsActive == true);
        }

        public async Task<Service> GetById(Guid id)
        {
            Service service = await _conn.Services.FindAsync(id);
            if (service != null)
                return service;

            return null;

        }

        public async Task<Service> GetByName(string Name)
        {
            Service service = await _conn.Services.FirstOrDefaultAsync(m => m.Name == Name);

            return service;
        }

        public async Task<BaseResponse> Update(Service service)
        {
            var DbService = await _conn.Services.FindAsync(service.ID);
            if (DbService != null)
            {
                service.Create_At = DbService.Create_At;
                service.Created_by = DbService.Created_by;

                _conn.Entry(DbService).CurrentValues.SetValues(service);
                await _conn.SaveChangesAsync(true);
                return new BaseResponse { IsSuccess = true, Message = "تم تحديث بيانات الخدمة " };
            }
            else
            {
                return new BaseResponse() { IsSuccess = false, Message = "الرجاء  التحقق من العملية " };
            }
        }

        public async Task<int> totalServices()
        {
            var totalServices =  await _conn.Services.CountAsync();
            return totalServices;
        }
        public async Task<int> ActiveServices()
        {
            var ActiveServices = await _conn.Services.Where(x =>x.IsActive == true).CountAsync();
            return ActiveServices;
        }
        public async Task<int> DeactiveServices()
        {
            var DeactiveServices = await _conn.Services.Where(x => x.IsActive == false).CountAsync();
            return DeactiveServices;
        }
    }
}
