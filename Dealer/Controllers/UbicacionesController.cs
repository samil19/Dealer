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
    public class UbicacionesController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Ubicaciones
        public ActionResult Index()
        {
            var ubicacions = db.Ubicacions.Include(u => u.Automovil).Include(u => u.Sucursal);
            return View(ubicacions.ToList());
        }

        // GET: Ubicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // GET: Ubicaciones/Create
        public ActionResult Create()
        {
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Latitud");
            return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Ubicacion,ID_Sucursal,ID_Auto")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Ubicacions.Add(ubicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", ubicacion.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Latitud", ubicacion.ID_Sucursal);
            return View(ubicacion);
        }

        // GET: Ubicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", ubicacion.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Latitud", ubicacion.ID_Sucursal);
            return View(ubicacion);
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Ubicacion,ID_Sucursal,ID_Auto")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", ubicacion.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Latitud", ubicacion.ID_Sucursal);
            return View(ubicacion);
        }

        // GET: Ubicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            db.Ubicacions.Remove(ubicacion);
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
