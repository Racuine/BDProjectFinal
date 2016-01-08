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
    public class EmploisController : Controller
    {
        private DBIG3A5Entities db = new DBIG3A5Entities();

        // GET: Emplois
        public ActionResult Index()
        {
            var emploi = db.Emploi.Include(e => e.DenominationEmploi).Include(e => e.Entreprise).Include(e => e.Travailleur);
            return View(emploi.ToList());
        }

        // GET: Emplois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emploi.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // GET: Emplois/Create
        public ActionResult Create()
        {
            ViewBag.idDenom = new SelectList(db.DenominationEmploi, "idDenom", "denomination");
            ViewBag.numero = new SelectList(db.Entreprise, "numero", "denomination");
            ViewBag.idTravailleur = new SelectList(db.Travailleur, "idTravailleur", "nom");
            return View();
        }

        // POST: Emplois/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Emploi emploi)
        {
            DateTime dateEnt = emploi.dateEntree;
            DateTime? dateSor = emploi.dateSortie;

            if (dateEnt > dateSor)
            {
                // insert code "Alert"
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Emploi.Add(emploi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idDenom = new SelectList(db.DenominationEmploi, "idDenom", "denomination", emploi.idDenom);
                ViewBag.numero = new SelectList(db.Entreprise, "numero", "denomination", emploi.numero);
                ViewBag.idTravailleur = new SelectList(db.Travailleur, "idTravailleur", "nom", emploi.idTravailleur);
            }
            return View(emploi);
        }

        // GET: Emplois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emploi.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDenom = new SelectList(db.DenominationEmploi, "idDenom", "denomination", emploi.idDenom);
            ViewBag.numero = new SelectList(db.Entreprise, "numero", "denomination", emploi.numero);
            ViewBag.idTravailleur = new SelectList(db.Travailleur, "idTravailleur", "nom", emploi.idTravailleur);
           
            return View(emploi);
        }

        // POST: Emplois/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTravail,idTravailleur,numero,codeTravail,dateEntree,dateSortie,idDenom")] Emploi emploi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDenom = new SelectList(db.DenominationEmploi, "idDenom", "denomination", emploi.idDenom);
            ViewBag.numero = new SelectList(db.Entreprise, "numero", "denomination", emploi.numero);
            ViewBag.idTravailleur = new SelectList(db.Travailleur, "idTravailleur", "nom", emploi.idTravailleur);
            return View(emploi);
        }

        // GET: Emplois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emploi.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // POST: Emplois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emploi emploi = db.Emploi.Find(id);
            db.Emploi.Remove(emploi);
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
