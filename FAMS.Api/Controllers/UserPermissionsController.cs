using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Controllers
{
    public class UserPermissionsController : BaseApiController
    {
        private readonly IUserPermissionService _services;
        private readonly IMapper _mapper;

        public UserPermissionsController(IUserPermissionService service, IMapper mapper) 
        {
            _services = service;
            _mapper = mapper;
        }

        [HttpGet("user-permissions")]
        public async Task<IActionResult> GetUserPermission()
        {
            var result = await _services.GetUserPermissionsAsync();
            return result;
        }
        [HttpPut("user-permissions/update")]
        public async Task<IActionResult> UpdatePermissions([FromBody] UpdatePermissionDto[] updatedPermissions)
        {
            var result = await _services.UpdatePermission(updatedPermissions);
            return result;
        }
    }
}
