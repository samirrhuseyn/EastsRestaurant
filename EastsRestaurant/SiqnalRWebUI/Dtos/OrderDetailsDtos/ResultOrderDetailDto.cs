﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Dtos.OrderDetailsDtos
{
    public class ResultOrderDetailDto
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
