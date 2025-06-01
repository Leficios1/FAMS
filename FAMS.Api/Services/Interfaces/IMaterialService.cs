using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<IActionResult> GetMaterials();
        Task<IActionResult> GetMaterialById(int id);
        Task<IActionResult> CreateMaterial(CreateMaterialDTO material);
        Task<IActionResult> DeleteMaterial(int id);

        Task<IEnumerable<MaterialDto>> GetByContentId (int contentId);
        Task DeleteRangeByContentId(int contentId);
    }
}
