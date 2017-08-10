﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtendedModel.Models;

namespace ExtendedModel.Controllers {
    [HandleError(Order = 2)]
    public class ProductController : Controller {
        private NorthwindAccessConsolidator nwa
            = new NorthwindAccessConsolidator();

        //
        // GET: /Product/

        public ActionResult Index() {
            return View(nwa.ListProducts());
        }

        //
        // GET: /Product/Details/5

        //[HandleError(View = "NoSuchRecordError", ExceptionType = typeof(NoSuchRecordException))]
        public ActionResult Details(int id) {
            Product prod = nwa.GetProduct(id);
            if (prod == null) {
                return RedirectToAction("CustomError",
                    new {
                        message = "You requested an unknown product",
                        detail = String.Format("No record for ID of {0}", id)
                    });
            } else {
                ViewData["CatName"] = nwa.GetCategoryName(prod);
                ViewData["SupName"] = nwa.GetSupplierName(prod);
                return View(prod);
            }
        }

        public ActionResult CustomError(string message, string detail) {
            ViewData["ErrorMessage"] = message;
            ViewData["ErrorDetail"] = detail;
            return View("CustomError");
        }

        public ActionResult JsonDetails(int id) {
            Product prod = nwa.GetProduct(id);
            if (prod == null) {
                throw new NoSuchRecordException();
            } else {
                return Json(new {
                    ProductId = prod.ProductID,
                    ProductName = prod.ProductName,
                    SupplierID = prod.SupplierID,
                    CategoryID = prod.CategoryID,
                    UnitPrice = prod.UnitPrice,
                    UnitsInStock = prod.UnitsInStock,
                    UnitsOnOrder = prod.UnitsOnOrder,
                    ReorderLevel = prod.ReorderLevel,
                    Discontinued = prod.Discontinued
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Create

        public ActionResult Create() {
            return View(new Product());
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product prod) {
            try {
                nwa.StoreNewProduct(prod);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id) {

            ViewData["categories"] = new SelectList(nwa.GetAllCategories());
            ViewData["suppliers"] = new SelectList(nwa.GetAllSuppliers());

            Product prod = nwa.GetProduct(id);

            ProductListWrapper wrap = new ProductListWrapper() {
                product = prod,
                SelectedCategory = prod.Category.CategoryName,
                SelectedSupplier = prod.Supplier.CompanyName,

            };
            return View(wrap);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProductListWrapper pwrap) {
            try {
                if (!ModelState.IsValid) {
                    ViewData["categories"] = new SelectList(nwa.GetAllCategories());
                    ViewData["suppliers"] = new SelectList(nwa.GetAllSuppliers());
                    return View("Edit", pwrap);
                }

                Product prod = nwa.GetProduct(id);
                if (prod != null) {
                    ProductListWrapper wrapper = new ProductListWrapper() {
                        product = prod
                    };
                    UpdateModel(wrapper);
                    prod.SupplierID = nwa.GetSupplierID(wrapper.SelectedSupplier);
                    prod.CategoryID = nwa.GetCategoryID(wrapper.SelectedCategory);
                    nwa.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    throw new NoSuchRecordException();
                }
            } catch {
                return View();
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id) {
            return View(nwa.GetProduct(id));
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                nwa.DeleteProduct(id);
                return RedirectToAction("Index");
            } catch (Exception ex) {
                Console.WriteLine(ex);
                return View();
            }
        }
    }

    class NorthwindAccessConsolidator {
        private NorthwindEntities db = new NorthwindEntities();

        public IEnumerable<Product> ListProducts() {
            return db.Products;
        }

        public Product GetProduct(int id) {
            IEnumerable<Product> data = db.Products
                .Where(e => e.ProductID == id)
                .Select(e => e);
            return data.Count() > 0 ? data.Single() : null;
        }

        public void DeleteProduct(int id) {
            Product prod = GetProduct(id);
            if (prod != null) {
                IEnumerable<Order_Detail> ods = 
                    db.Order_Details
                    .Where(e => e.ProductID == id)
                    .Select(e => e);
                foreach (Order_Detail od in ods) {
                    db.Order_Details.DeleteObject(od);
                }
                
                db.Products.DeleteObject(prod);
                SaveChanges();
            }
        }

        public string GetSupplierName(Product prod) {
            return db.Suppliers
                .Where(e => e.SupplierID == prod.SupplierID)
                .Select(e => e.CompanyName)
                .Single();
        }

        public string GetCategoryName(Product prod) {
            return db.Categories
                .Where(e => e.CategoryID == prod.CategoryID)
                .Select(e => e.CategoryName)
                .Single();
        }

        public void StoreNewProduct(Product prod) {
            db.Products.AddObject(prod);
            SaveChanges();
        }

        public void SaveChanges() {
            db.SaveChanges();
        }

        public IEnumerable<string> GetAllSuppliers() {
            return db.Suppliers.Select(e => e.CompanyName);
        }

        public int GetSupplierID(string name) {
            return db.Suppliers
                .Where(e => e.CompanyName == name)
                .Select(e => e.SupplierID).Single();
        }

        public IEnumerable<string> GetAllCategories() {
            return db.Categories.Select(e => e.CategoryName);
        }

        public int GetCategoryID(string name) {
            return db.Categories
                .Where(e => e.CategoryName == name)
                .Select(e => e.CategoryID).Single();
        }
    }

    class NoSuchRecordException : Exception {
    }
}
