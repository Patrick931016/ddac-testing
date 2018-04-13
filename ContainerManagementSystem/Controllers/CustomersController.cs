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
    public class CustomersController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.cus.ToList());
        }

        public ActionResult Search(string search, cu x)
        {
            //if a user choose the radio button option as Subject  
            if (search != "")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index", db.cus.Where(db => db.customerName == search).ToList());
            }
            else
            {
                return View(db.cus.ToList());
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cu cu = db.cus.Find(id);
            if (cu == null)
            {
                return HttpNotFound();
            }
            return View(cu);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerId,customerName,customerEmail,customerPhone")] cu cu)
        {
            if (ModelState.IsValid)
            {
                db.cus.Add(cu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cu);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cu cu = db.cus.Find(id);
            if (cu == null)
            {
                return HttpNotFound();
            }
            return View(cu);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerId,customerName,customerEmail,customerPhone")] cu cu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cu);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cu cu = db.cus.Find(id);
            if (cu == null)
            {
                return HttpNotFound();
            }
            return View(cu);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cu cu = db.cus.Find(id);
            db.cus.Remove(cu);
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
