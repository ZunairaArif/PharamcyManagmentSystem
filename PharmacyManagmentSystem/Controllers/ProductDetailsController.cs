using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;
using PharmacyManagmentSystem.DAL;

namespace PharmacyManagmentSystem.Controllers
{
    public class ProductDetailsController : Controller
    {
        private pharmacyEntities db = new pharmacyEntities();
        PharmacyDAL pdal = new PharmacyDAL();

        // GET: productdetails
        public ActionResult Index()
        {
            var productdetails = db.productdetails.Include(p => p.product);
            return View(productdetails.ToList());
        }

        // GET: productdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productdetail productdetail = db.productdetails.Find(id);
            if (productdetail == null)
            {
                return HttpNotFound();
            }
            return View(productdetail);
        }

        // GET: productdetails/Create
        public ActionResult CreateProductDetails()
        {
           ViewData["Category"] = pdal.GetCategory();
            ViewBag.productId = new SelectList(db.products, "productId", "productName");
            return View();
        }

        public JsonResult GetProduct(string id)
        {
            return Json(pdal.GetProduct(id));
        }

        // POST: productdetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductDetails([Bind(Include = "productDetailId,productId,productSize")] productdetail productdetail)
        {
           
            if (ModelState.IsValid)
            {
                db.productdetails.Add(productdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.products, "productId", "productName", productdetail.productId);
            return View(productdetail);
        }

        // GET: productdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productdetail productdetail = db.productdetails.Find(id);
            if (productdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.products, "productId", "productName", productdetail.productId);
            return View(productdetail);
        }

        // POST: productdetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productDetailId,productId,productSize")] productdetail productdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.products, "productId", "productName", productdetail.productId);
            return View(productdetail);
        }

        public ActionResult AddProduct()
        {
            ViewData["Category"] = pdal.GetCategory();
            ViewData["Supplier"] = pdal.GetSupplier();
            return View();
        }

        public JsonResult ProductDetails(string id, string size, string supplierID)
        {
            int id2 = int.Parse(id);
            int size2 = int.Parse(size);
            int supplierID2 = int.Parse(supplierID);
            pdal.AddNewProductDetails(id2, size2, supplierID2);
            return Json("OK");
        }
        
    }
}
