using Practical11MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical11MVC.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
    }
}