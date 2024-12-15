﻿using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EndpointParametersSolution.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Add-User")]
        public IActionResult AddUser([FromBody] UserInputDTO userInputDTO)
        {
            try
            {
                var user = _userService.AddUser(userInputDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpGet("Login")]
        public IActionResult UserLogin(string email, string password)
        {
            var user = _userService.GetUser(email, password);
            if (user == null)
            {
                return BadRequest("Invalid Credentials");
            }
            else
            {
                string token = GenerateJwtToken(user.userId.ToString(), user.userName);
                return Ok(token);
            }
        }

        [NonAction]
        public string GenerateJwtToken(string userId, string userName)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
