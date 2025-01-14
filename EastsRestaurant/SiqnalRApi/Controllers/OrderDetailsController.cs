﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using DtoLayer.OrderDetailsDto;
using EntityLayer.Entities;
using EntityLayer.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetails(CreateOrderDetailDto createOrderDetailDto)
        {
            _orderDetailService.TAdd(new OrderDetail
            {
                OrderID = createOrderDetailDto.OrderID,
                Count = createOrderDetailDto.Count,
                ProductID = createOrderDetailDto.ProductID,
                TotalPrice = createOrderDetailDto.TotalPrice
            });
            return Ok("Addition successfuly");
        }

        [HttpGet]
        public async Task<IActionResult> GetDetail(int id)
        {
            var context = new SignalRContext();
            var value = context.OrderDetails.Where(x => x.OrderID == id).Include(y => y.Product).Select(z => new ResultOrderDetailWithProduct
            {
                ProductName = z.Product.ProductName,
                Count = z.Count,
                OrderDetailID = z.OrderDetailID,
                TotalPrice = z.TotalPrice,
                ProductID = z.ProductID,
                OrderID = z.OrderID,
                ProductPrice = z.Product.Price
            });
            return Ok(value.ToList());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDetail(int id)
        {
            var value = _orderDetailService.TGetByID(id);
            _orderDetailService.TDelete(value);
            return Ok("Order Detail section has been deleted successfully!");
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            _orderDetailService.TUpdate(new OrderDetail
            {
                OrderDetailID = updateOrderDetailDto.OrderDetailID,
                Count = updateOrderDetailDto.Count,
                OrderID = updateOrderDetailDto.OrderID,
                ProductID= updateOrderDetailDto.ProductID,
                TotalPrice = updateOrderDetailDto.TotalPrice
            });
            return Ok("Order Detail section has been successfully updated!");
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var value = _orderDetailService.TGetByID(id);
            return Ok(value);
        }
    }
}
