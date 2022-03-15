
using AutoMapper;
using Hypotec.Data.Entity;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Hypotec.Web.Models;
using Hypotec.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hypotec.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;
        private readonly EmailManager _emailManager;
        /// <summary>
        /// Use constructor of AccountController
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="mapper"></param>
        /// <param name="appUserService"></param>
        /// <param name="emailManager"></param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IAppUserService appUserService, EmailManager emailManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _emailManager = emailManager;
        }
        /// <summary>
        /// add user AddUser
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddUser()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel userModel)
        {
            try
            {
                if (userModel != null)
                {
                    var mapUserModel = _mapper.Map<AppUserDto>(userModel);
                    var LoggedUser = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
                    mapUserModel.UserName = userModel.Email;
                    mapUserModel.NormalizedEmail = userModel.Email;
                    mapUserModel.NormalizedUserName = userModel.Email;
                    if (mapUserModel.Id == 0)
                    {
                        mapUserModel.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        mapUserModel.ModifiedDate = DateTime.Now;
                    }
                    bool IsSuccess = await _appUserService.SaveUser(mapUserModel).ConfigureAwait(false);
                    if (IsSuccess)
                    {
                        ViewBag.Message = HypotecResource.UserRegisterSuccess;
                        return View(userModel);
                    }
                    else
                    {
                        ViewBag.Message = HypotecResource.UserNotRegister;
                        return View(userModel);
                    }
                }
                else
                {
                    ViewBag.Message = HypotecResource.UserNotRegister;
                    return View(userModel);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// login feature
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// pen admin login page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AdminLogin()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Submitting login form 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                if (loginModel.Email == HypotecResource.EmailId && loginModel.Password == HypotecResource.Password)
                { 
                    ViewBag.Message = HypotecResource.LoginSuccess;
                    return RedirectToAction(nameof(DashboardController.Dashboard), "Dashboard");
                }
                else
                {
                    ViewBag.Message = HypotecResource.InvalidUser;
                    return await Task.Run(() => View(loginModel)).ConfigureAwait(false);
                }

            }
            return View(loginModel);
        }
        /// <summary>
        /// log out page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Lockout()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Logout for a logged in user
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
        /// <summary>
        /// Form for user when he forget his password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// Posting form with sending the mail to user email address
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                if (forgotPasswordModel != null)
                {
                    string mail = forgotPasswordModel.Email;
                    var user = await _userManager.FindByEmailAsync(mail).ConfigureAwait(false);
                    if (user != null)
                    {
                        string token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
                        if (token == null)
                        {
                            ModelState.AddModelError(string.Empty, HypotecResource.TokenFail);
                            return View();
                        }
                        else
                        {
                            var link = "<a href='" + Url.Action("ResetPassword", "Account", new { email = mail, code = token }, "http") + HypotecResource.ResetPasswordlink;
                            _emailManager.SendEmail(mail, link, HypotecResource.ForgetPasswordSubject, HypotecResource.ForgetPasswordMsgBody);
                            ViewBag.Message = HypotecResource.ConfirmationForFgtPwd;
                            return View("Confirmation");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, HypotecResource.EmailNotFound);
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, HypotecResource.EmailNotFound);
                    return View();
                }

            }
            return View();
        }
        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email, string code)
        {
            try
            {
                var model = new ResetPasswordModel { ReturnToken = code, Email = email };
                return await Task.Run(() => View(model)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Submitting form for resetting the password 
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resetPasswordModel != null)
                    {
                        var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email).ConfigureAwait(false);
                        if (user == null)
                        {
                            ModelState.AddModelError(string.Empty, HypotecResource.ResetPasswordEmail);
                            return View(resetPasswordModel);
                        }
                        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.ReturnToken, resetPasswordModel.Password).ConfigureAwait(false);
                        if (resetPassResult.Succeeded)
                        {
                            ViewBag.Success = HypotecResource.ConfirmationForResetPwd;
                            return View();
                        }
                        else
                        {
                            foreach (var error in resetPassResult.Errors)
                            {
                                ModelState.TryAddModelError(error.Code, error.Description);
                            }
                            return View();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(resetPasswordModel);
        }
        /// <summary>
        /// form for changing the password for logged-in user
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ChangePasswordModel changePassword = new ChangePasswordModel();
            return PartialView("_ChangePassword", changePassword);
        }
        /// <summary>
        /// Submitting the new password by user after checking the current password is right or wrong
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (changePasswordModel != null)
                    {
                        var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
                        if (user == null)
                        {
                            return RedirectToAction("Login");
                        }
                        var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword).ConfigureAwait(false);
                        if (result.Succeeded)
                        {
                            await _signInManager.RefreshSignInAsync(user).ConfigureAwait(false);
                            ViewBag.Message = HypotecResource.ConfirmationForChangePwd;
                            return Json(result);

                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return PartialView("_ChangePassword");
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return View();
        }
        /// <summary>
        /// method for checking password for passoword changing time by ajax call
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CheckPassword(string currentPassword)
        {
            try
            {
                if (currentPassword != null)
                {
                    var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
                    if (user == null)
                    {
                        return RedirectToAction("Login");
                    }
                    var result = await _userManager.CheckPasswordAsync(user, currentPassword).ConfigureAwait(false);

                    return Content(result.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        /// <summary>
        /// method for Confirmation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }

        /// <summary>
        /// method for Dashboard
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ResponseCache(CacheProfileName = "Never")]
        public IActionResult Dashboard()
        {
            return View();
        }
        /// <summary>
        /// IsEmail In Use
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json("Email already exist");
            }
        }
        /// <summary>
        /// Validate password 
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> ValidatePassword(string passwordHash)
        {
            var input = passwordHash;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                return await Task.Run(() => Json("Password should contain at least one lower case letter.")).ConfigureAwait(false);

            }
            else if (!hasUpperChar.IsMatch(input))
            {
                return await Task.Run(() => Json("Password should contain at least one upper case letter.")).ConfigureAwait(false);
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                return await Task.Run(() => Json("Password should not be lesser than 8 or greater than 20 characters.")).ConfigureAwait(false);
            }

            else if (!hasNumber.IsMatch(input))
            {
                return await Task.Run(() => Json("Password should contain at least one numeric value.")).ConfigureAwait(false);
            }

            else if (!hasSymbols.IsMatch(input))
            {
                return await Task.Run(() => Json("Password should contain at least one special case character.")).ConfigureAwait(false);
            }
            else
            {
                return await Task.Run(() => Json(true)).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// method for GetStarted
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetStarted()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// method for GetStarted to start flow of loan process
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Started()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// method for start purchase of loan process
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> StartPurchase()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
        /// <summary>
        /// method for start Refinance of loan process
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> StartRefinance()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }
    }
}
