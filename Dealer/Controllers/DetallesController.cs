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
    public class DetallesController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Detalles
        [OverrideAuthorization]
        public ActionResult Index()
        {
            var detalles = db.Detalles.Include(d => d.Automovil).Include(d => d.Automovil1).Include(d => d.Sucursal);
            return View(detalles.ToList());
        }

        // GET: Detalles/Details/5
        [OverrideAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre");
            return View();
        }

        // POST: Detalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Condicion,ID_Auto,Condicion,Cantidad,ID_Sucursal,Detalles")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Detalles.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Sucursal);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre", detalle.ID_Sucursal);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Sucursal);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre", detalle.ID_Sucursal);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Condicion,ID_Auto,Condicion,Cantidad,ID_Sucursal,Detalles")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Sucursal);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre", detalle.ID_Sucursal);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalles.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalles.Find(id);
            db.Detalles.Remove(detalle);
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
