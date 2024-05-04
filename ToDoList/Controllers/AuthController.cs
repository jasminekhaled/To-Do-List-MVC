using Microsoft.AspNetCore.Mvc;
using ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto;
using ToDoList_DomainModel.ViewModels;
using ToDoList_DomainModel.ViewModels.Auth;
using ToDoList_Services.IServices;

namespace ToDoList.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        public async Task<IActionResult> SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
           
            try
            {
                await _authServices.SignUp(model.SignUpDto);
                 return RedirectToAction("VerifyEmail", new { email = model.SignUpDto.Email });

            }
            catch (Exception ex)
            {
                var viewModel = new SignUpViewModel {
                    ErrorMessage = ex.Message,
                    SignUpDto = model.SignUpDto
                };

                return View(viewModel);
            }
        }

        public async Task<IActionResult> VerifyEmail(string email)
        {
            var viewModel = new VerifyEmailViewModel { Email = email }; // Pass email to the view
            return View(viewModel);
        }

        [HttpPost] 
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            try
            {
                var token = await _authServices.VerifyEmail(model.Email, model.VerificationCode);
                Response.Cookies.Append("token", token);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var viewModel = new VerifyEmailViewModel
                {
                    ErrorMessage = ex.Message,
                    Email = model.Email
                };

                return View(viewModel);
            }

        }
       
        public async Task<IActionResult> LogIn()
        {
            var viewModel = new LogInViewModel(); 
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            try
            {
                var result = await _authServices.LogIn(model.LogInDto);
                Response.Cookies.Append("token", result.Token);
                return RedirectToAction("Index", "Missions");
            }
            catch (Exception ex)
            {
                var viewModel = new LogInViewModel
                {
                    ErrorMessage = ex.Message,
                    LogInDto = model.LogInDto
                };

                return View(viewModel);
            }

        }

        public async Task<IActionResult> ForgetPassword()
        {
            var viewModel = new ForgetPasswordViewModel(); // Pass email to the view
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            try
            {
                await _authServices.ForgetPassword(model.ForgetPasswordDto);
                return RedirectToAction("ChangeForgettedPassword", new { userName = model.ForgetPasswordDto.UserName });
            }
            catch (Exception ex)
            {
                var viewModel = new ForgetPasswordViewModel
                {
                    ErrorMessage = ex.Message,
                    ForgetPasswordDto = model.ForgetPasswordDto
                };

                return View(viewModel);
            }
        }

        public async Task<IActionResult> ChangeForgettedPassword(string userName)
        {
            var viewModel = new ChangePasswordViewModel { UserName = userName }; // Pass email to the view
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeForgettedPassword(ChangePasswordViewModel model)
        {
            try
            {
                await _authServices.ChangeForgettedPassword(model.UserName, model.VerifyForgetPassword);
                model.SuccessMessage = "Password changed successfully. You can log in now.";
                return View(model);
            }
            catch (Exception ex)
            {
                var viewModel = new ChangePasswordViewModel
                {
                    ErrorMessage = ex.Message,
                    VerifyForgetPassword = model.VerifyForgetPassword,
                    UserName = model.UserName
                };

                return View(viewModel);
            }
        }

       
    }
}
