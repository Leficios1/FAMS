using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Services
{
    public class UserPermissionService : IUserPermissionService
    {

        private readonly IBaseRepository<UserPermission> _permissionRepo;
        private readonly IMapper _mapper;
        public UserPermissionService(IBaseRepository<UserPermission> UserPermission, IMapper mapper)
        {
            _permissionRepo = UserPermission;
            _mapper = mapper;
        }

        public async Task<UserPermission> GetByID(string id)
        {
            var permission = await _permissionRepo.Get().Where(x=>x.PermissionId.ToLower().Equals(id.ToLower())).FirstOrDefaultAsync() ;
            return permission;

        }

        public async Task<IActionResult> GetUserPermissionsAsync() {

            var objects= await _permissionRepo.Get().ToListAsync();
            if(!objects.Any()) return new OkObjectResult(new { Message= "Not found any user permissions" });
            var list = objects.Select(o=>_mapper.Map<UserPermissionResponseDto>(o)).ToList();
            return new OkObjectResult(new {Message = "Get user permissions successfully",Objects= list});
        }

        public async Task<IActionResult> UpdatePermission(UpdatePermissionDto[] permissionDtos)
        {
            var permissions = permissionDtos.Select(pdt=>_mapper.Map<UserPermission>(pdt));
            IActionResult result;
            if (!permissions.Any())
            {
                result = new BadRequestObjectResult(new { Message = "There is no permission in the request." });
            }
            else
            {
                foreach (var permission in permissions)
                {
                    var updatedPermission = await _permissionRepo.FindOne(p => p.PermissionId.Equals(permission.PermissionId));
                    if (updatedPermission == null)
                    {
                        result = new NotFoundObjectResult(new { Message = $"There has no permission that has id: '{permission.PermissionId} .'" });
                        return result;
                    }
                }
                foreach (var permission in permissions)
                {
                    var updatedPermission = await _permissionRepo.FindOne(p => p.PermissionId.Equals(permission.PermissionId));
                    updatedPermission.TrainingProgram = permission.TrainingProgram;
                    updatedPermission.UserManagement = permission.UserManagement;
                    updatedPermission.LearningMaterial = permission.LearningMaterial;
                    updatedPermission.Syllabus = permission.Syllabus;
                    updatedPermission.Class = permission.Class;
                    _permissionRepo.Update(updatedPermission);  
                    await _permissionRepo.SaveChangesAsync();
                }

                var updatedPermissions = await _permissionRepo.Find();
                var returnedPermissions = updatedPermissions?.Select(p => _mapper.Map<UserPermission, UpdatePermissionDto>(p)).ToArray();

                result = new OkObjectResult(new { Message = "There is succesful to update.", Permissions = returnedPermissions });
            }
            return result;
        }
    }
}
