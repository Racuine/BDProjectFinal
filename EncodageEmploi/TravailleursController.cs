using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EncodageEmploi
{
    public class TravailleursController : Controller
    {
        private DBIG3A5Entities db = new DBIG3A5Entities();

        // GET: Travailleurs
        public ActionResult Index()
        {
            return View(db.Travailleur.ToList());
        }

        // GET: Travailleurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travailleur travailleur = db.Travailleur.Find(id);
            if (travailleur == null)
            {
                return HttpNotFound();
            }
            return View(travailleur);
        }

        // GET: Travailleurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Travailleurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTravailleur,nom,numeroDossierMed")] Travailleur travailleur)
        {
            if (ModelState.IsValid)
            {
                db.Travailleur.Add(travailleur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(travailleur);
        }

        // GET: Travailleurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travailleur travailleur = db.Travailleur.Find(id);
            if (travailleur == null)
            {
                return HttpNotFound();
            }
            return View(travailleur);
        }

        // POST: Travailleurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTravailleur,nom,numeroDossierMed")] Travailleur travailleur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travailleur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(travailleur);
        }

        // GET: Travailleurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travailleur travailleur = db.Travailleur.Find(id);
            if (travailleur == null)
            {
                return HttpNotFound();
            }
            return View(travailleur);
        }

        // POST: Travailleurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Travailleur travailleur = db.Travailleur.Find(id);
            db.Travailleur.Remove(travailleur);
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
