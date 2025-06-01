using FAMS.Api.Dtos;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface ISyllabusService
    {
        Task<Syllabus> GetSyllabusById(int id);

        Task<StatusCodeResponse<Syllabus>> ImportExcel(IFormFile file, int userId, string importType, string? scan);

        Task<IActionResult> SearchSyllabus(int? PageNumber = null, int? PageSize = null, string? outputStandardStrings = null, string? searchString = null, string? createdDateBegin = null, string? createdDateEnd = null, string? orderBy = null, string? typeSort = null);

        Task<IActionResult> CreateSyllabusOtherScreen(AssessmentSchemeRequest assessment);

        Task<IActionResult> CreateSyllabusGeneralTab(SyllabusDto sys);

        Task<StatusCodeResponse<CreatedSyllabusOutlineDto>> ImportTrainingExcel(IFormFile file, int syllabusId, string importType);
        Task<IActionResult> DuplicationSyllabus(int id);
        Task<StatusCodeResponse<bool>> ImportSyllabusByExcel(IFormFile file, int userId, string importType, string? scan);
        Task<ViewDetailSyllabusDto> AddSyllabusOutline(CreatedSyllabusOutlineDto syllabusRequest);

        Task<IActionResult> CreateSyllabusFull(CreateSyllabusDto createSyllabusDto);
        Task<IActionResult> UpdateSyllabus(UpdateSyllabusRequest request);
        public Task<IActionResult> GetDetailSyllabus(int syllabusId);

        Task<bool> DeleteSyllabus(int syllabusId);

        Task<SyllabusCard[]> GetSyllabusCardWithProgramCode(int programCode);
        public Task<ViewOutlineSyllabus[]> GetOutLineSyllabusBySyllabusId(int syllabusId);
        Task<StatusCodeResponse<ViewDetailSyllabusDto>> CreateSyllabusOutline(CreatedSyllabusOutline createdSyllabusOutline);
        Task<IActionResult> ChangeStatusSyllabus(int syllabusCode, int status);
    }
}
