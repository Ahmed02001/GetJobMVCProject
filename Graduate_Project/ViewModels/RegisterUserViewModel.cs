using System.ComponentModel.DataAnnotations;

namespace Graduate_Project.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "الرجاء ادخال اسم المستخدم بشكل صحيح ويجب ألا يقل اسم المستخدم عن ثلاثة احرف")]
        [MinLength(3, ErrorMessage = "اسم المستخدم ليس صحيح")]
        [Display(Name = "ادخل اسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال البريد الالكترونى بشكل صحيح")]
        [EmailAddress(ErrorMessage = "البريد الالكترونى ليس صحيح")]
        [Display(Name = "ادخل البريد الالكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال كلمه المرور بشكل صحيح")]
        [DataType(DataType.Password)]
        [Display(Name = "ادخل كلمة السر")]
        [MinLength(5, ErrorMessage = "كلمه المرور ليست صحيح")]
        public string Password { get; set; }
    }
}
