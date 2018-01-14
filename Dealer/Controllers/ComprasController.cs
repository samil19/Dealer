using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dealer;
using Microsoft.AspNet.Identity;

namespace Dealer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ComprasController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.AspNetUser).Include(c => c.Automovil).Include(c => c.Empleado);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        [OverrideAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ID_User = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "ID_Empleado", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Compra,ID_User,ID_Empleado,ID_Auto,Cantidad,Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User = new SelectList(db.AspNetUsers, "Id", "Email", compra.ID_User);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", compra.ID_Auto);
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "ID_Empleado", "Nombre", compra.ID_Empleado);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User = new SelectList(db.AspNetUsers, "Id", "Email", compra.ID_User);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", compra.ID_Auto);
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "ID_Empleado", "Nombre", compra.ID_Empleado);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Compra,ID_User,ID_Empleado,ID_Auto,Cantidad,Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_User = new SelectList(db.AspNetUsers, "Id", "Email", compra.ID_User);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", compra.ID_Auto);
            ViewBag.ID_Empleado = new SelectList(db.Empleados, "ID_Empleado", "Nombre", compra.ID_Empleado);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
        [OverrideAuthorization]
        [Authorize]
        public ActionResult ListbyUser()
        {
            var user = User.Identity.GetUserId();
            var compras = db.Compras.Include(c => c.AspNetUser).Include(c => c.Automovil).Include(c => c.Empleado);
            using (var context = new DealersEntities())
            {

                var model = from s in context.Compras
                    select s;

                if (!String.IsNullOrEmpty(user))
                {
                    compras = compras.Where(s => s.AspNetUser.Id.Contains(user));

                }
            }

            return View(compras.ToList());
        }
    }
}
