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
using Dealer;

namespace Dealer.Controllers.API
{
    public class SucursalesController : ApiController
    {
        private DealersEntities db = new DealersEntities();

        // GET: api/Sucursales
        public IQueryable<Sucursal> GetSucursals()
        {
            return db.Sucursals;
        }

        // GET: api/Sucursales/5
        [ResponseType(typeof(Sucursal))]
        public async Task<IHttpActionResult> GetSucursal(int id)
        {
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return Ok(sucursal);
        }

        // PUT: api/Sucursales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSucursal(int id, Sucursal sucursal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sucursal.ID_Sucursal)
            {
                return BadRequest();
            }

            db.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
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

        // POST: api/Sucursales
        [ResponseType(typeof(Sucursal))]
        public async Task<IHttpActionResult> PostSucursal(Sucursal sucursal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sucursals.Add(sucursal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sucursal.ID_Sucursal }, sucursal);
        }

        // DELETE: api/Sucursales/5
        [ResponseType(typeof(Sucursal))]
        public async Task<IHttpActionResult> DeleteSucursal(int id)
        {
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            db.Sucursals.Remove(sucursal);
            await db.SaveChangesAsync();

            return Ok(sucursal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SucursalExists(int id)
        {
            return db.Sucursals.Count(e => e.ID_Sucursal == id) > 0;
        }
    }
}