using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Constants;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Exceptions;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Managers.Facade;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Providers.Facade;
using Newtonsoft.Json;
using System.Linq;

namespace MOROPO.Framework.Guama.Core.Security.Common.Web.Frontend.Controllers
{
    [Authorize]
    [Area("security")]
    public class AccountController : Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IIdentityProviderFacade identityProvider;
        private readonly IUserProfileManagerFacade userProfileManager;

        public AccountController(IStringLocalizer<AccountController> localizer, ILogger<AccountController> logger, IIdentityProviderFacade identityProvider, IUserProfileManagerFacade userProfileManager)
            : base(localizer, logger)
        {
            this.identityProvider = identityProvider;
            this.userProfileManager = userProfileManager;
        }

        public IActionResult Index()
        {
            var userDetails = userProfileManager.GetUserProfile(User.Identity.Name);

            return View(userDetails);
        }

        [AllowAnonymous]
        public async System.Threading.Tasks.Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Login(Presentation.Web.Security.Models.View.LoginViewModel model, string returnUrl = null)
        {
            if (Url.Action("logoff", "account", new { Area = "security" }).Equals(returnUrl))
            {
                returnUrl = null;
            }

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = "InvalidAuthenticationDataMessage"
            };

            ViewData["ReturnUrl"] = returnUrl;

            if (!HttpContext.Session.Keys.Contains(HttpSessionMapKeys.CaptchaCodeKey))
            {
                ModelState.ClearValidationState("CaptchaCode");
                ModelState.MarkFieldValid("CaptchaCode");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (HttpContext.Session.Keys.Contains(HttpSessionMapKeys.CaptchaCodeKey))
                    {
                        var captchaCode = HttpContext.Session.GetString(HttpSessionMapKeys.CaptchaCodeKey);

                        if (captchaCode != model.CaptchaCode)
                        {
                            ModelState.AddModelError("CaptchaCode", "InvalidVerificationCodeMessage");

                            notification.Message = "InvalidMessage";

                            throw new ForceAuthenticationDetectException();
                        }
                    }

                    var userIdentity = identityProvider.ProvideIdentity(model.UserName, model.Password, CookieAuthenticationDefaults.AuthenticationScheme);

                    if (userIdentity != null)
                    {
                        HttpContext.Session.Remove(HttpSessionMapKeys.CaptchaCodeKey);

                        if (!userIdentity.HasClaim(match => match.Type.Equals(ClaimTypes.AccountDisabled) && match.Value.Equals(true.ToString())))
                        {
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(userIdentity), new AuthenticationProperties
                            {
                                ExpiresUtc = System.DateTime.UtcNow.AddMinutes(30),
                                IsPersistent = model.RememberMe
                            });

                            return RedirectToLocal(returnUrl);
                        }

                        notification.Message = "UserDisabledMessage";
                    }
                }
                catch (ForceAuthenticationDetectException)
                {
                    var captchaCode = Presentation.Web.Security.Helpers.CaptchaHelper.GenerateCode(identityProvider.GetCaptchaCodeLength());

                    HttpContext.Session.SetString(HttpSessionMapKeys.CaptchaCodeKey, captchaCode);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            else
            {
                if (HttpContext.Session.Keys.Contains(HttpSessionMapKeys.CaptchaCodeKey))
                {
                    var captchaCode = HttpContext.Session.GetString(HttpSessionMapKeys.CaptchaCodeKey);

                    if (ModelState["CaptchaCode"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid || captchaCode != model.CaptchaCode)
                    {
                        notification.Message = "InvalidMessage";
                    }

                    var newCaptchaCode = Presentation.Web.Security.Helpers.CaptchaHelper.GenerateCode(identityProvider.GetCaptchaCodeLength());

                    HttpContext.Session.SetString(HttpSessionMapKeys.CaptchaCodeKey, newCaptchaCode);
                }
            }

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            model.CaptchaCode = string.Empty;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        public IActionResult Edit()
        {
            var userDetails = userProfileManager.GetUserProfile(User.Identity.Name);

            return View(userDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Presentation.Web.Security.Models.View.Contrat.IUserProfileViewModel model)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            userProfileManager.PreValidateUserProfile(User.Identity.Name, model, this);

            if (ModelState.IsValid)
            {
                userProfileManager.UpdateUserProfile(User.Identity.Name, model);

                notification.Type = NotificationType.Success;
                notification.Message = "UserProfileEditSuccessMessage";

                TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                return RedirectToAction("edit");
            }

            return View(model);
        }

        public IActionResult Password()
        {
            if (!(identityProvider is IIdentityManagerFacade))
            {
                // TODO Incomplete implementation!!
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Password(Presentation.Web.Security.Models.View.ChangePasswordViewModel model)
        {
            var notification = new NotificationViewModel
            {
                Type = NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            if (identityProvider is IIdentityManagerFacade)
            {
                if (ModelState.IsValid)
                {
                    var identityManager = identityProvider as IIdentityManagerFacade;

                    if (!identityManager.TryModifyPassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                    {
                        ModelState.AddModelError("OldPassword", "InvalidCurrentPasswordMessage");
                    }
                    else
                    {
                        notification.Type = NotificationType.Success;
                        notification.Message = "ChangePasswordSuccessMessage";

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                        return RedirectToAction("login");
                    }
                }
            }
            else
            {
                notification.Message = "UnavailableOperationMessage";

                if (User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountManagementUrl)))
                {
                    var url = User.Claims.First(claim => claim.Type.Equals(ClaimTypes.AccountManagementUrl)).Value;

                    notification.Message += " " + string.Format(localizer.GetString("GoExternalAccountManageAppMessageTemplate"), url);
                }
            }

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Captcha(int width, int height)
        {
            if (!HttpContext.Session.Keys.Contains(HttpSessionMapKeys.CaptchaCodeKey))
            {
                return NoContent();
            }

            var captchaCode = HttpContext.Session.GetString(HttpSessionMapKeys.CaptchaCodeKey);

            return new FileContentResult(Presentation.Web.Security.Helpers.CaptchaHelper.GenerateImageContent(captchaCode), System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        [NonAction]
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

        private static string GetInvalidMessage()
        {
            return "InvalidDataMessage";
        }
    }
}
