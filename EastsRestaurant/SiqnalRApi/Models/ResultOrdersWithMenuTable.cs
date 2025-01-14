﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ResultOrdersWithMenuTable
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string MenuTableName { get; set; }
    }
}
