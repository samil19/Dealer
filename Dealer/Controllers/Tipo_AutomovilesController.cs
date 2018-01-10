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
    public class Tipo_AutomovilesController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Tipo_Automoviles
        public ActionResult Index()
        {
            return View(db.Tipo_Automovil.ToList());
        }

        // GET: Tipo_Automoviles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Automovil tipo_Automovil = db.Tipo_Automovil.Find(id);
            if (tipo_Automovil == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Automovil);
        }

        // GET: Tipo_Automoviles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Automoviles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tipo,Tipo")] Tipo_Automovil tipo_Automovil)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Automovil.Add(tipo_Automovil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Automovil);
        }

        // GET: Tipo_Automoviles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Automovil tipo_Automovil = db.Tipo_Automovil.Find(id);
            if (tipo_Automovil == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Automovil);
        }

        // POST: Tipo_Automoviles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tipo,Tipo")] Tipo_Automovil tipo_Automovil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Automovil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Automovil);
        }

        // GET: Tipo_Automoviles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Automovil tipo_Automovil = db.Tipo_Automovil.Find(id);
            if (tipo_Automovil == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Automovil);
        }

        // POST: Tipo_Automoviles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Automovil tipo_Automovil = db.Tipo_Automovil.Find(id);
            db.Tipo_Automovil.Remove(tipo_Automovil);
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
