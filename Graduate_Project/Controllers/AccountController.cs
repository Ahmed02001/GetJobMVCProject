using Graduate_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Graduate_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {

            if (ModelState.IsValid)
            {
                IdentityUser User = new IdentityUser();

                User.UserName = registerUser.UserName;
                User.Email = registerUser.Email;
                User.PasswordHash = registerUser.Password;
                User.LockoutEnd = DateTimeOffset.UtcNow;    

                IdentityResult result = await userManager.CreateAsync(User, registerUser.Password);

                if (result.Succeeded == true)
                {
                    //create cookie
                    await signInManager.SignInAsync(User, isPersistent: false);

                    return RedirectToAction("New", "Student");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }

                return View(registerUser);
            }
            return View("");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddAdmin(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser User = new IdentityUser();

                User.UserName = model.UserName;
                User.Email = model.Email;
                User.PasswordHash = model.Password;
                User.LockoutEnd = DateTimeOffset.UtcNow;

                
                var existingUser = await userManager.FindByNameAsync(model.UserName);
                if (existingUser != null)
                {
                    // Add an error message if the username is already taken
                    ModelState.AddModelError("Username", "اسم المستخدم موجود مسبقا");
                    return View(model);
                }

                IdentityResult result = await userManager.CreateAsync(User, model.Password);

                if (result.Succeeded)
                {
                    // If the checkbox was checked, assign the Admin role
                    if (model.IsAdmin)
                    {
                        await userManager.AddToRoleAsync(User, "Admin");
                        
                    }
                    else
                    {
                        
                    }
                    return RedirectToAction("UserReport", "Admin");
                }

                // Handle errors in the result
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("UserReport", "Admin");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel UserloginView)
        {
            if (ModelState.IsValid)
            {
                IdentityUser UserModel = await userManager.FindByEmailAsync(UserloginView.Email);

                if(UserModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(UserModel, UserloginView.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(UserModel, UserloginView.RememberMe);
                        if (!User.IsInRole("Admin"))
                        { 
                            return RedirectToAction("New", "Student");
                            
                        }
                        else
                        {
                           return RedirectToAction("index", "Admin");
                        }
                    }
                }

                ModelState.AddModelError("", "البريد الالكترونى او كلمه المرور خطأ");

            }

            return View(UserloginView);

        }


        public IActionResult Logout()
        {
           signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
