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
      [Authorize]
    public class capsulesController : Controller
    {
        private pharmadbEntities db = new pharmadbEntities();

        // GET: capsules
          [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.capsule.ToList());
        }

        // GET: capsules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            capsule capsule = db.capsule.Find(id);
            if (capsule == null)
            {
                return HttpNotFound();
            }
            return View(capsule);
        }

        // GET: capsules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: capsules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(capsule capsule)
        {


            if (ModelState.IsValid)
            {
                string imagename = Path.GetFileNameWithoutExtension(capsule.Imageupload.FileName);
                string extension = Path.GetExtension(capsule.Imageupload.FileName);
                HttpPostedFileBase serverimage = capsule.Imageupload;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (size <= 2000000)
                    {
                        imagename = imagename + extension;
                        capsule.pic = "~/capsulefile/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/capsulefile/"), imagename);
                        capsule.Imageupload.SaveAs(imagename);
                        db.capsule.Add(capsule);
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
        // GET: capsules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            capsule capsule = db.capsule.Find(id);
            if (capsule == null)
            {
                return HttpNotFound();
            }
            return View(capsule);
        }

        // POST: capsules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,company_name,output,size,dimension,shipping_weight,Power,Price_")] capsule capsule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capsule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capsule);
        }

        // GET: capsules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            capsule capsule = db.capsule.Find(id);
            if (capsule == null)
            {
                return HttpNotFound();
            }
            return View(capsule);
        }

        // POST: capsules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            capsule capsule = db.capsule.Find(id);
            db.capsule.Remove(capsule);
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
