using AutoMapper;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FAMS.Api.Services
{
    public class LearningObjectiveService : ILearningObjectiveService
    {
        private readonly IBaseRepository<LearningObjective> _learningRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<SyllabusObjective> _syllabusObjectiveRepo;
        public LearningObjectiveService(IBaseRepository<LearningObjective> learningRepo, IMapper mapper, IBaseRepository<SyllabusObjective> syllabusObjectiveRepo)
        {
            _learningRepo = learningRepo;
            _mapper = mapper;
            _syllabusObjectiveRepo = syllabusObjectiveRepo;
            _syllabusObjectiveRepo = syllabusObjectiveRepo;
        }
        public async Task<IEnumerable<LearningObjective>> GetLearningObjectiveAsync()
        {
            var result = await _learningRepo.Get().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LearningObjective>> GetLeaningObjectiveByCode(string code)
        {
            var result = await _learningRepo.Get()
            .Where(result => result.ObjectiveCode.Contains(code))
            .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LearningObjective>> GetLearningObjectiveBySyllabusCode(int syllabusId)
        {
            var learningObjectives = await _learningRepo.Get()
                .Where(lo => lo.SyllabusObjectives.Any(so => so.SyllabusId==syllabusId))
                .ToListAsync();

            return learningObjectives;
        }
        public async Task<IEnumerable<LearningObjective>> GetLearningObjectiveByUnitCode(int unitCode)
        {
            var learningObjectives = await _learningRepo.Get()
                .Where(lo => lo.TrainingContents.Any(tc => tc.UnitCode == unitCode))
                .ToListAsync();

            return learningObjectives;
        }

        public async Task<IActionResult> Search(int? PageNumber = null, int? PageSize = null, string ? searchInput = null)
        {          
            var objects = await _learningRepo.Get().Where(x => string.IsNullOrEmpty(searchInput) || x.ObjectiveCode.ToLower().Contains(searchInput.ToLower()) || x.Description.Trim().ToLower().Contains(searchInput.Trim().ToLower())).ToListAsync();
            var returned = objects.Select(x=>new
            {
                x.ObjectiveCode, 
                x.Description
            }).ToList();
            if (!objects.Any()) return new OkObjectResult(EMS.EM88);

            var totalItems = returned.Count();

            if (PageNumber < 1 || PageNumber == null) PageNumber = 1;
            if (PageSize < 1 || PageSize == null)
            {
                PageSize = returned.Count();
            }
            var list = returned
                .Skip((int)((PageNumber - 1) * PageSize))
                .Take((int)PageSize).ToList();

            var totalPages = totalItems % PageSize == 0 && PageSize != list.Count ?
                             (int)Math.Ceiling((double)totalItems / (int)PageSize) : (int)Math.Ceiling((double)totalItems / (int)PageSize) + 1;

            return new OkObjectResult(new ViewListResponse()
            {
                TotalPage = totalPages,
                PageSize = (int)PageSize,
                PageNumber = (int)PageNumber,
                List = list.ToArray()
            });
        }
    }
}