﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.ContactDto
{
    public class CreateContactDto
    {
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string FooterDescription { get; set; }
        public string LocationIframe { get; set; }
        public string ProjectTitle { get; set; }
        public string LogoImage { get; set; }
    }
}
