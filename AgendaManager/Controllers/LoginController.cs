using AgendaManager.Bl.Dto;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AgendaManager.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgendaManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase { 

        private readonly IJwtService _jwtService;

        private readonly IUserService _userService;
        
        public LoginController(IJwtService jwtService, IUserService userService )
        { 
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login(LoginDto user)
        {
            
            var token = await _jwtService.GetToken(user);
            return token;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create( UserDto user )
        {
            return Ok(await _userService.Create(user));
        }

        [HttpGet("Test")]
        public string[] Test( )
        {
            var id = this.User.FindFirst(x => x.Type == "UserId").Value;
            var name = this.User.FindFirst(x => x.Type == "UserName").Value;
            return new string[] {
                id,
                name
            };
        }
    }
}
