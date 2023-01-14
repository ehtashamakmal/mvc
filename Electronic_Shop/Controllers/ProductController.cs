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
    public class ProductController : Controller
    {
        MVCPROJEntities5 db = new MVCPROJEntities5();
        // GET: Product
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Name")
            {
                var data = db.Products.Where(model => model.Product_Name.StartsWith(search)).ToList();
                return View(data);
            }
            else if (searchBy == "Type")
            {
                var data = db.Products.Where(model => model.Product_Type == search).ToList();
                return View(data);
            }
            else
            {
                var data = db.Products.ToList();
                return View(data);
            }
            // var data = db.Products.ToList();
            //return View(data);
        }

        public ActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ProductCreate(Product p)
        {

            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                string extension = Path.GetExtension(p.ImageFile.FileName);
                HttpPostedFileBase postedFile = p.ImageFile;
                int length = postedFile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        p.ProdImage = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        p.ImageFile.SaveAs(fileName);
                        db.Products.Add(p);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                            ModelState.Clear();

                            return RedirectToAction("Index", "Product");
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


        public ActionResult Edit( int id)
        {

            var ProductRow = db.Products.Where(model => model.Product_Id == id).FirstOrDefault();
            Session["Image"] = ProductRow.ImageFile;
            return View(ProductRow);
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {

            if (ModelState.IsValid == true)
            {
                if (p.ImageFile != null )
                {

                    string fileName = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                    string extension = Path.GetExtension(p.ImageFile.FileName);
                    HttpPostedFileBase postedFile = p.ImageFile;
                    int length = postedFile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            p.ProdImage = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            p.ImageFile.SaveAs(fileName);
                            db.Entry(p).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                              
                                TempData["UpdateMessage"] = "<script>alert('Data Updated Succesfully')</script>";
                                ModelState.Clear();

                                return RedirectToAction("Index", "Product");
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

                        return RedirectToAction("Index", "Product");
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

                var productrow = db.Products.Where(model => model.Product_Id == id).FirstOrDefault();

                if(productrow != null)
                {
                    db.Entry(productrow).State = EntityState.Deleted;
                   int a =  db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Deleted Successfully')</script>";
                        string ImagePath = Request.MapPath(productrow.ProdImage.ToString());
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



            return RedirectToAction("Index", "Product");
        }


        public ActionResult Details(int id)
        {
            var ProductRow = db.Products.Where(model => model.Product_Id == id).FirstOrDefault();
            Session["Image2"] = ProductRow.ProdImage.ToString();

            return View(ProductRow);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}