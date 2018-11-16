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
    [Route("api/Room")]
    //[ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomManager _roomService;
        IMapper _mapper;

        public RoomController(IRoomManager roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [Produces(typeof(List<Room>))]
        public async Task<IActionResult> GetAsync()
        {
            var res = await _roomService.GetRooms();
            var mapped = _mapper.Map<List<Room>>(res);
            return Ok(mapped);
        }

        [HttpGet("list/{id}", Name = "GetRoomById")]
        [Produces(typeof(Room))]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var res = await _roomService.GetRoomById(id);
            if(res == null)
            {
                return NotFound("Room not found");
            }

            var mapped = _mapper.Map<Room>(res);
            return Ok(mapped);
        }

        [HttpPost("create/kitchen")]
        public async Task<IActionResult> CreateKitchen(Kitchen k) 
        {
            var mapped = _mapper.Map<DAL.Entities.Kitchen>(k);

            await _roomService.AddRoom(mapped);

            return CreatedAtRoute("GetRoomById", new { id = mapped.Id }, mapped);
        }

        [HttpPost("create/bedroom")]
        public async Task<IActionResult> CreateBedroom(Bedroom br)
        {
            var mapped = _mapper.Map<DAL.Entities.Bedroom>(br);

            await _roomService.AddRoom(mapped);

            return CreatedAtRoute("GetRoomById", new { id = mapped.Id }, mapped);
        }

        [HttpPost("create/shower")]
        public async Task<IActionResult> CreateShower(Shower s)
        {
            var mapped = _mapper.Map<DAL.Entities.Shower>(s);

            await _roomService.AddRoom(mapped);

            return CreatedAtRoute("GetRoomById", new { id = mapped.Id }, mapped);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Room item)
        {
            var mapped = _mapper.Map<DAL.Entities.Room>(item);

            if(await _roomService.UpdateRoom(id, mapped) == null)
            {
                return NotFound("Room not found");
            }

            return Ok("Successful update");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var deleted = await _roomService.GetRoomById(id);
            if (deleted == null)
            {
                return NotFound("Room not found");
            }

            await _roomService.DeletRoom(deleted);

            return Ok("Successful delete");
        }
    }
}