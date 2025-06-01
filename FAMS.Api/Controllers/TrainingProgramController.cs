using FAMS.Api.Services.Interfaces;
using FAMS.Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using FAMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace FAMS.Api.Controllers
{
    [ApiController]

    public class TrainingProgramController : BaseApiController
    {
        private readonly ITrainingProgramService _trainingProgramService;
        private readonly IAuthService _authService;
        public TrainingProgramController(ITrainingProgramService trainingProgramService, IAuthService authService)
        {
            _trainingProgramService = trainingProgramService;
            _authService = authService;
        }

        [HttpGet("training-programs/{id}")]
        public async Task<IActionResult> ViewTrainingProgram([FromRoute] int id)
        {
            var result = await _trainingProgramService.ViewDetailTrainingProgram(id);
            return result;
        }
        [HttpGet("training-programs")]
        public async Task<IActionResult> Get(int? pageNumber = null, int? pageSize = null, string? searchString = null,
                                                       string? startDateBegin = null, string? startDateEnd = null,
                                                       string? sortBy = null, string? order = null)
        {
            var result = await _trainingProgramService.SearchTrainingProgram(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, order);
            return result;
        }

        [HttpPost("training-programs/{id}/duplicate-training-program")]
        public async Task<IActionResult> Duplicate(int id)
        {
            var result = await _trainingProgramService.DuplicationTrainingProgram(id);
            return result;
        }
        [HttpPost("importExcel")]
        public async Task<IActionResult> ImportExcel1(IFormFile file, int userId, string importType, string? scan)
        {
            var returnobj = await _trainingProgramService.ImportTrainingProgramByExcel(file, userId, importType, scan);
            return StatusCode((int)returnobj.statusCode, returnobj.Errormessge);
        }
        [HttpDelete("training-programs/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var returnobj = await _trainingProgramService.deleteTrainingProgram(id);
            return StatusCode((int)returnobj.statusCode, returnobj.Errormessge);
        }
        [HttpPost("training-programs")]
        public async Task<IActionResult> Create(CreateTrainingProgramDTO createTrainingProgramDTO)
        {
            var result = await _trainingProgramService.Create(createTrainingProgramDTO);
            return result;
        }

        [HttpPut("training-programs")]
        public async Task<IActionResult> UpdateTrainingProgram(TrainingProgramDtoRequest traingRequest)
        {
            

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _trainingProgramService.UpdateTrainingProgram(traingRequest);

            var returnDetailTrainingProgram = await _trainingProgramService.ViewDetailTrainingProgram(traingRequest.TrainingProgramCode);
            return Ok(returnDetailTrainingProgram);
        }
        [HttpPut("training-programs/{id}/change-status")]
        public async Task<IActionResult> ChangeStatusTrainingProgram(int id, int status)
        {
            var result = await _trainingProgramService.ChangeStatusTrainingProgram(id, status);
            return result;
        }
    }
}
