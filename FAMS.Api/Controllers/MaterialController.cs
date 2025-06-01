using FAMS.Api.Services.Interfaces;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAMS.Api.Controllers
{
    [ApiController]
    public class MaterialController : BaseApiController
    {
        private readonly IMaterialService _materialService;
        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        
        [HttpGet("materials")]
        public async Task<IActionResult> Get()
        {
            var result = await _materialService.GetMaterials();
            return result;
        }

   
        [HttpGet("materials/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _materialService.GetMaterialById(id);
            return result;
        }
        [HttpGet("materials/by-content-id/{id}")]
        public async Task<IActionResult> GetByContentId(int id)
        {
            var result = await _materialService.GetByContentId(id);
            if (result == null || !result.Any()) return new NotFoundObjectResult("There is no materials in content that has ID: " + id);
            return new OkObjectResult(result);
        }

        [HttpPost("materials")]
        public async Task<IActionResult> Create(CreateMaterialDTO material)
        {
            var result = await _materialService.CreateMaterial(material);
            return result;
        }
        
        [HttpDelete("materials/{id}")]
        public async Task<IActionResult> DElete(int id)
        {
            var result = await _materialService.DeleteMaterial(id);
            return result;
        }
    }
}
