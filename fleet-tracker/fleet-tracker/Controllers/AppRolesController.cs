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

namespace fleet_tracker.Controllers
{
    public class AppRolesController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: AppRoles
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: AppRoles/Details/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole appRole = db.Roles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // GET: AppRoles/Create
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Create(/*[Bind(Include = "Id,Name")]*/ AppRole appRole)
        {
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();

            if (!roleManager.RoleExists(appRole.Name))
            {
                roleManager.Create(new AppRole(appRole.Name));
                return RedirectToAction("Index");
            }

            return View(appRole);
        }

        // GET: AppRoles/Edit/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole appRole = db.Roles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // POST: AppRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name")] IdentityRole appRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appRole);
        }

        // GET: AppRoles/Delete/5
        [Authorize(Roles = "Global Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole appRole = db.Roles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // POST: AppRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Global Administrator")]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityRole appRole = db.Roles.Find(id);
            db.Roles.Remove(appRole);
            db.SaveChanges();
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
