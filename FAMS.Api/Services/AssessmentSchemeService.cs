using AutoMapper;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace FAMS.Api.Services
{
    public class AssessmentSchemeService : IAssessmentSchemeService
    {
        private readonly IBaseRepository<AssessmentScheme> _assessmentSchemeRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Class> _classRepo;
        private readonly FamsContext _context;

        public AssessmentSchemeService(IBaseRepository<AssessmentScheme> assessmentScheme, IMapper mapper, IBaseRepository<Class> classRepo
            , FamsContext context)
        {
            _assessmentSchemeRepo = assessmentScheme;
            _mapper = mapper;
            _classRepo = classRepo;
            _context = context;
        }

        public async Task<IActionResult> GetAll()
        {
            var types = await _assessmentSchemeRepo.Find();
            if (types == null)
            {
                return new OkObjectResult(EMS.EM77);
            }
            return new OkObjectResult(types);
        }

        public async Task<IActionResult> Create(CreateAssessmentSchemeDto createAssessmentSchemeDto)
        {
            var content = await _assessmentSchemeRepo.Get().Where(x => x.SyllabusId == createAssessmentSchemeDto.SyllabusId).FirstOrDefaultAsync();
            if (content != null) return new BadRequestResult();
            if(createAssessmentSchemeDto.Quiz + createAssessmentSchemeDto.Assignment + createAssessmentSchemeDto.Final != 100)
            {
                return new BadRequestObjectResult(EMS.EM33);
            }
            if(createAssessmentSchemeDto.FinalPractice + createAssessmentSchemeDto.FinalTheory != 100)
            {
                return new BadRequestObjectResult(EMS.EM33);
            }
            var map = _mapper.Map<AssessmentScheme>(createAssessmentSchemeDto);
            await _assessmentSchemeRepo.AddAsync(map);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Successfully");
        }

        public async Task<IActionResult> Update(UpdateAssessmentSchemeDto updateAssessmentSchemeDto)
        {
            try
            {
                var scheme = await _assessmentSchemeRepo.Get().Where(x => x.SyllabusId == updateAssessmentSchemeDto.SyllabusId).FirstOrDefaultAsync();
                if (scheme == null)
                {
                    return new BadRequestObjectResult("not found Syllabus Id: " + updateAssessmentSchemeDto.SyllabusId);
                }
                if (updateAssessmentSchemeDto.Quiz + updateAssessmentSchemeDto.Assignment + updateAssessmentSchemeDto.Final != 100)
                {
                    return new BadRequestObjectResult(EMS.EM33);
                }
                if (updateAssessmentSchemeDto.FinalPractice + updateAssessmentSchemeDto.FinalTheory != 100)
                {
                    return new BadRequestObjectResult(EMS.EM33);
                }
                _mapper.Map(updateAssessmentSchemeDto, scheme);
                _assessmentSchemeRepo.Update(scheme);
                await _assessmentSchemeRepo.SaveChangesAsync();
                return new OkObjectResult(scheme);
            }
            catch (Exception ex)
            {
                throw new Exception(EMS.EM104);
            }
        }
    }
}
