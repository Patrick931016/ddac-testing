using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContainerManagementSystem.Models;

namespace ContainerManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CMSEntities db = new CMSEntities();
        public ActionResult Index()
        {
            Session["UserType"] = "";
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(usr user)
        {
            var usr = db.usrs.Where(u => u.userName == user.userName && u.userPassword == user.userPassword).FirstOrDefault();
            if (usr != null)
            {
                Session["UserId"] = usr.userId.ToString();
                Session["Username"] = usr.userName.ToString();
                Session["UserType"] = usr.userType.ToString();
                if (Session["UserType"].ToString() == "Admin")
                {
                    return RedirectToAction("Index", "users");
                }
                else if (Session["UserType"].ToString() == "Agent")
                {
                    return RedirectToAction("Index", "Bookings");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong.");
            }

            return View();
        }
    }
}