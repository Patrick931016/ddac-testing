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
    public class AgentsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Agents
        public ActionResult Index()
        {
            var agns = db.agns.Include(a => a.usr);
            return View(agns.ToList());
        }

        public ActionResult Search(string search, agn x)
        {
            //if a user choose the radio button option as Subject  
            if (search != "")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index", db.agns.Where(db => db.agentName == search || search == null).ToList());
            }
            else
            {
                var agns = db.agns.Include(a => a.usr);
                return View("Index",db.agns.ToList());
            }
        }

        // GET: Agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agn agn = db.agns.Find(id);
            if (agn == null)
            {
                return HttpNotFound();
            }
            return View(agn);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.usrs, "userId", "userName");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "agentId,agentName,agentAge,agentDOB,agentPhone,agentEmail,userId")] agn agn)
        {
            if (ModelState.IsValid)
            {
                db.agns.Add(agn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.usrs, "userId", "userName", agn.userId);
            return View(agn);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agn agn = db.agns.Find(id);
            if (agn == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.usrs, "userId", "userName", agn.userId);
            return View(agn);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "agentId,agentName,agentAge,agentDOB,agentPhone,agentEmail,userId")] agn agn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.usrs, "userId", "userName", agn.userId);
            return View(agn);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agn agn = db.agns.Find(id);
            if (agn == null)
            {
                return HttpNotFound();
            }
            return View(agn);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agn agn = db.agns.Find(id);
            db.agns.Remove(agn);
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
