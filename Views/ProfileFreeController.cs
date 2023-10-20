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
    public class ProfileFreeController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: ProfileFree
        public ActionResult Index()
        {
            return View(db.Freelencers.ToList());
        }

        // GET: ProfileFree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelencer freelencer = db.Freelencers.Find(id);
            if (freelencer == null)
            {
                return HttpNotFound();
            }
            return View(freelencer);
        }

        // GET: ProfileFree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileFree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "f_no,Freelencer_ID,FirstName,LastName,PhoneNumber,Email,DateofBirth,F_Address,DateofJoining,JobCompleted,NetIncome,rating,username,isEmailVarified,pass")] Freelencer freelencer)
        {
            if (ModelState.IsValid)
            {
                db.Freelencers.Add(freelencer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freelencer);
        }

        // GET: ProfileFree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelencer freelencer = db.Freelencers.Find(id);
            if (freelencer == null)
            {
                return HttpNotFound();
            }
            return View(freelencer);
        }

        // POST: ProfileFree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "f_no,Freelencer_ID,FirstName,LastName,PhoneNumber,Email,DateofBirth,F_Address,DateofJoining,JobCompleted,NetIncome,rating,username,isEmailVarified,pass")] Freelencer freelencer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freelencer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freelencer);
        }

        // GET: ProfileFree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Freelencer freelencer = db.Freelencers.Find(id);
            if (freelencer == null)
            {
                return HttpNotFound();
            }
            return View(freelencer);
        }

        // POST: ProfileFree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Freelencer freelencer = db.Freelencers.Find(id);
            db.Freelencers.Remove(freelencer);
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
