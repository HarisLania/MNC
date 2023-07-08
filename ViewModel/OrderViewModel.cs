using MNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNC.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public Item Item { get; set; }
    }
}