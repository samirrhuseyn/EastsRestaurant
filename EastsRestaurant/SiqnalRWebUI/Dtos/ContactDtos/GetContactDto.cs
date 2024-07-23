﻿namespace WebUI.Dtos.ContactDtos
{
    public class GetContactDto
    {
        public int ContactID { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LocationIframe { get; set; }
        public string ProjectTitle { get; set; }
        public string FooterDescription { get; set; }
        public string LogoImage { get; set; }
    }
}
