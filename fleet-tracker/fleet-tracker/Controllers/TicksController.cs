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
    public class TicksController : Controller
    {
        private FleetModel db = new FleetModel();

        // GET: Ticks
        public async Task<ActionResult> Index()
        {
            var ticks = db.Ticks.Include(t => t.Device).Include(t => t.Invoice);
            return View(await ticks.ToListAsync());
        }

        // GET: Ticks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick tick = await db.Ticks.FindAsync(id);
            if (tick == null)
            {
                return HttpNotFound();
            }
            return View(tick);
        }

        // GET: Ticks/Create
        public ActionResult Create()
        {
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID");
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Token");
            return View();
        }
        
        // GET: Ticks/API_Create
        public async Task<ActionResult> API_Create(int deviceID, string token, Decimal lat, Decimal lon, string message)
        {
            Tick tick = new Tick();
            tick.DeviceID = deviceID;
            tick.InvoiceID = db.Invoices.Where(x => x.Token == token).First().ID;
            tick.Lat = lat;
            tick.Long = lon;
            tick.Message = message;
            tick.CreatedAt = DateTime.Now;

            db.Ticks.Add(tick);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        // POST: Ticks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DeviceID,InvoiceID,Lat,Long,Message,CreatedAt")] Tick tick)
        {
            if (ModelState.IsValid)
            {
                db.Ticks.Add(tick);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", tick.DeviceID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Token", tick.InvoiceID);
            return View(tick);
        }

        // GET: Ticks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick tick = await db.Ticks.FindAsync(id);
            if (tick == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", tick.DeviceID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Token", tick.InvoiceID);
            return View(tick);
        }

        // POST: Ticks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DeviceID,InvoiceID,Lat,Long,Message,CreatedAt")] Tick tick)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tick).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceID = new SelectList(db.Devices, "ID", "ID", tick.DeviceID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Token", tick.InvoiceID);
            return View(tick);
        }

        // GET: Ticks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tick tick = await db.Ticks.FindAsync(id);
            if (tick == null)
            {
                return HttpNotFound();
            }
            return View(tick);
        }

        // POST: Ticks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tick tick = await db.Ticks.FindAsync(id);
            db.Ticks.Remove(tick);
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
