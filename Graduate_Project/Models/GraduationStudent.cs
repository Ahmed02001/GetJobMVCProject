using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduate_Project.Models
{
    public class GraduationStudent
    {
        public int GraduationStudentId { get; set; }

        [Required(ErrorMessage = "يجب ادخال الاسم")]
        [Display(Name = "ادخل الأسم")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يجب ادخال البريد الالكترونى")]
        [EmailAddress(ErrorMessage = "يجب ان تكتب البريد الالكترونى بشكل صحيح")]
        [Display(Name = "ادخل البريد الالكترونى")]
        public string Email { get; set; }


        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [Phone(ErrorMessage = "يجب ادخال رقم الههاتف بشكل صحيح")]
        [Display(Name = "ادخل رقم الهاتف")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "اختر الجامعة")]
        public string University { get; set; }

        [Required]
        [Display(Name = "اختر الكلية")]
        public string College { get; set; }

        [Required]
        [Display(Name = "اختر التخصص")]
        public string Specialization { get; set; }

        [Required]
        [Display(Name = "التقديم العام")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "سنه التخرج")]
        public DateOnly GraduationYear { get; set; }

        public string UserId { get; set; } // Foreign key for the user
        public IdentityUser User { get; set; }

        public ICollection<StudentSkill> StudentSkills { get; set; }
    }
}
