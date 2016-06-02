using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fleet_tracker.Models;

namespace fleet_tracker.Controllers
{
    public class RoutesController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: Routes
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Index()
        {
            var routes = db.Routes.Include(r => r.Group);
            return View(routes.ToList());
        }

        // GET: Routes/Details/5
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // GET: Routes/Create
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Create([Bind(Include = "ID,Name,Origin,OriginLat,OriginLong,Destination,DestinationLat,DestinationLong,GroupID")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", route.GroupID);
            return View(route);
        }

        // GET: Routes/Edit/5
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", route.GroupID);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Edit([Bind(Include = "ID,Name,Origin,OriginLat,OriginLong,Destination,DestinationLat,DestinationLong,GroupID")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", route.GroupID);
            return View(route);
        }

        // GET: Routes/Delete/5
        [Authorize(Roles = "Client Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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
