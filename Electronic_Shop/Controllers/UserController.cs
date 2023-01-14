using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Shop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }

        }

      

        public ActionResult Product()
        {
            return RedirectToAction("Index", "Product");
        }


        public ActionResult Employee()
        {
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Customer()
        {
            return RedirectToAction("Index", "Customer");
        }


        public ActionResult Category()
        {
            return RedirectToAction("Index", "Category");
        }



        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


    }
}