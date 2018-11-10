using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flatbuilder.DAL.Interfaces;
using AutoMapper;
using Flatbuilder.DTO;

namespace Flatbuilder.WebAPI.Controllers
{
    [Route("api/Order")]
//    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderService;
        IMapper _mapper;

        public OrderController(IOrderManager orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [Produces(typeof(List<Order>))]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _orderService.GetOrders();
            var mapped = _mapper.Map<List<Order>>(res);
            return Ok(mapped);
        }

        [HttpGet("list/{name}")]
        [Produces(typeof(List<Order>))]
        public async Task<IActionResult> GetAsyncByName(String name)
        {
            var res = await _orderService.GetOrders(name);
            if (res == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<List<Order>>(res);
            return Ok(mapped);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.GetOrderById(id);
            if (deleted == null)
            {
                return NotFound("Order not found");
            }

            await _orderService.DeleteOrder(deleted);

            return Ok("Successful delete");
        }

        [HttpGet]
        public async Task<IActionResult> InsertAsync()
        {
            await _orderService.InsertAsync();
            return Ok();
        }
    }
}