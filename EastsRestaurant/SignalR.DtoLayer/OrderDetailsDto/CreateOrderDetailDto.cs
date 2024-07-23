﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.OrderDetailsDto
{
    public class CreateOrderDetailDto
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderID { get; set; }
    }
}
