using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface ILearningObjectiveService
    {
        Task<IEnumerable<LearningObjective>> GetLearningObjectiveAsync();
        Task<IEnumerable<LearningObjective>> GetLeaningObjectiveByCode(string code);
        Task<IEnumerable<LearningObjective>> GetLearningObjectiveBySyllabusCode(int syllabusId);
        Task<IEnumerable<LearningObjective>> GetLearningObjectiveByUnitCode(int unitCode);
        Task<IActionResult> Search(int? PageNumber = null, int? PageSize = null, string? searchInput = null);
    }
}