using System;
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
    public class EmployeesController : Controller
    {
        private pharmacyEntities db = new pharmacyEntities();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.employees.Include(e => e.department).Include(e => e.designation).Include(e => e.employee2).Include(e => e.user);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.departmentId = new SelectList(db.departments, "departmentId", "name");
            ViewBag.designationId = new SelectList(db.designations, "designationId", "designation1");
            ViewBag.managerId = new SelectList(db.employees, "empId", "firstName");
            ViewBag.userName = new SelectList(db.users, "userName", "password");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empId,firstName,birthDate,lastName,gender,mobile,remarks,deleted,address,joiningDate,designationId,departmentId,managerId,userName")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departmentId = new SelectList(db.departments, "departmentId", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.designations, "designationId", "designation1", employee.designationId);
            ViewBag.managerId = new SelectList(db.employees, "empId", "firstName", employee.managerId);
            ViewBag.userName = new SelectList(db.users, "userName", "password", employee.userName);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentId = new SelectList(db.departments, "departmentId", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.designations, "designationId", "designation1", employee.designationId);
            ViewBag.managerId = new SelectList(db.employees, "empId", "firstName", employee.managerId);
            ViewBag.userName = new SelectList(db.users, "userName", "password", employee.userName);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empId,firstName,birthDate,lastName,gender,mobile,remarks,deleted,address,joiningDate,designationId,departmentId,managerId,userName")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departmentId = new SelectList(db.departments, "departmentId", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.designations, "designationId", "designation1", employee.designationId);
            ViewBag.managerId = new SelectList(db.employees, "empId", "firstName", employee.managerId);
            ViewBag.userName = new SelectList(db.users, "userName", "password", employee.userName);
            return View(employee);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
