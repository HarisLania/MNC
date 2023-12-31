﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MNC.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public int ItemId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
    }
}