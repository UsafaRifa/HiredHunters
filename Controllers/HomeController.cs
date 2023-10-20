using HiredHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiredHunters.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["r_no"] != null)
            {
                ViewBag.loggedIn = "User already logged in";
                return RedirectToAction("Index", "Recruiter");

            }
            return View();
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

        public ActionResult SignUP_Index()
        {
            if (Session["r_no"] != null)
            {
                ViewBag.loggedIn = "User already logged in";
                return RedirectToAction("Index", "Recruiter");

            }else { return View(); }
        }
        [HttpGet]
        public ActionResult Login_Index()
        {
            if (Session["r_no"] != null)
            {
                ViewBag.loggedIn = "User already logged in";
                return RedirectToAction("Index", "Recruiter");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login_Index(FormCollection collection)
        {
            
            
            string btn=collection["flexRadioDefault"].ToString();
            string mail = collection["Email"];
            string p = collection["pass"];
            HiredHuntersEntities1 db = new HiredHuntersEntities1();
            if(btn.Equals("Freelencer"))
            {
                var user = db.Freelencers.Where(x => x.Email == mail).FirstOrDefault();
                if(user != null)
                {
                    if (string.Compare(Crypto.Hash(p),user.pass)==0)
                    {
                        Session["f_no"] = user.f_no;
                        
                        return RedirectToAction("Index","Freelancer");
                    }
                    else
                    {
                        ViewBag.errmesg = "Login Failed";
                       
                    }
                }
            }
            else
            {
                var user = db.Recruiters.Where(x => x.Email == mail).FirstOrDefault();
                if (user != null)
                {
                    if (string.Compare(Crypto.Hash(p), user.pass) == 0)
                    {
                        Session["r_no"] = user.r_no;
                        Response.ClearHeaders();

                        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Cache.SetNoStore();
                        return RedirectToAction("Index", "Recruiter");
                    }
                    else
                    {
                        ViewBag.errmesg = "Login Failed";
                        
                    }
                }
            }
            return View();
            
        }
    }
}