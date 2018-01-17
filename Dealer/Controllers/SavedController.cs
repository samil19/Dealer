﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dealer.Models;
using Microsoft.AspNet.Identity;

namespace Dealer.Controllers
{
    public class SavedController : Controller
    {
        private DealersEntities db = new DealersEntities();
        // GET: Saved
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Index()
        {
            List<Automovil> automovils = new List<Automovil>();
                //automovils = db.Automovils.SqlQuery("SELECT Automovil.ID_Auto, Automovil.ID_Marca, Automovil.Modelo, Automovil.Año_Fabricacion, Automovil.ID_Tipo, Automovil.ID_CantPasajeros, Automovil.ID_TipoTrans, Automovil.Precio, Automovil.Tipo_Moneda FROM Automovil INNER JOIN Saved ON Automovil.ID_Auto = Saved.ID_Auto WHERE Saved.Id = '@param'", new SqlParameter("param", User.Identity.GetUserId())).ToList<Automovil>();
                automovils = db.Automovils.SqlQuery(@"SELECT * FROM Automovil INNER JOIN Saved ON Automovil.ID_Auto = Saved.ID_Auto WHERE Saved.Id = @param", new SqlParameter("param", (User.Identity.GetUserId()))).ToList();


                return View(automovils);
        }

        public RedirectToRouteResult Delete(int? id)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM Saved WHERE ID_Auto = @param", new SqlParameter("param", id));
            return RedirectToAction("Index");
        }
        public void Verificar()
        {
            using (var context = new DealersEntities())
            {
                var seleccion = context.Database.ExecuteSqlCommand("SELECT ID_Auto FROM Saved WHERE Id=@param1", new SqlParameter("param1", (User.Identity.GetUserId())));
                context.SaveChanges();
            }
        }
    }
}