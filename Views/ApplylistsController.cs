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
    public class ApplylistsController : Controller
    {
        private HiredHuntersEntities1 db = new HiredHuntersEntities1();

        // GET: Applylists
        public ActionResult Index()
        {
            var applylists = db.Applylists.Include(a => a.Freelencer).Include(a => a.Job);
            return View(applylists.ToList());
        }

        // GET: Applylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applylist applylist = db.Applylists.Find(id);
            if (applylist == null)
            {
                return HttpNotFound();
            }
            return View(applylist);
        }

        // GET: Applylists/Create
        public ActionResult Create()
        {
            ViewBag.Freelencer_ID = new SelectList(db.Freelencers, "f_no", "Freelencer_ID");
            ViewBag.Job_ID = new SelectList(db.Jobs, "J_no", "Job_ID");
            return View();
        }

        // POST: Applylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apply_no,Freelencer_ID,Job_ID,isgiven")] Applylist applylist)
        {
            if (ModelState.IsValid)
            {
                db.Applylists.Add(applylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Freelencer_ID = new SelectList(db.Freelencers, "f_no", "Freelencer_ID", applylist.Freelencer_ID);
            ViewBag.Job_ID = new SelectList(db.Jobs, "J_no", "Job_ID", applylist.Job_ID);
            return View(applylist);
        }

        // GET: Applylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applylist applylist = db.Applylists.Find(id);
            if (applylist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Freelencer_ID = new SelectList(db.Freelencers, "f_no", "Freelencer_ID", applylist.Freelencer_ID);
            ViewBag.Job_ID = new SelectList(db.Jobs, "J_no", "Job_ID", applylist.Job_ID);
            return View(applylist);
        }

        // POST: Applylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "apply_no,Freelencer_ID,Job_ID,isgiven")] Applylist applylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Freelencer_ID = new SelectList(db.Freelencers, "f_no", "Freelencer_ID", applylist.Freelencer_ID);
            ViewBag.Job_ID = new SelectList(db.Jobs, "J_no", "Job_ID", applylist.Job_ID);
            return View(applylist);
        }

        // GET: Applylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applylist applylist = db.Applylists.Find(id);
            if (applylist == null)
            {
                return HttpNotFound();
            }
            return View(applylist);
        }

        // POST: Applylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applylist applylist = db.Applylists.Find(id);
            db.Applylists.Remove(applylist);
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
