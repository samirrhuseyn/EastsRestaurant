﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin,Manager,Moderator,")]
    public class QRCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator createQRCode = new QRCodeGenerator();
                QRCodeGenerator.QRCode squareCode = createQRCode.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap image = squareCode.GetGraphic(10))
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    ViewBag.QrCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }
    }
}