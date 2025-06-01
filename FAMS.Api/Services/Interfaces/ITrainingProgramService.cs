using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface ITrainingProgramService
    {
        Task<IActionResult> Create(CreateTrainingProgramDTO programDTO);
        Task<TrainingProgramDtoRequest> UpdateTrainingProgram(TrainingProgramDtoRequest trainingRequest);
        Task<StatusCodeResponse<List<CreateTrainingProgramDTO>>> ImportTrainingProgramByExcel(IFormFile file, int userId, string importType, string? scan);
        Task<IActionResult> ViewDetailTrainingProgram(int id);
        Task<IActionResult> SearchTrainingProgram(int? pageNumber = null, int? pageSize = null, string? searchString = null,
                                                       string? startDateBegin = null, string? startDateEnd = null,
                                                       string? sortBy = null, string? typeSort = null);
        Task<IActionResult> DuplicationTrainingProgram(int id);
        Task<IActionResult> ChangeStatusTrainingProgram(int trainingProgramCode, int status);
        Task<StatusCodeResponse<bool>> deleteTrainingProgram(int trainingProgramId);
    }
}
