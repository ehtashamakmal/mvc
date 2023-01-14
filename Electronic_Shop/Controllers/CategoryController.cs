using Electronic_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Shop.Controllers
{
    public class CategoryController : Controller
    {
        MVCPROJEntities9 db = new MVCPROJEntities9();
        // GET: Category
        public ActionResult Index()
        {
            var data = db.Categories.ToList();
            return View(data);
        }


        public ActionResult Category()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Category(Category c)
        {

            db.Categories.Add(c);
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["CreateMessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                ModelState.Clear();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                TempData["CreateMessage"] = "<script>alert('Data not Inserted')</script>";
            }
            return View();
        }



        public ActionResult Edit(int id)
        {
            var data = db.Categories.Where(model => model.Cat_Id == id).FirstOrDefault();

            return View(data);


        }

        [HttpPost]

        public ActionResult Edit(Category c)
        {
            var data = db.Categories.Where(model => model.Cat_Id == c.Cat_Id).FirstOrDefault();
            if (data != null)
            {
                data.Cat_Name = c.Cat_Name;
              
                int a = db.SaveChanges();
                if (a > 0)
                {

                    TempData["UpdateMessage"] = "<script>alert('Data Updated Succesfully')</script>";
                    ModelState.Clear();

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                }
            }
            return View();

        }
        public ActionResult Delete(int id)
        {
            var data = db.Categories.Where(model => model.Cat_Id == id).FirstOrDefault();
            db.Categories.Remove(data);
            int a = db.SaveChanges();
            if (a > 0)
            {

                TempData["DeleteMessage"] = "<script>alert('Data Deleted Succesfully')</script>";
                ModelState.Clear();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Data not Deleted')</script>";
            }

            return View(data);
        }

        public ActionResult Details(int id)
        {
            var CategoryRow = db.Categories.Where(model => model.Cat_Id == id).FirstOrDefault();


            return View(CategoryRow);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}