using Graduate_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Graduate_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager) 
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleModel.RoleName;

                IdentityResult Result =  await roleManager.CreateAsync(role);

                if (Result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                foreach(var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleModel);
        }
    }
}
