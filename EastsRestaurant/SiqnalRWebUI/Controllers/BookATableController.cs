﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using WebUI.Dtos.BookingDtos;
using WebUI.Dtos.NotificationDtos;
using System.Text;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class BookATableController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookATableController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateBookingDto createBookingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5056/api/Booking", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                Notification notification = new Notification()
                {
                    NotificationDescription = "There is a new reservation!",
                    Date = DateTime.Now.ToString("MMMM dd, yyyy HH:mm"),
                    NotificationTypeColor = "info",
                    NotificationTypeIcon = "las la-calendar-plus",
                    Status = false
                };
                var context = new SignalRContext();
                context.Notifications.Add(notification);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
