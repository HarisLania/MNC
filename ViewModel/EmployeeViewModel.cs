using MNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNC.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public Department Department { get; set; }
    }
}