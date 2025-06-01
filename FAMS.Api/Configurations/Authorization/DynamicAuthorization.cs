using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Entities;
using FAMS.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FAMS.Api.Configurations.Authorization
{
    public class DynamicAuthorization : IAuthorizationMiddlewareResultHandler
    {
        private readonly IUserPermissionService _userPermissionService;

        public DynamicAuthorization(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var role = user.FindFirst(ClaimTypes.Role)?.Value;

                if (role == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                var method = context.Request.Method;
                var endpoint = context.GetEndpoint();
                var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;

                var checkAuth = await ValidateRole(role, routePattern, method);

                if (checkAuth)
                {
                    authorizeResult = PolicyAuthorizationResult.Success();
                    await next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("You don't have enough permission's policies to use this api.");
            return;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var role = user.FindFirst(ClaimTypes.Role)?.Value;

                if (role == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                var method = context.Request.Method;
                var endpoint = context.GetEndpoint();
                var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;

                var checkAuth = await ValidateRole(role, routePattern, method);

                if (checkAuth)
                {
                    await next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("You don't have enough permission's policies to use this API.");
        }
        public async Task<bool> ValidateRole(string role, string routePattern, string httpMethod)
        {
            var userPermission = await _userPermissionService.GetByID(role);

            if (userPermission == null)
                return false;

            switch (httpMethod.ToLower())
            {
                case "post":
                    if (Endpoints.GetSyllabusEndpoints_CreateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.Syllabus == (int)PermissionEnum.FullAccess || userPermission.Syllabus == (int)PermissionEnum.Create || userPermission.Syllabus == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetClassEndPoints_CreateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.Class == (int)PermissionEnum.FullAccess || userPermission.Class == (int)PermissionEnum.Create || userPermission.Class == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetTrainingProgramEndPoints_CreateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.TrainingProgram == (int)PermissionEnum.FullAccess || userPermission.TrainingProgram == (int)PermissionEnum.Create || userPermission.TrainingProgram == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetUserEndPoints_CreateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.UserManagement == (int)PermissionEnum.FullAccess || userPermission.UserManagement == (int)PermissionEnum.Create || userPermission.UserManagement == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    break;

                case "get":
                    return true;

                case "delete":
                    if (Endpoints.GetSyllabusEndpoints_FullAccess().Contains(routePattern))
                    {
                        return userPermission.Syllabus == (int)PermissionEnum.FullAccess || userPermission.Syllabus == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetClassEndpoints_FullAccess().Contains(routePattern))
                    {
                        return userPermission.Class == (int)PermissionEnum.FullAccess || userPermission.Class == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetTrainingProgramEndpoints_FullAccess().Contains(routePattern))
                    {
                        return userPermission.TrainingProgram == (int)PermissionEnum.FullAccess || userPermission.TrainingProgram == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    else if (Endpoints.GetUserEndpoints_FullAccess().Contains(routePattern))
                    {
                        return userPermission.UserManagement == (int)PermissionEnum.FullAccess || userPermission.UserManagement == (int)PermissionEnum.ExceptImportSyllabus;
                    }
                    break;

                case "put":
                    if (Endpoints.GetSyllabusEndPoints_UpdateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.Syllabus == (int)PermissionEnum.FullAccess || userPermission.Syllabus == (int)PermissionEnum.ExceptImportSyllabus || userPermission.Syllabus == (int)PermissionEnum.Modify;
                    }
                    else if (Endpoints.GetClassEndPoints_UpdateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.Class == (int)PermissionEnum.FullAccess || userPermission.Class == (int)PermissionEnum.ExceptImportSyllabus || userPermission.Class == (int)PermissionEnum.Modify;
                    }
                    else if (Endpoints.GetTrainingProgramEndPoints_UpdateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.TrainingProgram == (int)PermissionEnum.FullAccess || userPermission.TrainingProgram == (int)PermissionEnum.ExceptImportSyllabus || userPermission.TrainingProgram == (int)PermissionEnum.Modify;
                    }
                    else if (Endpoints.GetUserEndPoints_UpdateAndViewMode().Contains(routePattern))
                    {
                        return userPermission.UserManagement == (int)PermissionEnum.FullAccess || userPermission.UserManagement == (int)PermissionEnum.ExceptImportSyllabus || userPermission.UserManagement == (int)PermissionEnum.Modify;
                    }
                    break;
            }

            return false;
        }
    }
}
