using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using fleet_tracker.Models;

namespace fleet_tracker.Controllers
{
    public class TicksApiController : ApiController
    {
        private FleetModel db = new FleetModel();

        // GET: api/TicksApi
        public IQueryable<Tick> GetTicks()
        {
            return db.Ticks;
        }

        // GET: api/TicksApi/5
        [ResponseType(typeof(Tick))]
        public async Task<IHttpActionResult> GetTick(int id)
        {
            Tick tick = await db.Ticks.FindAsync(id);
            if (tick == null)
            {
                return NotFound();
            }

            return Ok(tick);
        }


        [Route("api/ticks/{device_id}/{last_id}")]
        public IQueryable<Tick> GetActiveTicks(int device_id, int last_id)
        {
            return db.Ticks.Where(x => x.DeviceID == device_id && x.Invoice.Finished == 0 && x.Invoice.DeviceID == x.DeviceID && x.ID > last_id);
        }

        [System.Web.Http.HttpGet]
        [Route("api/ticks/new/{device_id}/{lat}/{lon}")]
        public IHttpActionResult NewActiveTick(int device_id, decimal lat, decimal lon)
        {
            //return db.Ticks.Where(x => x.DeviceID == device_id && x.Invoice.Finished == 0 && x.Invoice.DeviceID == x.DeviceID && x.ID > last_id);

            var activeInvoices = db.Invoices.Where(x => x.Finished == 0 && x.DeviceID == device_id);
            if (activeInvoices.Count() == 1)
            {
                int activeInvoiceID = activeInvoices.First().ID;

                Tick t = new Tick();
                t.DeviceID = device_id;
                t.InvoiceID = activeInvoiceID;
                t.Message = "";
                t.Lat = lat;
                t.Long = lon;
                t.CreatedAt = DateTime.Now;

                db.Ticks.Add(t);
                db.SaveChanges();

                return Json("ok");
            }
            //.First().ID;
            //Tick t = new Tick();


            return Json("no");
        }

        // POST: api/TicksApi
        /*  [ResponseType(typeof(Tick))]
          public async Task<IHttpActionResult> PostTick(Tick tick)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              db.Ticks.Add(tick);
              await db.SaveChangesAsync();

              return CreatedAtRoute("DefaultApi", new { id = tick.ID }, tick);
          }
          */
        // PUT: api/TicksApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTick(int id, Tick tick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tick.ID)
            {
                return BadRequest();
            }

            db.Entry(tick).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TickExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TicksApi
        [ResponseType(typeof(Tick))]
        public async Task<IHttpActionResult> PostTick(Tick tick)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ticks.Add(tick);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tick.ID }, tick);
        }

        // DELETE: api/TicksApi/5
        [ResponseType(typeof(Tick))]
        public async Task<IHttpActionResult> DeleteTick(int id)
        {
            Tick tick = await db.Ticks.FindAsync(id);
            if (tick == null)
            {
                return NotFound();
            }

            db.Ticks.Remove(tick);
            await db.SaveChangesAsync();

            return Ok(tick);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TickExists(int id)
        {
            return db.Ticks.Count(e => e.ID == id) > 0;
        }
    }
}