﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Models
{
    public class OrderProducts
    {
        public int OrderId { get; set; }
        public Order Orders { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
