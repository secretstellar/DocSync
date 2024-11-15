using DocSync.API.DTOs;
using DocSync.API.Models;
using DocSync.API.Services;
using DocSync.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DocSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var response = await _authService.LoginUser(userDto);
            return Ok(response);
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDetailsDto userDetailsDto)
        {
            var response = await _authService.RegisterUser(userDetailsDto);
            return Ok(response);
        }

    }
}
