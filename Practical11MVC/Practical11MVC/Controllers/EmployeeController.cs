using Practical11MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Practical11MVC.Controllers
{


    public class EmployeeController : Controller
    {

        private static List<Employee> employee = new List<Employee>();
        // GET: Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,DOB,Address,Salary")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                if (employee.Count == 0)
                {
                    emp.id = 1;
                    employee.Add(emp);

                }
                else
                {
                    emp.id = employee.Max(x => x.id) + 1;
                    employee.Add(emp);
                }

                return RedirectToAction("Index");
            }


            return View();
        }


        public ActionResult Index()
        {

            return View(employee);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
                
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee emp = employee.Find(x => x.id == id);

            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {

            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Employee emp1 = employee.Find(x => x.id == emp.id);
            emp1.Address = emp.Address;
            emp1.Name = emp.Name;
            emp1.Salary = emp.Salary;

            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            Employee emp1 = employee.Find(x => x.id == id);
            if (emp1 == null)
            {

                return HttpNotFound();


            }

            return View(emp1);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmedDelete(int id)
        {
            Employee emp1 = employee.Find(x=>x.id==id);
            employee.Remove(emp1);  
            
            return RedirectToAction("Index");
        }

    }
}