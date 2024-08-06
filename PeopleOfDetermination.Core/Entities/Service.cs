//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////


using Core.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
    public class Service :BaseEntity
    {
        [Required(ErrorMessage ="الحقل مطلوب")]
        [DisplayName("اسم الخدمة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [DisplayName("وصف الخدمة")]
        public string Description { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [DisplayName("رابط الخدمة")]
        public string Link { get; set; }

        [DisplayName("حالة الخدمة")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
