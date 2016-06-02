using fleet_tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using fleet_tracker.App_Start;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace fleet_tracker.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Login(string email, string password, string ReturnUrl)
        {
            FleetModel dbb = HttpContext.GetOwinContext().Get<FleetModel>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            if(dbb.Users.First(x => x.Email == email) == null)
                return View();

            // get username
            string username = dbb.Users.First(x => x.Email == email).UserName;
            if (username == null)
                return View();

            AppUser user = userManager.Find(username, password);
            if (user != null)
            {
                var ident = userManager.CreateIdentity(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                
                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                return Redirect(ReturnUrl ?? Url.Action("Index", "Home"));
            }

            return View();
        }
    }
}