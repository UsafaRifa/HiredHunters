using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiredHunters.Models;

namespace HiredHunters.Controllers
{
    public class JobsController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Recruiter);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
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

        // GET: Jobs/Create
        public ActionResult Create()
        {
            if (Session["r_no"] != null)
            {
                //Recruiter rec = dc.Recruiters.Find(Session["r_no"]);
                ViewBag.Recruiter_ID = Session["r_no"];
                return View();
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }

        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "J_no,Job_ID,Title,Details,CreateDate,Locations,isAvailable,Recruiter_ID,AskingPrice")] Job job)
        {
            if (Session["r_no"] != null)
            {
                if (ModelState.IsValid)
                {
                    job.Recruiter_ID= Int32.Parse(Session["r_no"].ToString());
                    db.Jobs.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("Index","Recruiter");
                   // ViewBag.Recruiter_ID = Session["r_no"];
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }


          
           // return View(job);
        }

        // GET: Jobs/Edit/5
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

        // POST: Jobs/Edit/5
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

        // GET: Jobs/Delete/5
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

        // POST: Jobs/Delete/5
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
        //Apply For job
        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["f_no"] != null)
            {
                HiredHuntersEntities1 db = new HiredHuntersEntities1();
                Applylist ap = new Applylist();
                ap.Freelencer_ID = (int?)Session["f_no"];
                ap.Job_ID = id;
                ap.isgiven = 0;
                db.Applylists.Add(ap);
                db.SaveChanges();
                return RedirectToAction("Index"); ;

            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
           
        }
    }
}
