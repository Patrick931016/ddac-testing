using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContainerManagementSystem.Models;

namespace ContainerManagementSystem.Controllers
{
    [Authorize]
    public class ShipmentsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Shipments
        public ActionResult Index()
        {
            return View(db.shps.ToList());
        }

        public ActionResult Search(string search)
        {
            //if a user choose the radio button option as Subject  
            if (search != "")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index", db.shps.Where(db => db.shipmentCode == search).ToList());
            }
            else
            {
                return View(db.shps.ToList());
            }
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shp shp = db.shps.Find(id);
            if (shp == null)
            {
                return HttpNotFound();
            }
            return View(shp);
        }

        // GET: Shipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "shipmentId,shipModel,shipmentCode,shipContainer,shipmentStatus,shipmentStartDate,shipmentEndDate")] shp shp)
        {
            if (ModelState.IsValid)
            {
                db.shps.Add(shp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shp);
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shp shp = db.shps.Find(id);
            if (shp == null)
            {
                return HttpNotFound();
            }
            return View(shp);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "shipmentId,shipModel,shipmentCode,shipContainer,shipmentStatus,shipmentStartDate,shipmentEndDate")] shp shp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shp);
        }

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shp shp = db.shps.Find(id);
            if (shp == null)
            {
                return HttpNotFound();
            }
            return View(shp);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            shp shp = db.shps.Find(id);
            db.shps.Remove(shp);
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
