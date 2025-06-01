using AutoMapper;
using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]

    public class LearningObjectiveController : BaseApiController
    {
        private readonly ILearningObjectiveService _learningobjectService;
        private readonly IMapper _mapper;

        public LearningObjectiveController(ILearningObjectiveService learningObjectiveService, IMapper mapper)
        {
            _learningobjectService = learningObjectiveService;
            _mapper = mapper;
        }
        [HttpGet("learning-objectives/all")]
        public async Task<IActionResult> GetLearningObjective()
        {
            var result = await _learningobjectService.GetLearningObjectiveAsync();
            return Ok(result);
        }
        [HttpGet("learning-objectives/{code}")]
        public async Task<IActionResult> GetbyCode([FromRoute]string code)
        {
            var result = await _learningobjectService.GetLeaningObjectiveByCode(code);
            if (result == null)
            {
                return NotFound("No Objective found!");
            }
            return Ok(result);
        }

        [HttpGet("learning-objectives")]
        public async Task<IActionResult> Search(int? PageNumber = null, int? PageSize = null, string ? searchInput = null)
        {
            var result = await _learningobjectService.Search(PageNumber, PageSize, searchInput);
            return result;
        }
    }

}