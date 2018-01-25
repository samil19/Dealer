using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;
using Dealer.Models;
using Microsoft.AspNet.Identity;
using PagedList;


namespace Dealer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AutomovilesController : Controller
    {
        private DealersEntities db = new DealersEntities();
        private DealersEntities db1 = new DealersEntities();

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

        [OverrideAuthorization]
        public ActionResult AutoDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //

            var detalles = db1.Detalles.Include(d => d.Automovil).Include(d => d.Automovil1).Include(d => d.Sucursal);

            //Detalle details = new Detalle();

            

            var model = from s in db1.Detalles
                        select s;

            detalles = detalles.Where(s => (s.ID_Auto.ToString()).Contains(id.ToString()));

            //details = detalles.FirstOrDefault();

            Automovil automovil = db.Automovils.Find(id);

            var mixto = new AutomovilViewModel
            {
                Automovil = automovil,
                Detalle = (Detalle)detalles.FirstOrDefault()
            };
        //
    
            
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(mixto);
        }



        //Testing
        /*public ActionResult CreateViewModel()
        {
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros");
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1");
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo");
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1");
            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo");
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateViewModel([Bind(Include = "ID_Auto,ID_Marca,Modelo,Año_Fabricacion,ID_Tipo,ID_CantPasajeros,ID_TipoTrans,Precio,Tipo_MonedaID_Condicion,ID_Auto,Condicion,Cantidad,ID_Sucursal,Detalles")] Automovil automovil, Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Automovils.Add(automovil);
                detalle.ID_Auto = automovil.ID_Auto;
                db.Detalles.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros", automovil.ID_CantPasajeros);
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1", automovil.ID_Marca);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo", automovil.ID_Tipo);
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1", automovil.ID_TipoTrans);
            ViewBag.ID_Sucursal = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Sucursal);
            ViewBag.ID_Auto = new SelectList(db.Automovils, "ID_Auto", "Modelo", detalle.ID_Auto);
            ViewBag.ID_Sucursal = new SelectList(db.Sucursals, "ID_Sucursal", "Nombre", detalle.ID_Sucursal);

            return View();
        }*/




        //Testing





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
        /*[HttpPost]
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
        }*/

        //

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
                return Redirect("/Detalles/Create");
            }

            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros", automovil.ID_CantPasajeros);
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1", automovil.ID_Marca);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo", automovil.ID_Tipo);
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1", automovil.ID_TipoTrans);
            return View(automovil);
        }


            //


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

          /*public ActionResult Search()
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
        }*/
        [OverrideAuthorization]
        public ActionResult Index2(int? page, string currentFilter3, string currentFilter4, string currentFilter5, string currentFilter6, string currentFilter7,string currentFilter, string currentFilter2, string searchString, string searchString1, string searchString2, string searchString3, string searchString4, string searchString5, string searchString6, string searchString7)
        {
            ViewBag.ID_CantPasajeros = new SelectList(db.Cant_Pasajeros, "ID_CanPasajeros", "ID_CanPasajeros");
            ViewBag.ID_Marca = new SelectList(db.Marcas, "ID_Marca", "Marca1");
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Automovil, "ID_Tipo", "Tipo");
            ViewBag.ID_TipoTrans = new SelectList(db.Tipo_Trans, "ID_TipoTrans", "Tipo_Trans1");

            //searchString=(searchString == null) ? searchString="" : "";
            //searchString1 = (searchString1 == null) ? searchString1 = "" : "";
            //searchString2 = (searchString2 == null) ? searchString2 = "" : "";
            //searchString3 = (searchString3 == null) ? searchString3 = "" : "";
            //searchString4 = (searchString4 == null) ? searchString4 = "" : "";
            //searchString5 = (searchString5 == null) ? searchString5 = "" : "";
            //searchString6 = (searchString6 == null) ? searchString6 = "" : "";
            //searchString7 = (searchString7 == null) ? searchString7 = "" : "";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                searchString2 = currentFilter2;
                searchString3 = currentFilter3;
                searchString4 = currentFilter4;
                searchString5 = currentFilter5;
                searchString6 = currentFilter6;
                searchString7 = currentFilter7;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilter2 = searchString2;
            ViewBag.CurrentFilter3 = searchString3;
            ViewBag.CurrentFilter4 = searchString4;
            ViewBag.CurrentFilter5 = searchString5;
            ViewBag.CurrentFilter6 = searchString6;
            ViewBag.CurrentFilter7 = searchString7;

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
                }
            }
            int pageSize = 10;
            if (!String.IsNullOrEmpty(searchString + searchString1 + searchString2 + searchString3 + searchString4 + searchString + searchString5 + searchString6 + searchString7))
            {
                pageSize = automovils.ToList().Count;
                if(automovils.ToList().Count == 0)
                {
                    pageSize = 25;
                }
            }
            int pageNumber = (page ?? 1);
            return View((automovils.ToList()).ToPagedList(pageNumber, pageSize));
        }
        [OverrideAuthorization]
        [Authorize]
        public void Agregar(int? ID)
        {
            using (var context = new DealersEntities())
            {
                context.Database.ExecuteSqlCommand("INSERT INTO Saved VALUES (@param1,@param2)", new SqlParameter("param1",(User.Identity.GetUserId())), new SqlParameter("param2", ID) );
                context.SaveChanges(); 
            }

            ViewBag.Message = "Agregado a tu historial";
        }
        /*[OverrideAuthorization]
        [Authorize]
        public ActionResult SavedIndex()
        {
          List<Automovil> automovils = new List<Automovil>();

            using (var context = new DealersEntities())
            {
                //automovils = db.Automovils.SqlQuery("SELECT Automovil.ID_Auto, Automovil.ID_Marca, Automovil.Modelo, Automovil.Año_Fabricacion, Automovil.ID_Tipo, Automovil.ID_CantPasajeros, Automovil.ID_TipoTrans, Automovil.Precio, Automovil.Tipo_Moneda FROM Automovil INNER JOIN Saved ON Automovil.ID_Auto = Saved.ID_Auto WHERE Saved.Id = '@param'", new SqlParameter("param", User.Identity.GetUserId())).ToList<Automovil>();
                automovils = db.Automovils.SqlQuery(@"SELECT * FROM Automovil INNER JOIN Saved ON Automovil.ID_Auto = Saved.ID_Auto WHERE Saved.Id = @param", new SqlParameter("param", (User.Identity.GetUserId()))).ToList();


                return View(automovils);
            }
        }*/
        
    }
}
