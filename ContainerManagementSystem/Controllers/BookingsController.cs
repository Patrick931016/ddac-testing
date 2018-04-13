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
    public class BookingsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            var bkgs = db.bkgs.Include(b => b.agn).Include(b => b.cu).Include(b => b.shp);
            return View(bkgs.ToList());
        }

        public ActionResult Search(string search, agn x)
        {
            if (search != null)
            {
                return View("Index", db.bkgs.Where(db => db.cu.customerName.ToString() == search).ToList());
            }
            else
            {
                var bkgs = db.bkgs.Include(b => b.agn).Include(b => b.cu).Include(b => b.shp);
                return View("Index", bkgs.ToList());
            }
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bkg bkg = db.bkgs.Find(id);
            if (bkg == null)
            {
                return HttpNotFound();
            }
            return View(bkg);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.agnId = new SelectList(db.agns, "agentId", "agentName");
            ViewBag.custId = new SelectList(db.cus, "customerId", "customerName");
            ViewBag.shipId = new SelectList(db.shps, "shipmentId", "shipModel");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingId,custId,shipId,agnId")] bkg bkg)
        {
            if (ModelState.IsValid)
            {
                db.bkgs.Add(bkg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.agnId = new SelectList(db.agns, "agentId", "agentName", bkg.agnId);
            ViewBag.custId = new SelectList(db.cus, "customerId", "customerName", bkg.custId);
            ViewBag.shipId = new SelectList(db.shps, "shipmentId", "shipModel", bkg.shipId);
            return View(bkg);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bkg bkg = db.bkgs.Find(id);
            if (bkg == null)
            {
                return HttpNotFound();
            }
            ViewBag.agnId = new SelectList(db.agns, "agentId", "agentName", bkg.agnId);
            ViewBag.custId = new SelectList(db.cus, "customerId", "customerName", bkg.custId);
            ViewBag.shipId = new SelectList(db.shps, "shipmentId", "shipModel", bkg.shipId);
            return View(bkg);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingId,custId,shipId,agnId")] bkg bkg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bkg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.agnId = new SelectList(db.agns, "agentId", "agentName", bkg.agnId);
            ViewBag.custId = new SelectList(db.cus, "customerId", "customerName", bkg.custId);
            ViewBag.shipId = new SelectList(db.shps, "shipmentId", "shipModel", bkg.shipId);
            return View(bkg);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bkg bkg = db.bkgs.Find(id);
            if (bkg == null)
            {
                return HttpNotFound();
            }
            return View(bkg);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bkg bkg = db.bkgs.Find(id);
            db.bkgs.Remove(bkg);
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
