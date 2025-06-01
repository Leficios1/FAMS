using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FAMS.Api.Controllers
{
    
    [ApiController]
    
    public class AuthController :BaseApiController
    {
        IMapper _mapper;
        IAuthService _authService;
        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] AuthDto authDto)
        {
            var user = _mapper.Map<AuthDto, User>(authDto);

            var result = await _authService.LoginAccount(user);
            return result;
        }

        [HttpGet("login/user")]
        public async Task<IActionResult> GetUserByToken(string token)
        {
            var result = await _authService.GetUserByToken(token);
            return result;
        }
    }
}
