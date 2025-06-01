using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Controllers
{
    [ApiController]
    public class ClassController: BaseApiController
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("classes/{id}")]
        public async Task<IActionResult> ViewDetail(int id)
        {
            var result=await _classService.ViewClassDetail(id);
            return result;
        }

        [HttpGet("classes")]
        public async Task<IActionResult> SearchClassOnList(int? PageNumber= null, int? PageSize = null, string? searchString = null
                                                , string? locations = null, string? startDate = null, string? endDate = null,string? attendees = null, string? status = null, string? typeClassTime = null, string? FSU = null, int? trainerId = null,string? sortBy=null, string? order=null)
        {
            var list = await _classService.SearchClassOnList(PageNumber, PageSize, searchString, locations, startDate, endDate, attendees, status, typeClassTime, FSU, trainerId,sortBy,order);
            return list;
        }

        [HttpPost("classes")]
        public async Task<IActionResult> CreateClass(CreateClassDto createClassDto)
        {
            var result = await _classService.CreateClass(createClassDto);
            return result;
        }

        [HttpPut("classes")]
        public async Task<IActionResult> UpdateClass01(UpdateClass01RequestDto classUpdateDto)
        {
            var result = await _classService.UpdateClass01(classUpdateDto);
            return result;
        }
        [HttpDelete("classes/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var returnobj = await _classService.deleteClass(id);
            return StatusCode((int)returnobj.statusCode, returnobj.Errormessge);

        }
        [HttpPut("classes/{id}/change-status")]
        public async Task<IActionResult> ChangeStatusSyllabus(int id, int status)
        {
            var result = await _classService.ChangeStatusClass(id, status);
            return result;
        }
        [HttpPost("class/{id}/duplicate-class")]
        public async Task<IActionResult> DuplicateClass(int id)
        {
            var result = await _classService.DuplicationClass(id);
            return result;
        }
    }
}
