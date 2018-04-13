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
    public class usersController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: users
        public ActionResult Index()
        {
            return View(db.usrs.ToList());
        }
        public ActionResult Search(string search,usr x)
        {
            //if a user choose the radio button option as Subject  
            if (search != "")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index",db.usrs.Where(db => db.userName == search).ToList());
            }
            else
            {
                return View(db.usrs.ToList());
            }
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usr usr = db.usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,userName,userPassword,userType")] usr usr)
        {
            if (ModelState.IsValid)
            {
                db.usrs.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usr);
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usr usr = db.usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,userName,userPassword,userType")] usr usr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usr);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usr usr = db.usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usr usr = db.usrs.Find(id);
            db.usrs.Remove(usr);
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
