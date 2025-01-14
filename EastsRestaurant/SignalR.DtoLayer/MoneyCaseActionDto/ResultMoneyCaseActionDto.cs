﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.MoneyCaseActionDto
{
    public class ResultMoneyCaseActionDto
    {
        public int ActionID { get; set; }
        public decimal ActionAmount { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public bool ActionType { get; set; }
    }
}
