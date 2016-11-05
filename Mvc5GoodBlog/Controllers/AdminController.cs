using Microsoft.Web.WebPages.OAuth;
using Mvc5GoodBlog.Filters;
using Mvc5GoodBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc5GoodBlog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && new CustomAdminMembershipProvider().ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The username or password supplied is incorrect.");
            return View(model);
        }

        // POST: /Admin/LogOff
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "The username already exists. Enter a different user name.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for this email address already exists. Enter a different email address.";
                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is not valid. Enter a valid password value.";
                case MembershipCreateStatus.InvalidEmail:
                    return "The email address provided is not valid. Check the value and try again.";
                case MembershipCreateStatus.InvalidAnswer:
                    return "The password recovery answer provided is not valid. Check the value and try again.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "The password recovery questions provided is not valid. Check the value and try again.";
                case MembershipCreateStatus.InvalidUserName:
                    return "The user name supplied is invalid. Check the value and try again.";
                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Check your entry and try again. If the problem persists, contact your system administrator.";
                case MembershipCreateStatus.UserRejected:
                    return "The user creation request was canceled. Check your entry and try again. If the problem persists, contact your system administrator.";
                default:
                    return "An unknown error occurred. Check your entry and try again. If the problem persists, contact your system administrator.";
            }
        }
    }
}