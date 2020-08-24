using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterDetailsDemo.DAL;
using MasterDetailsDemo.Models;

namespace MasterDetailsDemo.Controllers
{
    public class UnitMeasurementController : Controller
    {
        private MasterDetailDemoContext db = new MasterDetailDemoContext();

        // GET: UnitMeasurement
        public ActionResult Index()
        {
            return View(db.UnitMeasurements.ToList());
        }

        // GET: UnitMeasurement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitMeasurement unitMeasurement = db.UnitMeasurements.Find(id);
            if (unitMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(unitMeasurement);
        }

        // GET: UnitMeasurement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitMeasurement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Description")] UnitMeasurement unitMeasurement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitMeasurement.UnitMeasurementId = Guid.NewGuid().ToString();
                    db.UnitMeasurements.Add(unitMeasurement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again,and if the problem persists see your system administrator.");
            }

            return View(unitMeasurement);
        }

        // GET: UnitMeasurement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitMeasurement unitMeasurement = db.UnitMeasurements.Find(id);
            if (unitMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(unitMeasurement);
        }

        // POST: UnitMeasurement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitMeasurementId,Code,Description")] UnitMeasurement unitMeasurement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(unitMeasurement).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again,and if the problem persists see your system administrator.");

            }

            return View(unitMeasurement);
        }

        // GET: UnitMeasurement/Delete/5
        public ActionResult Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            UnitMeasurement unitMeasurement = db.UnitMeasurements.Find(id);
            if (unitMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(unitMeasurement);
        }

        // POST: UnitMeasurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                UnitMeasurement unitMeasurement = db.UnitMeasurements.Find(id);
                db.UnitMeasurements.Remove(unitMeasurement);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            
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
