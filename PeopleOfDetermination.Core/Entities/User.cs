//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////

using Core.Entities.Base;
using Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "الحقل مطلوب")]
        [DisplayName("اسم الستخدم")]
        public string UserName { get; set; }
        [DisplayName("صلاحية المستخدم")]
        [Required(ErrorMessage = "الحقل مطلوب")]
        public Roles Role { get; set; }
    }
}
