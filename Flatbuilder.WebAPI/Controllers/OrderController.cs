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

        [HttpGet("get/{name}")]
        [Produces(typeof(List<Order>))]
        public async Task<IActionResult> GetAsyncByName(string name)
        {
            var res = await _orderService.GetOrdersByName(name);
            if (res == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<List<Order>>(res);
            return Ok(mapped);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var res = await _orderService.GetOrderById(id);
            if(res == null)
            {
                return NotFound("Order not found!");
            }
            var mapped = _mapper.Map<Order>(res);
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(Order o)
        {

           var mappedorder = _mapper.Map<DAL.Entities.Order>(o);

            //teszthez
            //o = new Order
            //{
            //    Costumer = new Costumer { Name = "Name" },
            //    StartDate = DateTime.Now.AddDays(-1),
            //    EndDate = DateTime.Now.AddDays(1),
            //    Rooms = new List<Room>
            //    {
            //       new Kitchen(){ Price = 100  },
            //       new Bedroom(){ Price = 100  },
            //       new Shower(){ Price = 100  }
            //    }

            //};

            //var mappedorder = new DAL.Entities.Order
            //{
            //    Costumer = new DAL.Entities.Costumer { Name = o.Costumer.Name },
            //    StartDate = o.StartDate,
            //    EndDate = o.EndDate
            //};

            List<DAL.Entities.Room> mappedrooms = new List<DAL.Entities.Room>();

            foreach (var r in o.Rooms)
            {
                if (r.GetType() == typeof(Kitchen))
                    mappedrooms.Add(_mapper.Map<DAL.Entities.Kitchen>(r));
                else if (r.GetType() == typeof(Bedroom))
                    mappedrooms.Add(_mapper.Map<DAL.Entities.Bedroom>(r));
                else if (r.GetType() == typeof(Shower))
                    mappedrooms.Add(_mapper.Map<DAL.Entities.Shower>(r));
            }

            var neworder = await _orderService.AddOrder(mappedorder,mappedrooms);
            if(neworder == null)
            {
                return NotFound("No free room in the time interval");
            }

            var mappedneworder = _mapper.Map<Order>(neworder);

            return CreatedAtRoute("GetOrderById", new { id = mappedneworder.Id }, mappedneworder);
        }

        [HttpGet]
        public async Task<IActionResult> InsertAsync()
        {
            await _orderService.InsertAsync();
            return Ok();
        }
    }
}