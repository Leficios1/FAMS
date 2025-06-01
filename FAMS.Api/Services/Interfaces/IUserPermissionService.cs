using FAMS.Api.Dtos;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface IUserPermissionService
    {   
        public Task<UserPermission> GetByID(string id); 
        Task<IActionResult> GetUserPermissionsAsync();
        public Task<IActionResult> UpdatePermission(UpdatePermissionDto[] permissions);
    }
}
