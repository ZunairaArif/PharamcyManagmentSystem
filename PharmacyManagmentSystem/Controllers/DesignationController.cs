﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;

namespace PharmacyManagmentSystem.Controllers
{
    public class DesignationController : Controller
    {
        private pharmacyEntities db = new pharmacyEntities();

        // GET: /Designation/
        public ActionResult Index()
        {
            var designations = db.designations.Include(d => d.designation2);
            return View(designations.ToList());
        }

        // GET: /Designation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            designation designation = db.designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // GET: /Designation/Create
        public ActionResult Create()
        {
            ViewBag.parentDesignationId = new SelectList(db.designations, "designationId", "designation1");
            return View();
        }

        // POST: /Designation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="designationId,designation1,level,parentDesignationId")] designation designation)
        {
            if (ModelState.IsValid)
            {
                db.designations.Add(designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parentDesignationId = new SelectList(db.designations, "designationId", "designation1", designation.parentDesignationId);
            return View(designation);
        }

        // GET: /Designation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            designation designation = db.designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentDesignationId = new SelectList(db.designations, "designationId", "designation1", designation.parentDesignationId);
            return View(designation);
        }

        // POST: /Designation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="designationId,designation1,level,parentDesignationId")] designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parentDesignationId = new SelectList(db.designations, "designationId", "designation1", designation.parentDesignationId);
            return View(designation);
        }

        
    }
}
