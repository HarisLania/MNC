using MNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNC.ViewModel
{
    public class EmployeeCreateViewModel
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { get; set; }
    }
}