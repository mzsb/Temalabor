using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Flatbuilder.DAL.Entities.Authentication;
using Flatbuilder.DAL.Interfaces;
using Flatbuilder.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
        private readonly IConfiguration _configuration;

        public CostumerController(ICostumerManager roomService,
                                  IMapper mapper,
                                  SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager,
                                  IConfiguration configuration)
        {
            _costumerService = roomService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]LoginDto loginInfo)
        {
            var user = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
            var login = await _userManager.CreateAsync(user, loginInfo.Password);

            if (login.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(login.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginInfo)
        {
            //var user = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
            //var login = await _userManager.CreateAsync(user, loginInfo.Password);
            var user = await _userManager.FindByNameAsync(loginInfo.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginInfo.Password, false, false);

                if (result.Succeeded)
                {
                    var res = await GenerateJwtToken(user);
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
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

        [HttpPost("customerLogin")]
        [Produces(typeof(Costumer))]
        public async Task<IActionResult> loginCustomer([FromBody] Costumer customer)
        {
            var mapped = _mapper.Map<DAL.Entities.Costumer>(customer);

            var c = await _costumerService.GetCostumerByNameAsync(customer.Name);

            PasswordHasher<DAL.Entities.Costumer> passwordHasher = new PasswordHasher<DAL.Entities.Costumer>();
            var result = passwordHasher.VerifyHashedPassword(mapped,c.Password,mapped.Password);

            if(result != PasswordVerificationResult.Failed){
                return Ok(c);
            }
            else
            {
                return BadRequest();
            }
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

            PasswordHasher<DAL.Entities.Costumer> passwordHasher = new PasswordHasher<DAL.Entities.Costumer>();
            var hashed = passwordHasher.HashPassword(mapped, mapped.Password);

            mapped.Password = hashed;

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

        private async Task<object> GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}