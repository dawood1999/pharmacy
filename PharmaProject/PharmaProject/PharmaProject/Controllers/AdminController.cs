using PharmaProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PharmaProject.Controllers
{
    public class AdminController : Controller
    {
        private pharmadbEntities db = new pharmadbEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(adlogin admin,string ReturnUrl)
        {
            if (admin.Username == "" && admin.Password=="" )
            {
                ViewBag.msg1 = "<script>alert('All fields are required');</script>";
                
            }
            var login = db.adlogin.Where(a => a.Username == admin.Username && a.Password == admin.Password).FirstOrDefault();
            if (login!=null)
            {
                FormsAuthentication.SetAuthCookie(login.Username, false);
                Session["admin"] = login.Username;
                if (ReturnUrl!=null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Create","Home");
                }


            }
            else
            {
                ViewBag.msg = "<script>alert('Invalid Credential');</script>";
            }
            return View();
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Create","Home");
        }
        
    }
}