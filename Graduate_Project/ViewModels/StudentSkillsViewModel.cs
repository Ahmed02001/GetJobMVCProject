using Graduate_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Graduate_Project.ViewModels
{
    public class StudentSkillsViewModel
    {
        public int GraduationStudentId { get; set; }

        [Required(ErrorMessage ="يجب ادخال الاسم")]
        [Display(Name = "الأسم")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يجب ادخال البريد الالكترونى")]
        [EmailAddress(ErrorMessage = "يجب ان تكتب البريد الالكترونى بشكل صحيح")]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }


        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [Phone(ErrorMessage = "يجب ادخال رقم الههاتف بشكل صحيح")]
        [Display(Name = "رقم الهاتف")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = " اختر الجامعة")]
        public string University { get; set; }

        public List<SelectListItem> UniversityOptions { get; set; }

        [Required]
        [Display(Name = "اختر الكلية")]
        public string College { get; set; }
        public List<SelectListItem> CollegeOptions { get; set; }

        [Required]
        [Display(Name = "اختر التخصص")]
        public string Specialization { get; set; }
        public List<SelectListItem> SpecializationOptions { get; set; }

        [Required]
        [Display(Name = "التقدير")]
        public string Degree { get; set; }
        public List<SelectListItem> DegreeOptions { get; set; }

        [Required]
        [Display(Name = "سنه التخرج")]
        public DateOnly GraduationYear { get; set; }
        public List<SelectListItem> GraduationYearOptions { get; set; }


        public int StudentId { get; set; }
        public List<SkillViewModel> SelectedSkills { get; set; } = new List<SkillViewModel>();

    }
}
