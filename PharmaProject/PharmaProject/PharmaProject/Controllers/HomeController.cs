using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmaProject.Models;
using System.IO;



namespace PharmaProject.Controllers
{
    public class HomeController : Controller
    {
        private pharmadbEntities db = new pharmadbEntities();

        // GET: Home
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult Gallery()
        {
        return View();
         }
        public ActionResult Create()
        {
            return View();
        }
       
        public ActionResult about ()
        {
            return View();
        }
        public ActionResult pipline()
        {
            return View();
        }

        public ActionResult job()
        {
            return View();
        }
        [HttpPost]
        public ActionResult job( Job job)
        {


            if (ModelState.IsValid)
            {
                string imagename = Path.GetFileNameWithoutExtension(job.Imageayi.FileName);
                string extension = Path.GetExtension(job.Imageayi.FileName);
                HttpPostedFileBase serverimage = job.Imageayi;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".pdf" || extension.ToLower() == ".doc" || extension.ToLower() == ".docx")
                {
                    if (size <= 32000000)
                    {
                        imagename = imagename + extension;
                        job.Resume = "~/files/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/files/"), imagename);
                        job.Imageayi.SaveAs(imagename);
                        db.Job.Add(job);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.msg = "<script>alert('invalid error')</script>";
                    }
                }
                else
                {
                    ViewBag.msg = "<script>alert('Wrong Extension')</script>";
                }
            }
 
            return View();
        }



        public ActionResult joblist()
        {
            return View(db.Job.ToList());
        }


       


    }
}