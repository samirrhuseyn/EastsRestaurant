﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using DataAccessLayer.Concrete;
using WebUI.Dtos.OrderDetailsDtos;
using WebUI.Dtos.OrderDtos;
using WebUI.Dtos.ProductDtos;
using System.Data;
using System.Text;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin,Manager,Moderator,")]
    public class OrderDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5056/api/OrderDetails?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderDetailDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateDetail()
        {
            var clientProduct = _httpClientFactory.CreateClient();
            var responseMessageProduct = await clientProduct.GetAsync("http://localhost:5056/api/Product");
            var jsondataProduct = await responseMessageProduct.Content.ReadAsStringAsync();
            var valuesProduct = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsondataProduct);
            List<SelectListItem> valueProduct = (from x in valuesProduct.Where(x=>x.ProductStatus is true)
                                          select new SelectListItem
                                          {
                                              Text = x.ProductName,
                                              Value = x.ProductId.ToString()
                                          }).ToList();
            ViewBag.ProductValues = valueProduct;

            var clientOrder = _httpClientFactory.CreateClient();
            var responseMessageOrder = await clientOrder.GetAsync("http://localhost:5056/api/Orders");
            var jsondataOrder = await responseMessageOrder.Content.ReadAsStringAsync();
            var valuesOrder = JsonConvert.DeserializeObject<List<ResultOrderDto>>(jsondataOrder);
            List<SelectListItem> valueOrder = (from x in valuesOrder.Where(x=>x.Description == "Custumer at the table")
                                               select new SelectListItem
                                                 {
                                                     Text = x.MenuTableName,
                                                     Value = x.OrderID.ToString()
                                                 }).ToList();
            ViewBag.OrderValues = valueOrder;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createOrderDetailDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5056/api/OrderDetails", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Order");
            }
            return View();
        }

        public async Task<IActionResult> DeleteDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5056/api/OrderDetails/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Order");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrderDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5056/api/OrderDetails/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateOrderDetailDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateOrderDetailDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responsiveMessage = await client.PutAsync("http://localhost:5056/api/OrderDetails", stringContent);
            if (responsiveMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Order");
            }
            return View();
        }
    }
}
