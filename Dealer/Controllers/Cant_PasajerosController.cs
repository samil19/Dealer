using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dealer;

namespace Dealer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Cant_PasajerosController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Cant_Pasajeros
        public ActionResult Index()
        {
            return View(db.Cant_Pasajeros.ToList());
        }

        // GET: Cant_Pasajeros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cant_Pasajeros cant_Pasajeros = db.Cant_Pasajeros.Find(id);
            if (cant_Pasajeros == null)
            {
                return HttpNotFound();
            }
            return View(cant_Pasajeros);
        }

        // GET: Cant_Pasajeros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cant_Pasajeros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CanPasajeros,CanPasajeros")] Cant_Pasajeros cant_Pasajeros)
        {
            if (ModelState.IsValid)
            {
                db.Cant_Pasajeros.Add(cant_Pasajeros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cant_Pasajeros);
        }

        // GET: Cant_Pasajeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cant_Pasajeros cant_Pasajeros = db.Cant_Pasajeros.Find(id);
            if (cant_Pasajeros == null)
            {
                return HttpNotFound();
            }
            return View(cant_Pasajeros);
        }

        // POST: Cant_Pasajeros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CanPasajeros,CanPasajeros")] Cant_Pasajeros cant_Pasajeros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cant_Pasajeros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cant_Pasajeros);
        }

        // GET: Cant_Pasajeros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cant_Pasajeros cant_Pasajeros = db.Cant_Pasajeros.Find(id);
            if (cant_Pasajeros == null)
            {
                return HttpNotFound();
            }
            return View(cant_Pasajeros);
        }

        // POST: Cant_Pasajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cant_Pasajeros cant_Pasajeros = db.Cant_Pasajeros.Find(id);
            db.Cant_Pasajeros.Remove(cant_Pasajeros);
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
