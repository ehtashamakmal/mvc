using Electronic_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Shop.Controllers
{
    public class EmployeeController : Controller
    {
        MVCPROJEntities7 db = new MVCPROJEntities7();
        // GET: Employee
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "name")
            {
                var data = db.Employees.Where(model => model.Employee_Name.StartsWith(search)).ToList();
                return View(data);
            }
            else if (searchBy == "cnic")
            {
                var data = db.Employees.Where(model => model.Employee_CNIC == search).ToList();
                return View(data);
            }
            else
            {
                var data = db.Employees.ToList();
                return View(data);
            }
            //    var data = db.Employees.ToList();
            //  return View(data);
        }



        public ActionResult CreateEmployee()
        {


            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee p)
        {
            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(p.Image.FileName);
                string extension = Path.GetExtension(p.Image.FileName);
                HttpPostedFileBase postedFile = p.Image;
                int length = postedFile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        p.EmployeeImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        p.Image.SaveAs(fileName);
                        db.Employees.Add(p);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                            ModelState.Clear();

                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data not Inserted')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image size should be less than 1 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                }
            }

            return View();
        }

        public ActionResult Edit(int id)
        {

            var ProductRow = db.Employees.Where(model => model.Employee_Id == id).FirstOrDefault();
            Session["Image"] = ProductRow.Image;
            return View(ProductRow);

        }


        [HttpPost]
        public ActionResult Edit(Employee p)
        {
            if (ModelState.IsValid == true)
            {
                if (p.Image != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(p.Image.FileName);
                    string extension = Path.GetExtension(p.Image.FileName);
                    HttpPostedFileBase postedFile = p.Image;
                    int length = postedFile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            p.EmployeeImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            p.Image.SaveAs(fileName);
                            db.Entry(p).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {

                                TempData["UpdateMessage"] = "<script>alert('Data Updated Succesfully')</script>";
                                ModelState.Clear();

                                return RedirectToAction("Index", "Employee");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image size should be less than 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }


                }

                else
                {
                    // p.ProdImage = Session["Image"].ToString();
                    db.Entry(p).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Succesfully')</script>";
                        ModelState.Clear();

                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                    }



                }

               

            }

            return View();

        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {

                var productrow = db.Employees.Where(model => model.Employee_Id == id).FirstOrDefault();

                if (productrow != null)
                {
                    db.Entry(productrow).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Deleted Successfully')</script>";
                        string ImagePath = Request.MapPath(productrow.EmployeeImage.ToString());
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }

                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data  Not Deleted')</script>";
                    }
                }
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Data  Not Deleted')</script>";
            }


            return RedirectToAction("Index", "Employee");


        }

        public ActionResult Details(int id)
        {
            var ProductRow = db.Employees.Where(model => model.Employee_Id == id).FirstOrDefault();
            Session["Image2"] = ProductRow.EmployeeImage.ToString();

            return View(ProductRow);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}