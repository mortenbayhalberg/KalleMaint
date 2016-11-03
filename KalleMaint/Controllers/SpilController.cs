using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KalleMaint.Models;

namespace KalleMaint.Controllers
{
    public class SpilController : Controller
    {
        private DBKalleKamelEntities db = new DBKalleKamelEntities();

        // GET: Spil
        // spillerID er passed og er nummeret/id'et på tblSpiller(spillerPK), der er valgt.
        public ActionResult Index(int spillerID)
        {
            // Jeg læser denne som, husk vi skal have Spiller oplysningen med.
            // I Model er der defineret sammenhæng mellem tabeller.
            var tblSpils = db.tblSpils.Include(t => t.tblSpiller);

            // Svaret er en list vi ikke ved hvor lang er
            // List indholdet i tblSpil
            // Gem resultatet i ListSpil
            // db.tblSpils.Where - clause som søger på en anden nøgle end Spil primær key, nemlig spiller ID. Jeg vil kun have spillene til den valgte spiller
            // y => Variabel der skal benyttes
            List<tblSpil> ListSpil = db.tblSpils.Where(y => y.spilSpillerID == spillerID).ToList();

            ViewBag.spillerID = spillerID;

            var spillerInfo = db.tblSpillers.FirstOrDefault(z => z.spillerPK == spillerID);
            ViewBag.spillerNavn = spillerInfo.spillerForNavn + " " + spillerInfo.spillerEfterNavn;

            // Oprindeligt: return View(db.tblSpil.ToList());
            return View(ListSpil);
        }

        // GET: Spil/Details/5
        public ActionResult Details(int? id, int spillerID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpil tblSpil = db.tblSpils.Find(id);
            if (tblSpil == null)
            {
                return HttpNotFound();
            }
            // ViewBag.spillerID = id;
            ViewBag.spillerID = spillerID;

            ViewBag.spillerNavn = tblSpil.tblSpiller.spillerForNavn + " " + tblSpil.tblSpiller.spillerEfterNavn;

            return View(tblSpil);
        }

        // GET: Spil/Create
        public ActionResult Create(int spillerID)
        {
            // Skal ikke benytte en liste, spilleren er valgt, som skal have oprettet spillet.
            // ViewBag.spilSpillerID = new SelectList(db.tblSpillers, "spillerPK", "spillerForNavn");

            ViewBag.spillerID = spillerID;

            var spillerInfo = db.tblSpillers.FirstOrDefault(z => z.spillerPK == spillerID);
            ViewBag.spillerNavn = spillerInfo.spillerForNavn + " " + spillerInfo.spillerEfterNavn;

            return View();
        }

        // POST: Spil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int spillerID, [Bind(Include = "spilPK,spilSpillerID,spilDato,spilScore1")] tblSpil tblSpil)
        {
            if (ModelState.IsValid)
            {
                tblSpil.spilSpillerID = spillerID;
                db.tblSpils.Add(tblSpil);
                db.SaveChanges();
                return RedirectToAction("Index", new { spillerID = tblSpil.spilSpillerID });
            }

            // Danne listen med spillere, hvorfra man kan vælge hvilken der skal oprettes spil fra.
            // Denne skal udskiftes senere med at spilleren allerede er valgt og kommer fra Home-controller
            ViewBag.spilSpillerID = new SelectList(db.tblSpillers, "spillerPK", "spillerForNavn", tblSpil.spilSpillerID);

            ViewBag.spillerID = tblSpil.spilSpillerID;

            return View(tblSpil);
        }

        // GET: Spil/Edit/5
        public ActionResult Edit(int? id, int spillerID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpil tblSpil = db.tblSpils.Find(id);
            if (tblSpil == null)
            {
                return HttpNotFound();
            }
            // Skal ikke længere benytte drop down, spiller er valgt.
            //ViewBag.spilSpillerID = new SelectList(db.tblSpillers, "spillerPK", "spillerForNavn", tblSpil.spilSpillerID);

            ViewBag.spillerID = spillerID;

            ViewBag.spillerNavn = tblSpil.tblSpiller.spillerForNavn + " " + tblSpil.tblSpiller.spillerEfterNavn;

            return View(tblSpil);
        }

        // POST: Spil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int spillerID, [Bind(Include = "spilPK,spilSpillerID,spilDato,spilScore1")] tblSpil tblSpil)
        {
            if (ModelState.IsValid)
            {
                tblSpil.spilSpillerID = spillerID;
                db.Entry(tblSpil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { spillerID = tblSpil.spilSpillerID });
            }
            ViewBag.spilSpillerID = new SelectList(db.tblSpillers, "spillerPK", "spillerForNavn", tblSpil.spilSpillerID);

            ViewBag.spillerID = tblSpil.spilSpillerID;

            return View(tblSpil);
        }

        // GET: Spil/Delete/5
        public ActionResult Delete(int? id, int spillerID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpil tblSpil = db.tblSpils.Find(id);
            if (tblSpil == null)
            {
                return HttpNotFound();
            }

            ViewBag.spillerID = tblSpil.spilSpillerID;

            ViewBag.spillerNavn = tblSpil.tblSpiller.spillerForNavn + " " + tblSpil.tblSpiller.spillerEfterNavn;

            return View(tblSpil);
        }

        // POST: Spil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int spillerID)
        {
            tblSpil tblSpil = db.tblSpils.Find(id);
            db.tblSpils.Remove(tblSpil);
            db.SaveChanges();
            return RedirectToAction("Index", new { spillerID});
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
