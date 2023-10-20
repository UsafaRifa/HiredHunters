using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiredHunters.Models;

namespace HiredHunters.Views
{
    public class Admin_ManangesController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: Admin_Mananges
        public ActionResult Index()
        {
            return View(db.Mananges.ToList());
        }
        public ActionResult Login_Index()
        {
            return View();
        }


        public ActionResult logout()
        {
            if (Session["username"] != null)
            {
                Session["username"] = null;
                return RedirectToAction("Login_Index", "Admin_Mananges");
            }
            else
            {
                return RedirectToAction("Login_Index", "Admin_Mananges");
            }
           
        }

        [HttpPost]
        public ActionResult Login_Index(FormCollection collection)
        {
           // string btn = collection["flexRadioDefault"].ToString();
            string mail = collection["email"];
            string p = collection["pass"];
            HiredHuntersEntities1 db = new HiredHuntersEntities1();


            //var user = db.Freelencers.Where(x => x.Email == mail).FirstOrDefault();
            var user = db.Mananges.Where(x=> x.username==mail).FirstOrDefault();

            if (user != null)
                {
                    if (p==user.pass)
                    {
                        Session["username"] = user.username;
                        return RedirectToAction("Index", "Admin_job_Details");
                    }
          
                    else
                    {
                        ViewBag.errmesg = "Login Failed";

                    }
                }


            return RedirectToAction("Index", "Admin_job_Details");

        }
    




// GET: Admin_Mananges/Details/5
public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manange manange = db.Mananges.Find(id);
            if (manange == null)
            {
                return HttpNotFound();
            }
            return View(manange);
        }

        // GET: Admin_Mananges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Mananges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,pass")] Manange manange)
        {
            if (ModelState.IsValid)
            {
                db.Mananges.Add(manange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manange);
        }

        // GET: Admin_Mananges/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manange manange = db.Mananges.Find(id);
            if (manange == null)
            {
                return HttpNotFound();
            }
            return View(manange);
        }

        // POST: Admin_Mananges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,pass")] Manange manange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manange);
        }

        // GET: Admin_Mananges/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manange manange = db.Mananges.Find(id);
            if (manange == null)
            {
                return HttpNotFound();
            }
            return View(manange);
        }

        // POST: Admin_Mananges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Manange manange = db.Mananges.Find(id);
            db.Mananges.Remove(manange);
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
