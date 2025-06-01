using AutoMapper;
using FAMS.Core.Exceptions;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FAMS.Api.Controllers
{
        
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordGenerator _passwordGenerator;

        public UsersController(IUserService userService, IMapper mapper, 
            IPasswordGenerator passwordGenerator)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordGenerator = passwordGenerator;
        }

        [HttpGet("users")]

        public async Task<IActionResult> SearchUsers(int pageNumber = 1, int pageSize = 10, string? searchInput = null, string? gender = null, string? roleName = null, string? dobFro = null, string? dobTo = null,
                                           string? sortBy = null, string? order = null)
        {
      
            var response = await _userService.SearchUsers( pageNumber, pageSize ,  searchInput ,  gender ,  roleName ,  dobFro ,  dobTo , sortBy ,  order );
            return response;
        }

        [HttpGet("users/{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null) return NotFound(EMS.EM51);
            return Ok(user);
        }
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {

            var userResponse = await _userService.CreateUser(user);
            if (userResponse == null) return new NotFoundObjectResult("Not Found");
            return Ok(userResponse);
        }

        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO user)
        {
            
            await _userService.UpdateUser(user);
            
            return Ok(await _userService.UpdateUser(user));
        }
        [HttpPut("users/change-avatar/{id}")]
        public async Task<IActionResult> EditAvatar([FromRoute] int id, [FromBody] string link)
        {

            var result = await _userService.EditAvater(link, id);

            return result;
        }
        [HttpGet("users/dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _userService.GetDashboard();
            return Ok(result);
        }

    }
}
