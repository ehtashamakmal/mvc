using Electronic_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Shop.Controllers
{
    public class LoginController : Controller
    {
        MVCPROJEntities db = new MVCPROJEntities();

        // GET: Login
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Signup(User u)
        {

            if(ModelState.IsValid == true)
            {
                db.Users.Add(u);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.InsertMessage = "<script>alert('Registered SuccesFully  ! !')</script>";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Registered Failed  ! !')</script>";
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            var user = db.Users.Where(model => model.username == u.username && model.password == u.password).FirstOrDefault();
            if(user != null)
            {
                Session["UserId"] = u.Id.ToString();
                Session["Username"] = u.username.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('Login SuccessFully')</script>";
                return RedirectToAction("Index", "User");
            }

            else
            {
                ViewBag.ErrorMessage = "<script>alert('Username or password incorrect')</script>";
                return View();
            }
           
        }
    }
}