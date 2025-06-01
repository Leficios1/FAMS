using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<IActionResult> LoginAccount(User user);
        public Task<IActionResult> GetUserByToken(string token);
    }
}
