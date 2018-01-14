using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Dealer;
using Microsoft.Ajax.Utilities;


namespace Dealer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AutomovilesController : Controller
    {
        private DealersEntities db = new DealersEntities();

        // GET: Automoviles
        [OverrideAuthorization]
        public ActionResult Index()
        {
            var automovils = db.Automovils.Include(a => a.Cant_Pasajeros).Include(a => a.Marca).Include(a => a.Tipo_Automovil).Include(a => a.Tipo_Trans);
            return View(automovils.ToList());
        }

        // GET: Automoviles/Details/5
        [OverrideAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // GET: Automoviles/Create
        public ActionResult Create()
        {
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros");
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1");
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo");
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1");
            return View();
        }

        // POST: Automoviles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Auto,ID_Marca,Modelo,Año_Fabricacion,ID_Tipo,ID_CantPasajeros,ID_TipoTrans,Precio,Tipo_Moneda")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                db.Automovils.Add(automovil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros", automovil.ID_CantPasajeros);
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1", automovil.ID_Marca);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo", automovil.ID_Tipo);
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1", automovil.ID_TipoTrans);
            return View(automovil);
        }

        // GET: Automoviles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros", automovil.ID_CantPasajeros);
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1", automovil.ID_Marca);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo", automovil.ID_Tipo);
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1", automovil.ID_TipoTrans);
            return View(automovil);
        }

        // POST: Automoviles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Auto,ID_Marca,Modelo,Año_Fabricacion,ID_Tipo,ID_CantPasajeros,ID_TipoTrans,Precio,Tipo_Moneda")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(automovil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros", automovil.ID_CantPasajeros);
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1", automovil.ID_Marca);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo", automovil.ID_Tipo);
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1", automovil.ID_TipoTrans);
            return View(automovil);
        }

        // GET: Automoviles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = db.Automovils.Find(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // POST: Automoviles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Automovil automovil = db.Automovils.Find(id);
            db.Automovils.Remove(automovil);
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

          public ActionResult Search()
          {
              int año; float precio; string moneda; int pasajeros; string marca; string tipoauto; string tipotrans;
             año = 2017;
              precio = (float) 3000000.00;
              pasajeros = 2; 
             marca = "Bugatti";
              tipoauto = "Coupé";
              tipotrans = "Manual";
              moneda = "EUR";


                var automovils = db.Automovils.SqlQuery(@"SELECT * FROM Automovil INNER JOIN Marca ON Automovil.ID_Marca = Marca.ID_Marca INNER JOIN Cant_Pasajeros ON Automovil.ID_CantPasajeros = Cant_Pasajeros.ID_CanPasajeros INNER JOIN Tipo_Automovil ON Automovil.ID_Tipo = Tipo_Automovil.ID_Tipo INNER JOIN Tipo_Trans ON Automovil.ID_TipoTrans = Tipo_Trans.ID_TipoTrans
WHERE Marca = @param1 AND CanPasajeros = @param2 AND Tipo = @param3 AND Tipo_Trans = @param4 
AND Precio =@param5 AND Año_Fabricacion = @param6 AND Tipo_Moneda = @param7", 
                  new SqlParameter("param1", marca), 
                  new SqlParameter("param2", pasajeros),
                  new SqlParameter("param3", tipoauto),
                  new SqlParameter("param4", tipotrans),
                  new SqlParameter("param5", precio),
                  new SqlParameter("param6", año),
                  new SqlParameter("param7", moneda)
                  ).ToList();
                
            return View(automovils);
        }

        public ActionResult Index2(string searchString, string searchString1, string searchString2, string searchString3, string searchString4, string searchString5, string searchString6, string searchString7)
        {
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros");
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1");
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo");
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1");

            
            var automovils = db.Automovils.Include(a => a.Cant_Pasajeros).Include(a => a.Marca).Include(a => a.Tipo_Automovil).Include(a => a.Tipo_Trans);

            decimal price = searchString6.AsDecimal();
            using (var context = new DealersEntities())
            {
                
                var model = from s in context.Automovils
                    select s;
                //Added this area to, Search and match data, if search string is not null or empty
                if (!String.IsNullOrEmpty(searchString+ searchString1+ searchString2+ searchString3+ searchString4+ searchString+ searchString5+ searchString6+ searchString7))
                {
                    automovils = automovils.Where(s => s.Modelo.Contains(searchString) 
                    && s.Tipo_Moneda.Contains(searchString1)
                    && s.Marca.Marca1.Contains(searchString2)
                    && s.Tipo_Automovil.Tipo.Contains(searchString3)
                    && s.Cant_Pasajeros.CanPasajeros.ToString().Contains(searchString4)
                    && s.Año_Fabricacion.ToString().Contains(searchString5)
                    && s.Precio >=(price)
                    && s.Tipo_Trans.Tipo_Trans1.Contains(searchString7)
                                                       );
                    //model = model.Where(s => s.Modelo.Contains(searchString));
                    // || s.Cant_Pasajeros.Contains(searchString)
                    //|| s.Tipo_Automovil.Contains(searchString));
                }
                /*switch (sortOrder)
                {
                    case "name_desc":
                        ModelList = model.OrderByDescending(s => s.Marca).ToList();
                        break;

                    default:
                        ModelList = model.OrderBy(automovils => automovils.Marca).ToList();
                        break;
                }
                */
            }

            return View(automovils.ToList());
        }        
    }
}
