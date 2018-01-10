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
    public class CondicionsController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Condicions
        public ActionResult Index()
        {
            var condicions = db.Condicions.Include(c => c.Automovil);
            return View(condicions.ToList());
        }

        // GET: Condicions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condicion condicion = db.Condicions.Find(id);
            if (condicion == null)
            {
                return HttpNotFound();
            }
            return View(condicion);
        }

        // GET: Condicions/Create
        public ActionResult Create()
        {
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            return View();
        }

        // POST: Condicions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Condicion,Condicion1,Cantidad,ID_Auto")] Condicion condicion)
        {
            if (ModelState.IsValid)
            {
                db.Condicions.Add(condicion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", condicion.ID_Auto);
            return View(condicion);
        }

        // GET: Condicions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condicion condicion = db.Condicions.Find(id);
            if (condicion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", condicion.ID_Auto);
            return View(condicion);
        }

        // POST: Condicions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Condicion,Condicion1,Cantidad,ID_Auto")] Condicion condicion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condicion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", condicion.ID_Auto);
            return View(condicion);
        }

        // GET: Condicions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condicion condicion = db.Condicions.Find(id);
            if (condicion == null)
            {
                return HttpNotFound();
            }
            return View(condicion);
        }

        // POST: Condicions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condicion condicion = db.Condicions.Find(id);
            db.Condicions.Remove(condicion);
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
