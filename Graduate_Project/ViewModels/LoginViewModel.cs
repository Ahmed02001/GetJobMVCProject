using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Graduate_Project.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "الرجاء ادخال البريد الالكترونى بشكل صحيح")]
        [EmailAddress(ErrorMessage = "البريد الالكترونى ليس صحيح")]
        [Display(Name ="ادخل البريد الالكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال كلمه المرور بشكل صحيح")]
        [DataType(DataType.Password)]
        [Display(Name = "ادخل كلمة السر")]

        public string Password { get; set; }

        [Display(Name = "تذكرنى")]
        public bool RememberMe { get; set; }

    }
}
