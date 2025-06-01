using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface IClassService
    {
  
        Task<IActionResult> ViewClassDetail(int id);
        Task<IActionResult> SearchClassOnList(int? PageNumber = null, int? PageSize = null, string? searchString = null
                                                , string? locations = null, string? startDate = null, string? endDate = null, string? attendees = null, string? status = null, string? typeClassTime = null, string? FSU = null, int? trainerId = null, string? sortBy = null, string? order = null);
        Task<IActionResult> UpdateClass01(UpdateClass01RequestDto classUpdateRequest);

        Task<IEnumerable<ViewListClassDTO>> GetByTrainingProgramCode(int trainingProgramCode); 
        Task<IActionResult> CreateClass(CreateClassDto createClassDto);
        Task<IEnumerable<Class>> GetClassByUserId(int Id);
        Task<Class> GetClassById(int Id);
        Task<StatusCodeResponse<bool>> deleteClass(int id);
        Task<IActionResult> ChangeStatusClass(int classCode, int status);
        Task<IActionResult> DuplicationClass(int id);
    }
}
