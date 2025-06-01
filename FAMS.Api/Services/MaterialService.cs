using AutoMapper;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly FamsContext _context;
        private readonly IBaseRepository<Material> _materialRepo;
        private readonly IBaseRepository<TrainingContent> _trainingContentRepo;
        private readonly IMapper _mapper;
        public MaterialService(FamsContext context, IBaseRepository<Material> materialRepo, IMapper mapper, IBaseRepository<TrainingContent> trainingContentRepo)
        {
            _context = context;
            _materialRepo = materialRepo;
            _mapper = mapper;
            _trainingContentRepo = trainingContentRepo;
        }

        public async Task<IActionResult> GetMaterials()
        {
            var materials = await _materialRepo.Get().ToListAsync();
            var returned = materials.Select(x => _mapper.Map<MaterialDto>(x));
            return new OkObjectResult(returned);
        }

        public async Task<IActionResult> GetMaterialById(int id)
        {    
            var material = await _materialRepo.Get().FirstOrDefaultAsync(x => x.Id == id);
            if(material == null) return new NotFoundObjectResult(EMS.EM59+ id);
            var returned = _mapper.Map<MaterialDto>(material);   
            return new OkObjectResult(returned);
        }

        public async Task<IActionResult> CreateMaterial(CreateMaterialDTO material)
        {   
            var content = await _trainingContentRepo.Get().Where(x=>x.Id==material.ContentId).FirstOrDefaultAsync();
            if(content == null ) return new BadRequestObjectResult(EMS.EM56+material.ContentId);
                var entity = new Material()
            {
                ContentId = material.ContentId,
                Url = material.Url
            };
            await _materialRepo.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Add Successfully");
        }

        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var material = await _materialRepo.Get().FirstOrDefaultAsync(x => x.Id == id);
            if (material == null) return new BadRequestObjectResult(EMS.EM56 + id);
            _materialRepo.Delete(material);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Delete Successfully");
        }

        public async Task <IEnumerable<MaterialDto>> GetByContentId(int contentId)
        {   
            var content = await _trainingContentRepo.Get().Where(x=>x.Id == contentId).FirstOrDefaultAsync();
            if (content == null) throw new Exception(EMS.EM56 + contentId);
            var materials = await _materialRepo.Get().Where(x=>x.ContentId==contentId).Select(x=>new MaterialDto()
            {
                Id=x.Id,ContentId=x.ContentId, Url=x.Url
            }).ToListAsync();
            return materials.AsEnumerable();
        }

        public async Task DeleteRangeByContentId(int contentId)
        {
            var content = await _trainingContentRepo.Get().Where(x => x.Id == contentId).FirstOrDefaultAsync();
            if (content == null) throw new Exception(EMS.EM56 + contentId);

            var materials = await _materialRepo.Get().Where(x => x.ContentId == contentId).ToListAsync();

            foreach (var material in materials)
            {
                _materialRepo.Delete(material);
                await _materialRepo.SaveChangesAsync();
            }

        }
    }
}
