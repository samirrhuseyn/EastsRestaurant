﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using DataAccessLayer.Concrete;
using SiqnalR.EntityLayer.Entities;
using WebUI.Dtos.CategoryDtos;
using WebUI.Dtos.ProductDtos;
using System.Data;
using System.Text;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin,Manager,Moderator,")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        SignalRContext context = new SignalRContext();
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5056/api/Product/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>  CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5056/api/Category");
            var jsondata = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsondata);
            List<SelectListItem> value = (from x in values
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString()
                                          }).ToList();
            ViewBag.Values = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            CreateProductDto createProductDto = new CreateProductDto()
            {
                ImageURL = UploadFile(createProduct.ImageURL),
                Description = createProduct.Description,
                Price = createProduct.Price,
                ProductName = createProduct.ProductName,
                ProductStatus = true,
                CategoryID = createProduct.CategoryID
            };
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5056/api/Product", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5056/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var image = context.Products.Where(x => x.ProductId == id).Select(x => x.ImageURL).FirstOrDefault();
            ViewBag.Image = image;
            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("http://localhost:5056/api/Category");
            var jsondata = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsondata);
            List<SelectListItem> value = (from x in values1
                                         select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString()
                                          }).ToList();
            ViewBag.Values = value;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5056/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            var image = context.Products.Where(x => x.ProductId == updateProduct.ProductId).Select(x => x.ImageURL).FirstOrDefault();
            UpdateProductDto updateProductDto = new UpdateProductDto()
            {
                ProductId = updateProduct.ProductId,
                ProductName = updateProduct.ProductName,
                ProductStatus = updateProduct.ProductStatus,
                CategoryID = updateProduct.CategoryID,
                Description = updateProduct.Description,
                Price = updateProduct.Price,
                ImageURL = updateProduct.ImageURL != null ? UploadFile(updateProduct.ImageURL) : image
            };
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responsiveMessage = await client.PutAsync("http://localhost:5056/api/Product", stringContent);
            if (responsiveMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult FalseStatus(int id)
        {
            var context = new SignalRContext();
            var value = context.Set<Product>().Find(id);
            value.ProductStatus = false;
            context.Update(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ActiveStatus(int id)
        {
            var context = new SignalRContext();
            var value = context.Set<Product>().Find(id);
            value.ProductStatus = true;
            context.Update(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Image/ProductImage/",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Image/ProductImage/" + file.FileName;
        }
    }
}
