using FAMS.Domain.Dtos.Response;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IActionResult> EditAvater(string link, int userId);
        Task<UserResponseDto> CreateUser(CreateUserDTO user);

        Task<IEnumerable<UserResponseDto>> GetUsers(int? pageNumber, int? pageSize);


        Task<UserResponseDto> GetUser(int id);
        
        Task<UserResponseDto> UpdateUser(UpdateUserDTO user);

        Task<IActionResult> SearchUsers(int pageNumber = 1, int pageSize = 10, string? searchInput = null, string? gender = null, string? roleName = null, string? dobFro = null, string? dobTo = null,
                                           string? sortBy = null, string? order = null);
        Task<DashboardDto> GetDashboard();
    }
}
