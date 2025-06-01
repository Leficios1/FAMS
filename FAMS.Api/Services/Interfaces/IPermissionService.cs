using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        public Task<IActionResult> UpdatePermission(UserPermission[] permissions);
    }
}
