using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Cors;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Results;

namespace FAMS.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
  
    public class SyllabusController : BaseApiController
    {
        private readonly ISyllabusService _syllabusService;
        private readonly IMapper _mapper;
        private readonly ITrainingUnitService _unitService;
        // mapping ITrainingContentService
        private readonly ITrainingContentService _contentService;

        public SyllabusController(ISyllabusService syllabusService, IMapper mapper, ITrainingContentService trainingContentService, ITrainingUnitService trainingUnitService)
        {
            _syllabusService = syllabusService;
            _mapper = mapper;
            _unitService = trainingUnitService;
            _contentService = trainingContentService;
        }

        
        [HttpGet("syllabuses")]
        public async Task<IActionResult> SearchSyllabus(int? PageNumber = 1, int? PageSize = 10, string? outputStandardStrings = null, string? searchString = null, string? createdDateBegin = null, string? createdDateEnd = null, string? sortBy = null, string? order = null)
        {
            var result = await _syllabusService.SearchSyllabus(PageNumber, PageSize, outputStandardStrings, searchString, createdDateBegin, createdDateEnd, sortBy, order);
            return result;
        }

        [HttpGet("syllabuses/time-allocation/{id}")]
        public async Task<IActionResult> getTimeAllocation([FromRoute] int id)
        {
            var result = await _contentService.getTimeAllocation(id);
            return result;
        }
        [HttpGet("syllabuses/{id}")]
        public async Task<IActionResult> GetDetailSyllabus([FromRoute] int id)
        {
            var result = await _syllabusService.GetDetailSyllabus(id);
            return result;
        }

        [HttpGet("syllabuses/outline/{id}")]

        public async Task<IActionResult> GetOutlineSyllabus([FromRoute]int id)
        {
            var result = await _syllabusService.GetOutLineSyllabusBySyllabusId(id);
            return Ok(result);
        }
        [HttpGet("syllabuses/syllabus-card/{programCode}")]
        public async Task<IActionResult> GetSyllabusCardByProgramCode([FromRoute]int programCode)
        {
            var result = await _syllabusService.GetSyllabusCardWithProgramCode(programCode); 
            return Ok(result);
        }
        [HttpPost("syllabuses/create-syllabus-general-tab")]
        public async Task<IActionResult> CreateSyllabusGeneralTab(SyllabusDto syllabus)
        {

            var result = await _syllabusService.CreateSyllabusGeneralTab(syllabus);
            return result;
        }

        [HttpPost("syllabuses/duplicate-syllabus/{id}")]
        public async Task<IActionResult> DuplicateSyllabus([FromRoute] int id)
        {
            var cloneSyllabus = await _syllabusService.DuplicationSyllabus(id);
            return cloneSyllabus;
        }
        [HttpPost("syllabuses/create-syllabus-outline")]
        public async Task<ActionResult> CreateSyllabusOutline([FromBody] CreatedSyllabusOutline syllabusRequest)
        {
            var returnobj = await _syllabusService.CreateSyllabusOutline(syllabusRequest);
            return StatusCode((int)returnobj.statusCode, returnobj.Data != null ? returnobj.Data : returnobj.Errormessge) ;

        }

        [HttpPost("syllabuses/create-syllabus-otherscreen")]
        public async Task<ActionResult> CreateSyllabusOtherScreen(AssessmentSchemeRequest assessment)
        {
            var result = await _syllabusService.CreateSyllabusOtherScreen(assessment);
            return Ok(result);
        }

        [HttpPost("syllabuses/import-excel")]
        public async Task<IActionResult> ImportExcel(IFormFile file, int userId, string importType, string? scan)
        {
            var returnobj = await _syllabusService.ImportSyllabusByExcel(file, userId, importType, scan);
            if (returnobj == null)
            {
                return BadRequest("Can't not save the OutlineSyllabus");
            }
            return StatusCode((int)returnobj.statusCode, returnobj.Errormessge);
        }
        [HttpPost("syllabus/create-syllabus")]
        public async Task<IActionResult> CreateSyllabusFull(CreateSyllabusDto createSyllabus)
        {
            var res = await _syllabusService.CreateSyllabusFull(createSyllabus);

            return res;
        }
        [HttpPut("syllabus")]
        public async Task<IActionResult> UpdateSyllabus(UpdateSyllabusRequest updateSyllabus)
        {
            var result = await _syllabusService.UpdateSyllabus(updateSyllabus);
            return result;
        }
        [HttpDelete("syllabuses/{id}")]
        public async Task<IActionResult> DeleteSyllabus([FromRoute] int id)
        {
            var result = await _syllabusService.DeleteSyllabus(id);
            return Ok("Syllabus deleted successfully.");
        }
        [HttpPut("syllabuses/{id}/change-status")]
        public async Task<IActionResult> ChangeStatusSyllabus(int id, int status)
        {
            var result = await _syllabusService.ChangeStatusSyllabus(id, status);
            return result;
        }
    }
}
