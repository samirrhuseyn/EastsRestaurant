﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using WebUI.Dtos.BasketDto;
using System.Text;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int id)
        {
            var context = new SignalRContext();
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5056/api/Basket/BasketListByMenuTableWithProductName?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> MenuTableList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5056/api/Basket/GetBasketByMenuTableGroupBy");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> DeleteBasket(int id)
        {
            var context = new SignalRContext();
            var menuTableID = context.Baskets.Where(x=>x.BasketID == id).Select(x=>x.MenuTableID).FirstOrDefault();
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5056/api/Basket/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return LocalRedirect("/Baskets/Index/" + menuTableID);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBasket(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5056/api/Basket/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBasketDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(UpdateBasketDto updateBasketDto)
        {
            using var context = new SignalRContext();
            var value = context.Baskets.Where(x => x.BasketID == updateBasketDto.BasketID).FirstOrDefault();
            updateBasketDto.ProductID = value.ProductID;
            updateBasketDto.MenuTableID = value.MenuTableID;
            updateBasketDto.BasketID = value.BasketID;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBasketDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responsiveMessage = await client.PutAsync("http://localhost:5056/api/Basket", stringContent);
            if (responsiveMessage.IsSuccessStatusCode)
            {
                return LocalRedirect("/Baskets/Index/" + value.MenuTableID);
            }
            return View();
        }
    

        public IActionResult DeleteBaskets(int id)
        {
            var context = new SignalRContext();
            var value = context.Baskets.Where(x => x.MenuTableID == id);
            context.BulkDelete(value);
            context.BulkSaveChanges();
            return RedirectToAction("MenuTableList");
        }
    }
}
