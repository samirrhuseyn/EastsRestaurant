﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.OrderDto
{
    public class ResultOrderDto
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
