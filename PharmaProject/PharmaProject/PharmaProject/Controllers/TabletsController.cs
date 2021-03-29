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
    public class TabletsController : Controller
    {
        private pharmadbEntities db = new pharmadbEntities();

        // GET: Tablets
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Tablets.ToList());
        }

        // GET: Tablets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablets tablets = db.Tablets.Find(id);
            if (tablets == null)
            {
                return HttpNotFound();
            }
            return View(tablets);
        }

        // GET: Tablets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tablets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Tablets tablets)
        {


            if (ModelState.IsValid)
            {
                string imagename = Path.GetFileNameWithoutExtension(tablets.Image3.FileName);
                string extension = Path.GetExtension(tablets.Image3.FileName);
                HttpPostedFileBase serverimage = tablets.Image3;
                int size = serverimage.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (size <= 2000000)
                    {
                        imagename = imagename + extension;
                        tablets.photo = "~/tabletfiles/" + imagename;
                        imagename = Path.Combine(Server.MapPath("~/tabletfiles/"), imagename);
                        tablets.Image3.SaveAs(imagename);
                        db.Tablets.Add(tablets);
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
       

        // GET: Tablets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablets tablets = db.Tablets.Find(id);
            if (tablets == null)
            {
                return HttpNotFound();
            }
            return View(tablets);
        }

        // POST: Tablets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Model,Dies,Max_pressure,Max_diameter,Max_depth,Production_capacity,machine_size,net_weight,power,Price_")] Tablets tablets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tablets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tablets);
        }

        // GET: Tablets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablets tablets = db.Tablets.Find(id);
            if (tablets == null)
            {
                return HttpNotFound();
            }
            return View(tablets);
        }

        // POST: Tablets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tablets tablets = db.Tablets.Find(id);
            db.Tablets.Remove(tablets);
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
