using Practical11MVC.Models;
using Practical11MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Practical11MVC.Controllers
{
    public class Test2Controller : Controller
    {

        private static List<Employee> employees = new List<Employee>();
        // GET: Test2
      public ActionResult Index()
        {
            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Create() {

            ViewBag.name = "Create";
            TempData["name"] = "Create";

            return View("Index");
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create([Bind(Include = "id,Name,DOB,Address,Salary")] Employee emp) {
            
            if (!ModelState.IsValid)
            {
                ViewBag.name = "Create";
                TempData["name"] = "Create";
                return View("Index");
            }

            if (employees.Count == 0)
            {
                emp.id = 1;
                employees.Add(emp);

            }
            else
            {
                emp.id = employees.Max(x => x.id) + 1;
                employees.Add(emp);
            }
            //ViewBag.name = "Details";
            TempData["name"] = "Details";
            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };
            return RedirectToAction("Index", viewModel);
        }


        public ActionResult Details()
        {
            ViewBag.name = "Details";
            TempData["name"] = "Details";
            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };
            return View("Index", viewModel);

        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            Employee emp = employees.Find(x => x.id == id);

            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var viewModel = new EmployeeViewModel
            {
                Employee = emp
            };
            TempData["name"] = "Edit";

            return View("_Edit",viewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit(int id, EmployeeViewModel empViewModel)
        {
        
            if (empViewModel.Employee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Employee emp1 = employees.Find(x => x.id == empViewModel.Employee.id);
            emp1.Address = empViewModel.Employee.Address;
            emp1.Name = empViewModel.Employee.Name;
            emp1.Salary = empViewModel.Employee.Salary;

            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };
            TempData["name"] = "Details";
            return RedirectToAction("Index", viewModel);
        }

        public ActionResult Delete(int id)
        {
           // ViewBag.name = "Delete";
            TempData["name"] = "Delete";
            Employee emp1 = employees.Find(x => x.id == id);
            if (emp1 == null)
            {

                return HttpNotFound();


            }

            return View("_delete",emp1);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmedDelete(int id)
        {
           // ViewBag.name = "Delete";
            TempData["name"] = "Details";

            Employee emp1 = employees.Find(x => x.id == id);
            employees.Remove(emp1);
            var viewModel = new EmployeeViewModel
            {
                Employees = employees
            };

            return RedirectToAction("Index",viewModel);
        }



    }
}