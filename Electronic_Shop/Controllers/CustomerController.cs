using Electronic_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Shop.Controllers
{
    public class CustomerController : Controller
    {

        MVCPROJEntities8 db = new MVCPROJEntities8();
        // GET: Customer
        public ActionResult Index(string searchBy, string search)
        {
            if(searchBy== "Name")
            {
                var mydata = db.Customers.Where(model => model.Cust_Name.StartsWith(search)).ToList();
                return View(mydata);
            }
            else if(searchBy == "Gender")
            {
                var mydata = db.Customers.Where(model => model.Gender == search).ToList();
                return View(mydata);
            }
            else
            {
                var mydata = db.Customers.ToList();
                return View(mydata);
            }
            //var data = db.Customers.ToList();
           // return View(data);
        }

        //Get
        public ActionResult Customer()
        {
           
            return View();
        }


        [HttpPost]
        public ActionResult Customer(Customer c)
        {
            db.Customers.Add(c);
           int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["CreateMessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                ModelState.Clear();

                return RedirectToAction("Index", "Customer");
            }
            else
            {
                TempData["CreateMessage"] = "<script>alert('Data not Inserted')</script>";
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = db.Customers.Where(model => model.Cust_Id == id).FirstOrDefault();

            return View(data);
        }


        [HttpPost]

        	public ActionResult Edit(Customer c)
            {  
                 var data = db.Customers.Where(model=> model.Cust_Id== c.Cust_Id ).FirstOrDefault();  
                 if (data != null)  
	        {  
                data.Cust_Name= c.Cust_Name;
                data.Cust_City = c.Cust_City;
                data.Gender = c.Gender;
                int a = db.SaveChanges();
                if (a > 0)
                {

                    TempData["UpdateMessage"] = "<script>alert('Data Updated Succesfully')</script>";
                    ModelState.Clear();

                    return RedirectToAction("Index", "Customer");
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
            var data = db.Customers.Where(model => model.Cust_Id == id).FirstOrDefault();
            db.Customers.Remove(data);
           int a =  db.SaveChanges();
            if (a > 0)
            {

                TempData["DeleteMessage"] = "<script>alert('Data Deleted Succesfully')</script>";
                ModelState.Clear();

                return RedirectToAction("Index", "Customer");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Data not Deleted')</script>";
            }

            return View(data);
        }


        public ActionResult Details(int id)
        {
            var CustomerRow = db.Customers.Where(model => model.Cust_Id == id).FirstOrDefault();
           

            return View(CustomerRow);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}