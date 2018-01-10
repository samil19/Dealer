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
    public class Tipo_TransController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Tipo_Trans
        public ActionResult Index()
        {
            return View(db.Tipo_Trans.ToList());
        }

        // GET: Tipo_Trans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Trans tipo_Trans = db.Tipo_Trans.Find(id);
            if (tipo_Trans == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Trans);
        }

        // GET: Tipo_Trans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Trans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TipoTrans,Tipo_Trans1")] Tipo_Trans tipo_Trans)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Trans.Add(tipo_Trans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Trans);
        }

        // GET: Tipo_Trans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Trans tipo_Trans = db.Tipo_Trans.Find(id);
            if (tipo_Trans == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Trans);
        }

        // POST: Tipo_Trans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TipoTrans,Tipo_Trans1")] Tipo_Trans tipo_Trans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Trans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Trans);
        }

        // GET: Tipo_Trans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Trans tipo_Trans = db.Tipo_Trans.Find(id);
            if (tipo_Trans == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Trans);
        }

        // POST: Tipo_Trans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Trans tipo_Trans = db.Tipo_Trans.Find(id);
            db.Tipo_Trans.Remove(tipo_Trans);
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
