using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Travelore.DTOs;
using Travelore.Model;

namespace Travelore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _config;
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ICustomerRepository customerRepository,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = await GetTokenAsync(user);
                    return Created("", token); //return het token                
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            Customer customer = new Customer { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "user"));
            }

            if (result.Succeeded)
            {
                _customerRepository.Add(customer);
                _customerRepository.SaveChanges();
                string token = await GetTokenAsync(user);
                return Created("", token);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        private async Task<string> GetTokenAsync(IdentityUser user)
        {
            var roleClaims = await _userManager.GetClaimsAsync(user);
            // Create the token
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            claims.AddRange(roleClaims);
            // de secret
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            // de signing credentials, verwacht de secret en het algoritme
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // aanmaken van het token
            var token = new JwtSecurityToken(
              null, // issuer
              null, // audience
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}