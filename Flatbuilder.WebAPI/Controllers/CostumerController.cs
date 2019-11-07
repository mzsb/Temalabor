using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Flatbuilder.DAL.Entities.Authentication;
using Flatbuilder.DAL.Interfaces;
using Flatbuilder.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Flatbuilder.WebAPI.Controllers
{
    [Route("api/Customer")]
    //[ApiController]
    public class CostumerController : Controller
    {
        private readonly ICostumerManager _costumerService;
        IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CostumerController(ICostumerManager roomService,
                                  IMapper mapper,
                                  SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager)
        {
            _costumerService = roomService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginInfo)
        {
            //var user = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
            //var login = await _userManager.CreateAsync(user, loginInfo.Password);
            var user = await _userManager.FindByNameAsync(loginInfo.Email);
            var result = await _signInManager.PasswordSignInAsync(user,loginInfo.Password,false,false);

            if (result.Succeeded)
            {
                return Ok(user.Id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("list")]
        [Produces(typeof(List<Costumer>))]
        public async Task<IActionResult> GetCostumersAsync()
        {
            var res = await _costumerService.GetCostumersAsync();
            var mapped = _mapper.Map<List<Costumer>>(res);
            return Ok(mapped);
        }

        [HttpGet("{id}", Name = "GetCostumerById")]
        [Produces(typeof(Costumer))]
        public async Task<IActionResult> GetCostumerByIdAsync(int id)
        {
            var res = await _costumerService.GetCostumerByIdAsync(id);
            if (res == null)
            {
                return NotFound("Costumer not found");
            }

            var mapped = _mapper.Map<Costumer>(res);
            return Ok(mapped);
        }

        [HttpGet("get/{name}", Name = "GetCostumerByName")]
        [Produces(typeof(Costumer))]
        public async Task<IActionResult> GetCostumerByNameAsync(string name)
        {
            var res = await _costumerService.GetCostumerByNameAsync(name);
            if (res == null)
            {
                return NotFound("Costumer not found");
            }

            var mapped = _mapper.Map<Costumer>(res);
            return Ok(mapped);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCostumerAsync([FromBody]Costumer c)
        {
            var mapped = _mapper.Map<DAL.Entities.Costumer>(c);

            await _costumerService.AddCostumerAsync(mapped);

            return CreatedAtRoute("GetCostumerById", new { id = mapped.Id }, mapped);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCostumerAsync(int id, Costumer c)
        {
            var mapped = _mapper.Map<DAL.Entities.Costumer>(c);

            if (await _costumerService.UpdateCostumerAsync(id, mapped) == null)
            {
                return NotFound("Costumer not found");
            }

            return Ok("Successful update");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCostumerAsync(int id)
        {
            var deleted = await _costumerService.GetCostumerByIdAsync(id);
            if (deleted == null)
            {
                return NotFound("Costumer not found");
            }

            await _costumerService.DeletCostumerAsync(deleted);

            return Ok("Successful delete");
        }
    }
}