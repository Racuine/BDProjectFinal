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
    public class DenominationEmploisController : Controller
    {
        private DBIG3A5Entities db = new DBIG3A5Entities();

        // GET: DenominationEmplois
        public ActionResult Index()
        {
            var denominationEmploi = db.DenominationEmploi.Include(d => d.Langue);
            return View(denominationEmploi.ToList());
        }

        // GET: DenominationEmplois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenominationEmploi denominationEmploi = db.DenominationEmploi.Find(id);
            if (denominationEmploi == null)
            {
                return HttpNotFound();
            }
            return View(denominationEmploi);
        }

        // GET: DenominationEmplois/Create
        public ActionResult Create()
        {
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle");
            return View();
        }

        // POST: DenominationEmplois/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDenom,denomination,idLangue")] DenominationEmploi denominationEmploi)
        {
            if (ModelState.IsValid)
            {
                db.DenominationEmploi.Add(denominationEmploi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", denominationEmploi.idLangue);
            return View(denominationEmploi);
        }

        // GET: DenominationEmplois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenominationEmploi denominationEmploi = db.DenominationEmploi.Find(id);
            if (denominationEmploi == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", denominationEmploi.idLangue);
            return View(denominationEmploi);
        }

        // POST: DenominationEmplois/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDenom,denomination,idLangue")] DenominationEmploi denominationEmploi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(denominationEmploi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idLangue = new SelectList(db.Langue, "idLangue", "libelle", denominationEmploi.idLangue);
            return View(denominationEmploi);
        }

        // GET: DenominationEmplois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenominationEmploi denominationEmploi = db.DenominationEmploi.Find(id);
            if (denominationEmploi == null)
            {
                return HttpNotFound();
            }
            return View(denominationEmploi);
        }

        // POST: DenominationEmplois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DenominationEmploi denominationEmploi = db.DenominationEmploi.Find(id);
            db.DenominationEmploi.Remove(denominationEmploi);
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
