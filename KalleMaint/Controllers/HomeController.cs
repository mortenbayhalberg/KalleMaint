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
    public class HomeController : Controller
    {
        private DBKalleKamelEntities db = new DBKalleKamelEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.tblSpillers.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpiller tblSpiller = db.tblSpillers.Find(id);
            if (tblSpiller == null)
            {
                return HttpNotFound();
            }

            ViewBag.spillerNavn = tblSpiller.spillerForNavn + " " + tblSpiller.spillerEfterNavn;

            return View(tblSpiller);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "spillerPK,spillerForNavn,spillerEfterNavn,spillerOptagelsesDato,spillerFunktion,spillerUdmeldt")] tblSpiller tblSpiller)
        {
            if (ModelState.IsValid)
            {
                db.tblSpillers.Add(tblSpiller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSpiller);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpiller tblSpiller = db.tblSpillers.Find(id);
            if (tblSpiller == null)
            {
                return HttpNotFound();
            }

            ViewBag.spillerNavn = tblSpiller.spillerForNavn + " " + tblSpiller.spillerEfterNavn;

            return View(tblSpiller);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "spillerPK,spillerForNavn,spillerEfterNavn,spillerOptagelsesDato,spillerFunktion,spillerUdmeldt")] tblSpiller tblSpiller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSpiller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSpiller);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSpiller tblSpiller = db.tblSpillers.Find(id);
            if (tblSpiller == null)
            {
                return HttpNotFound();
            }

            ViewBag.spillerNavn = tblSpiller.spillerForNavn + " " + tblSpiller.spillerEfterNavn;

            return View(tblSpiller);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSpiller tblSpiller = db.tblSpillers.Find(id);
            db.tblSpillers.Remove(tblSpiller);
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
