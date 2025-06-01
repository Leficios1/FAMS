using Microsoft.AspNetCore.Mvc;
using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace FAMS.Api.Controllers
{
    [ApiController]
    public class AssessmentSchemeController : BaseApiController
    {
        private readonly IAssessmentSchemeService _assessmentSchemeService;
        public AssessmentSchemeController(IAssessmentSchemeService assessmentSchemeService)
        {
            _assessmentSchemeService = assessmentSchemeService;
        }

        [HttpGet("assessment-scheme/{get-by-syllabusid}")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _assessmentSchemeService.GetAll();
            return result;
        }

        [HttpPost("assessment-scheme")]
        public async Task<IActionResult> Create(CreateAssessmentSchemeDto createAssessmentSchemeDto)
        {
            var result = await _assessmentSchemeService.Create(createAssessmentSchemeDto);
            return result;
        }

        [HttpPut("assessment-scheme")]
        public async Task<IActionResult> Update(UpdateAssessmentSchemeDto updateAssessmentSchemeDto)
        {
            var result = await _assessmentSchemeService.Update(updateAssessmentSchemeDto);
            return result;
        }
    }
}
