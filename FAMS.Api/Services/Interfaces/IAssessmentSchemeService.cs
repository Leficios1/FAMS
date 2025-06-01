using FAMS.Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface IAssessmentSchemeService
    {
        Task<IActionResult> Create(CreateAssessmentSchemeDto createAssessmentSchemeDto);
        Task<IActionResult> Update(UpdateAssessmentSchemeDto updateAssessmentSchemeDto);

        Task<IActionResult> GetAll();
    }
}
