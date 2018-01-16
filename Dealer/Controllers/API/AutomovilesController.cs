using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Dealer;

namespace Dealer.Controllers.API
{
    public class AutomovilesController : ApiController
    {
        private DealersEntities db = new DealersEntities();

        // GET: api/Automoviles
        public IQueryable<Automovil> GetAutomovils()
        {
            return db.Automovils;
        }

        // GET: api/Automoviles/5
        [ResponseType(typeof(Automovil))]
        public IHttpActionResult GetAutomovil(int id)
        {
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return NotFound();
            }

            return Ok(automovil);
        }

        // PUT: api/Automoviles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutomovil(int id, Automovil automovil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != automovil.ID_Auto)
            {
                return BadRequest();
            }

            db.Entry(automovil).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutomovilExists(id))
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

        // POST: api/Automoviles
        [ResponseType(typeof(Automovil))]
        public IHttpActionResult PostAutomovil(Automovil automovil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Automovils.Add(automovil);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = automovil.ID_Auto }, automovil);
        }

        // DELETE: api/Automoviles/5
        [ResponseType(typeof(Automovil))]
        public IHttpActionResult DeleteAutomovil(int id)
        {
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return NotFound();
            }

            db.Automovils.Remove(automovil);
            db.SaveChanges();

            return Ok(automovil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutomovilExists(int id)
        {
            return db.Automovils.Count(e => e.ID_Auto == id) > 0;
        }
    }
}