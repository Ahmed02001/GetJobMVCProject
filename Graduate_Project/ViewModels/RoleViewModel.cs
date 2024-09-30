using System.ComponentModel.DataAnnotations;

namespace Graduate_Project.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
