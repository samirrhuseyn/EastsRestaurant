﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.SlideDto
{
    public class UpdateSlideDto
    {
        public int SliderID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
