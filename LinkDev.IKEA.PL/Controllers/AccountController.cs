using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}



        [HttpGet] // Get : /Account/SignUp
        public IActionResult SignUp()
        { 
            return View();  
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // Check That not duplicate user 


            var user =await _userManager.FindByNameAsync(model.UserName);

            if (user is { })
            { 
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This User Already Exist !! ");// بعت الكيي كا انا عايزه يبقى في بروب معينه 
                return View(model);

            }
                user = new ApplicationUser()
                {
                    FName = model.FirstName,
                    LName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(SignIn));

                // If not Succeed هعدي ع الايرورز الممكنه ممكن ايميل ممكن بلا بلا وابعت الكي كا سترنج فاضي 
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);





		}


		#region Sign In 

		public IActionResult SignIn()
        {
            return View();
        }


        #endregion

    }
}
