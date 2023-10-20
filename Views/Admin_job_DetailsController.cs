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
    public class Admin_job_DetailsController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: Admin_job_Details
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Recruiter);
            return View(jobs.ToList());
        }
        public ActionResult Approve(int? id) {
            var user = db.Jobs.Find(id);
            user.isAvailable = 1;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult DisApprove(int? id)
        {
            var user = db.Jobs.Find(id);
            user.isAvailable = 0;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Dlt(int? id)
        {
        

            var job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        // GET: Admin_job_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // GET: Admin_job_Details/Create
        public ActionResult Create()
        {
            ViewBag.Recruiter_ID = new SelectList(db.Recruiters, "r_no", "Recruiter_ID");
            return View();
        }

        // POST: Admin_job_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "J_no,Job_ID,Title,Details,CreateDate,Locations,isAvailable,Recruiter_ID,AskingPrice")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Recruiter_ID = new SelectList(db.Recruiters, "r_no", "Recruiter_ID", job.Recruiter_ID);
            return View(job);
        }

        // GET: Admin_job_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.Recruiter_ID = new SelectList(db.Recruiters, "r_no", "Recruiter_ID", job.Recruiter_ID);
            return View(job);
        }

        // POST: Admin_job_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "J_no,Job_ID,Title,Details,CreateDate,Locations,isAvailable,Recruiter_ID,AskingPrice")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Recruiter_ID = new SelectList(db.Recruiters, "r_no", "Recruiter_ID", job.Recruiter_ID);
            return View(job);
        }
        // GET: Admin_job_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Admin_job_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
