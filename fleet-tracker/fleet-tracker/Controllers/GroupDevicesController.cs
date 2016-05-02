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
    public class GroupDevicesController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: GroupDevices
        public ActionResult Index()
        {
            var groupDevices = db.GroupDevices.Include(g => g.Device).Include(g => g.Group);
            return View(groupDevices.ToList());
        }

        // GET: GroupDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupDevice groupDevice = db.GroupDevices.Find(id);
            if (groupDevice == null)
            {
                return HttpNotFound();
            }
            return View(groupDevice);
        }

        // GET: GroupDevices/Create
        public ActionResult Create()
        {
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID");
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name");
            return View();
        }

        // POST: GroupDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DeviceID,GroupID")] GroupDevice groupDevice)
        {
            if (ModelState.IsValid)
            {
                db.GroupDevices.Add(groupDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", groupDevice.DeviceID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", groupDevice.GroupID);
            return View(groupDevice);
        }

        // GET: GroupDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupDevice groupDevice = db.GroupDevices.Find(id);
            if (groupDevice == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", groupDevice.DeviceID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", groupDevice.GroupID);
            return View(groupDevice);
        }

        // POST: GroupDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeviceID,GroupID")] GroupDevice groupDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", groupDevice.DeviceID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", groupDevice.GroupID);
            return View(groupDevice);
        }

        // GET: GroupDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupDevice groupDevice = db.GroupDevices.Find(id);
            if (groupDevice == null)
            {
                return HttpNotFound();
            }
            return View(groupDevice);
        }

        // POST: GroupDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupDevice groupDevice = db.GroupDevices.Find(id);
            db.GroupDevices.Remove(groupDevice);
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
