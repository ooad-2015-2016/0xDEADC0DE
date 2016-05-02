using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fleet_tracker.Models;

namespace fleet_tracker.Controllers
{
    public class InvoicesController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: Invoices
        public async Task<ActionResult> Index()
        {
            var invoices = db.Invoices.Include(i => i.Device).Include(i => i.Driver).Include(i => i.Group).Include(i => i.Route).Include(i => i.Vehicle);
            return View(await invoices.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID");
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name");
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name");
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Name");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "ID", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Token,RouteID,VehicleID,DeviceID,DriverID,GroupID,Finished,CreatedAt,UpdatedAt,StartedAt,FinishedAt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", invoice.DeviceID);
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name", invoice.DriverID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", invoice.GroupID);
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Name", invoice.RouteID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "ID", "Name", invoice.VehicleID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", invoice.DeviceID);
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name", invoice.DriverID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", invoice.GroupID);
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Name", invoice.RouteID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "ID", "Name", invoice.VehicleID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Token,RouteID,VehicleID,DeviceID,DriverID,GroupID,Finished,CreatedAt,UpdatedAt,StartedAt,FinishedAt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", invoice.DeviceID);
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name", invoice.DriverID);
            ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name", invoice.GroupID);
            ViewBag.RouteID = new SelectList(db.Routes, "ID", "Name", invoice.RouteID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "ID", "Name", invoice.VehicleID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Invoice invoice = await db.Invoices.FindAsync(id);
            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();
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
