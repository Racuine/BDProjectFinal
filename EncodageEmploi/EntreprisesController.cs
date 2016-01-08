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
    public class EntreprisesController : Controller
    {
        private DBIG3A5Entities db = new DBIG3A5Entities();

        // GET: Entreprises
        public ActionResult Index()
        {
            var entreprise = db.Entreprise.Include(e => e.Langue);
            return View(entreprise.ToList());
        }

        // GET: Entreprises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprise.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // GET: Entreprises/Create
        public ActionResult Create()
        {
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle");
            return View();
        }

        // POST: Entreprises/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numero,denomination,numTel,nbrTravSoumis,nbrTravNonSoumis,idLangue")] Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                db.Entreprise.Add(entreprise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", entreprise.idLangue);
            return View(entreprise);
        }

        // GET: Entreprises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprise.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", entreprise.idLangue);
            return View(entreprise);
        }

        // POST: Entreprises/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "numero,denomination,numTel,nbrTravSoumis,nbrTravNonSoumis,idLangue")] Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entreprise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", entreprise.idLangue);
            return View(entreprise);
        }

        // GET: Entreprises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprise.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // POST: Entreprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entreprise entreprise = db.Entreprise.Find(id);
            db.Entreprise.Remove(entreprise);
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
