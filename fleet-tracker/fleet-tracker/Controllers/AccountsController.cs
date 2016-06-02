using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fleet_tracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using fleet_tracker.App_Start;

namespace fleet_tracker.Controllers
{
    public class AccountsController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: Accounts
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Index()
        {
            var accounts = db.Users.Include(a => a.Group);
            return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser account = db.Users.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");
            ViewBag.Group = new SelectList(db.Groups, "ID", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Create(string UserName, string Email, string PasswordHash, string Role, int Group)
        {
            FleetModel dbb = HttpContext.GetOwinContext().Get<FleetModel>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            Group g = dbb.Groups.First(a => a.ID == Group);
            var result = userManager.Create(new AppUser
            {
                UserName = UserName,
                Email = Email,
                EmailConfirmed = true,
                Group = g
            }, PasswordHash);

            userManager.AddToRole(userManager.FindByName(UserName).Id, Role);
            if (!result.Succeeded)
            {
                //ViewBag.Group = new SelectList(db.Groups, "ID", "Name", user.Group.ID);
                return View();
            }

            return RedirectToAction("Index");     
        }

        // GET: Accounts/Edit/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppUser account = db.Users.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name", account.Roles.First().RoleId);
            ViewBag.Group = new SelectList(db.Groups, "ID", "Name", account.Group.ID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Edit(string Id, string Email, string PasswordHash, int Group)
        {
            FleetModel dbb = HttpContext.GetOwinContext().Get<FleetModel>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            var user = userManager.FindById(Id);
            if(user == null)
                return View(user);

            user.PasswordHash = userManager.PasswordHasher.HashPassword(PasswordHash);
            user.Email = Email;
            user.Group = dbb.Groups.First(x => x.ID == Group);
            userManager.Update(user);

            return RedirectToAction("Index");

        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser account = db.Users.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult DeleteConfirmed(string id)
        {
            FleetModel dbb = HttpContext.GetOwinContext().Get<FleetModel>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            AppUser account = userManager.FindById(id);
            userManager.Delete(account);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
