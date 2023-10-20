using HiredHunters.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace HiredHunters.Controllers
{
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class RecruiterController : Controller
    {
        // GET: Recruiter
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUP()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUP([Bind(Include = "FirstName,LastName,JoiningDate,Email,pass,ConfirmPassword")] Recruiter user)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion
                #region Generate Activation Code 
                //  user.ActivationCode = Guid.NewGuid();
                #endregion

                #region  Password Hashing 
                user.pass = Crypto.Hash(user.pass);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                //user.isEmailVarified = 0;

                #region Save to Database
                using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
                {
                    user.JoiningDate = DateTime.Now;
                    dc.Recruiters.Add(user);
                    dc.SaveChanges();
                    //Send Email to User
                    //SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    //message = "Registration successfully done. Account activation link " +
                    //    " has been sent to your email id:" + user.EmailID;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
            {
                var v = dc.Recruiters.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }

        //Profile Details
       // private string p;
        [HttpGet]
        public ActionResult RecruiterProfile()
        {
            if (Session["r_no"] != null)
            {
                using (HiredHuntersEntities1 dc = new HiredHuntersEntities1())
                {

                    Recruiter rec=dc.Recruiters.Find(Session["r_no"]);
                  //  p = rec.pass;
                    if (rec == null)
                    {
                        return HttpNotFound();
                    }
                    return View(rec);
                }
               // return View();
            }
            else
            {
                return RedirectToAction("Login_Index","Home");
            }
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecruiterProfile([Bind(Include = "Recruiter_ID, FirstName, LastName,  PhoneNumber, JoiningDate, R_address, Rating, Total_job_Posted, pass")] Recruiter recruiter)
        {
            if(Session["r_no"] != null)
            {

                HiredHuntersEntities1 db = new HiredHuntersEntities1();
                string q = "Update Recruiter set FirstName='" + recruiter.FirstName + "', LastName='"+recruiter.LastName+ "', PhoneNumber='" + recruiter.PhoneNumber + "', JoiningDate='" + recruiter.JoiningDate + "', R_address='" + recruiter.R_address + "' where Recruiter_ID='" + recruiter.Recruiter_ID+"'";
                //var user = db.Recruiters.SqlQuery(q).FirstOrDefault();
                var user=db.Recruiters.SqlQuery(q).FirstOrDefaultAsync();
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //string p=recruiter.pass;

                //if (ModelState.IsValid)
                //{
                //    var user = db.Recruiters.Find(Session["r_no"]);
                //    if(string.Compare(Crypto.Hash(p), user.pass) == 0)
                //    {
                //        db.Entry(recruiter).State = System.Data.Entity.EntityState.Modified;

                //        db.SaveChanges();
                //        return RedirectToAction("Index");
                //    }

                //}
                return View(recruiter);
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
     
        }
        public ActionResult PostAJob()
        {
            if (Session["r_no"] != null)
            {
                return RedirectToAction("Create", "Jobs");
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
                
        }

        public ActionResult MyJob()
        {
            HiredHuntersEntities1 db = new HiredHuntersEntities1();
            if (Session["r_no"] != null)
            {
                var jobs = db.Jobs.Include(j => j.Recruiter);
                return View(jobs.ToList());
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
            
        }

        public ActionResult JobDetails(int? id)
        {
            if (Session["r_no"] != null)
            {
                 HiredHuntersEntities1 db = new HiredHuntersEntities1();
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
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }

        }
        public ActionResult applicantDetails(int? id)
        {

            if (Session["r_no"] != null)
            {
                HiredHuntersEntities1 db = new HiredHuntersEntities1();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Applylist apply = db.Applylists.Where(x => x.Job_ID == id).FirstOrDefault();
                if (apply == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index","Applist");
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
        }
        
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogOut()
        {
            if (Session["r_no"] != null)
            {
                Session["r_no"] = null;
                return RedirectToAction("Login_Index", "Home");
            }
            else
            {
                return RedirectToAction("Login_Index", "Home");
            }
           // return View();
        }

       public void Approve(int?id)
        {
            if (Session["r_no"] != null)
            {

                HiredHuntersEntities1 db = new HiredHuntersEntities1();
                Applylist rec = db.Applylists.Find(id);
                rec.isgiven = 1;
                db.SaveChanges();
            }
            
        }
        //search Freelancer
        //HiredHuntersEntities1 db = new HiredHuntersEntities1();
        //public ActionResult Search(string searching)
        //{
        //    db.Freelencers.Where(x => x.username.Contains(searching) || searching == null);
        //    return View();
        //}

    }
}