﻿using Microsoft.AspNetCore.Http;

namespace WebUI.Dtos.TestimonialDtos
{
    public class UpdateTestimonial
    {
        public int TestimonialID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public IFormFile ImageURL { get; set; }
        public bool Status { get; set; }
    }
}
