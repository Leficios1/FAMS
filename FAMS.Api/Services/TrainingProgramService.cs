using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Helpers;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Core.Repositories.Interfaces;
using FAMS.Domain.Models.Dtos;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Immutable;
using System.Drawing.Text;
using System.Linq;
using System.Xml.Serialization;
using System.Net;
using System.Linq.Expressions;
using System.Web.Http;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.NetworkInformation;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Diagnostics;

namespace FAMS.Api.Services
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly IBaseRepository<TrainingProgram> _programRepo;
        private readonly IBaseRepository<TrainingProgramSyllabus> _programSyllabusRepo;
        private readonly IBaseRepository<Syllabus> _syllabusRepo;
        private readonly IUserService _userService;
        private readonly ISyllabusRepo _syllabusRepo1;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Class> _classRepo;
        private readonly IClassService _classService;
        private readonly ISyllabusService _syllabusService;
        private readonly FamsContext _context;
        public TrainingProgramService(IBaseRepository<TrainingProgram> programRepo, IBaseRepository<TrainingProgramSyllabus> programSyllabusRepo,
            IMapper mapper, IBaseRepository<Syllabus> syllabusRepo, IBaseRepository<Class> classRepo, FamsContext context, IUserService userService,
            ISyllabusRepo syllabusRepo1, IClassService classService, ISyllabusService syllabusService)
        {
            _programRepo = programRepo;
            _programSyllabusRepo = programSyllabusRepo;
            _mapper = mapper;
            _syllabusRepo = syllabusRepo;
            _syllabusRepo1 = syllabusRepo1;
            _userService = userService;
            _classRepo = classRepo;
            _context = context;
            _context = context;
            _mapper = mapper;
            _classService = classService;
            _syllabusService = syllabusService;
        }
        //done
        public async Task<IActionResult> Create(CreateTrainingProgramDTO programDTO)
        {
            var count = programDTO.SyllabusDTOs == null ? 0 : programDTO.SyllabusDTOs.Count();
            int[] check = new int[count];
            var index = 0;
            if (programDTO.SyllabusDTOs!=null&&programDTO.SyllabusDTOs.Any())
                foreach (CreateTrainingProgramSyllabusDTO dto in programDTO.SyllabusDTOs)
                {
                    check[index] = dto.Sequence;
                    index++;
                    var syllabus = await _syllabusRepo.GetById(dto.SyllabusId);
                    if (syllabus == null) return new BadRequestObjectResult(EMS.EM53 + dto.SyllabusId);
                }
            var flag = StringHelper.IsConsecutive(check);
            if (!flag)
            {
                throw new Exception(EMS.EM96);
            }
            if (programDTO.ClassIds != null && programDTO.ClassIds.Any())
            {
                foreach (int i in programDTO.ClassIds)
                {
                    if (i <= 0) return new BadRequestObjectResult(EMS.EM54 + i);
                    var checkClass = await _classRepo.GetById(i);
                    if (checkClass == null)
                    {
                        return new BadRequestObjectResult(EMS.EM50 + i);
                    }
                    if (checkClass.StartDate.Date < DateTime.Now.Date)
                    {
                        return new BadRequestObjectResult(EMS.EM97 + i);
                    }
                }

            }

            var result = await CreateTrainingProgramService(programDTO);

            if (!result) return new ContentResult() { Content = EMS.EM98, StatusCode = 500 };

            var addedProgramCode = await _programRepo.Get().OrderBy(x => x.TrainingProgramCode).Select(x => x.TrainingProgramCode).LastAsync();
            var returned = await ViewDetailTrainingProgram(addedProgramCode);

            return returned;
        }
        //done

        public async Task<IActionResult> SearchTrainingProgram(int? pageNumber = null, int? pageSize = null, string? searchString = null,
                                                       string? startDateBegin = null, string? startDateEnd = null,
                                                       string? sortBy = null, string? typeSort = null)
        {

            var query = _programRepo.Get();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                query = query.Where(x => x.TrainingProgramCode.ToString().Equals(searchString) || x.TopicCode.ToLower().Contains(searchString) || x.Name.ToLower().Contains(searchString)
                                       || x.CreatedBy.ToLower().Contains(searchString) || ((int)x.Duration).ToString().Equals(searchString));

            }
            if (!string.IsNullOrEmpty(startDateBegin))
            {
                DateTime startDateFrom = DateTime.Parse(startDateBegin);
                query = query.Where(x => x.CreatedDate.Date >= startDateFrom.Date);
            }
            if (!string.IsNullOrEmpty(startDateEnd))
            {
                DateTime startDateTo = DateTime.Parse(startDateEnd);
                query = query.Where(x => x.CreatedDate.Date <= startDateTo.Date);
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "trainingprogramcode":
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.TrainingProgramCode) : query.OrderBy(x => x.TrainingProgramCode);
                        break;
                    case OrderByConstant.SortBy_Name:
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case OrderByConstant.SortBy_CreatedBy:
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy);
                        break;
                    case OrderByConstant.SortBy_CreatedDate:
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate);
                        break;
                    case "durationbyday":
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.Duration) : query.OrderBy(x => x.Duration);
                        break;
                    case OrderByConstant.SortBy_Status:
                        query = !string.IsNullOrEmpty(typeSort) && typeSort.ToLower() == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status);
                        break;
                }
            }

            pageNumber = pageNumber.HasValue && pageNumber.Value > 0 ? pageNumber.Value : 1;
            pageSize = pageSize.HasValue && pageSize.Value > 0 ? pageSize.Value : 10;

            var totalItems = (decimal?)await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems.Value / pageSize.Value);

            query = query.Skip((int)((int)(pageNumber - 1) * pageSize)).Take((int)pageSize);
            var returned = (await query.Include(x => x.TrainingProgramSyllabuses).ThenInclude(x => x.Syllabus).ThenInclude(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ToListAsync()).Select(x => _mapper.Map<ViewListTrainingProgramDTO>(x));

            return new OkObjectResult(new ViewListResponse()
            {
                TotalPage = totalPages,
                PageSize = (int)pageSize,
                PageNumber = (int)pageNumber,
                List = returned.ToArray()
            });

        }
        public async Task<TrainingProgramDtoRequest> UpdateTrainingProgram(TrainingProgramDtoRequest trainingRequest)
        {

            var trainingProgram = await _programRepo.Get().Include(x => x.TrainingProgramSyllabuses).FirstOrDefaultAsync(x => x.TrainingProgramCode == trainingRequest.TrainingProgramCode);
            if (trainingProgram == null)
            {
                var mapperObject = _mapper.Map<TrainingProgram>(trainingRequest);
                await _programRepo.AddAsync(mapperObject);
            }
            else
            {
                _mapper.Map(trainingRequest, trainingProgram);
                if (trainingRequest.TrainingProgramSyllabus != null && trainingRequest.TrainingProgramSyllabus.Any())
                {
                    _context.TrainingProgramSyllabuses.RemoveRange(trainingProgram.TrainingProgramSyllabuses);
                    var mapperTraingList = _mapper.Map<IEnumerable<TrainingProgramSyllabus>>(trainingRequest.TrainingProgramSyllabus);
                    foreach (var item in mapperTraingList)
                    {
                        trainingProgram.TrainingProgramSyllabuses.Add(item);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return trainingRequest;
        }

        //done
        public async Task<StatusCodeResponse<List<CreateTrainingProgramDTO>>> ImportTrainingProgramByExcel(IFormFile file, int userId, string importType, string? scan)
        {
            var response = new StatusCodeResponse<List<CreateTrainingProgramDTO>>();
            response.Data = new List<CreateTrainingProgramDTO>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                if (file == null || file.Length <= 0)
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.Errormessge = (EMS.EM34);
                    return response;
                }

                // Ensure that the uploaded file is an Excel file
                if (!Path.GetFileNameWithoutExtension(file.FileName).Equals("import_trainningprogramm", StringComparison.OrdinalIgnoreCase) ||
                    !Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.Errormessge = EMS.EM99;
                    return response;
                }
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["TrainningProgram"];
                        int index = 3;
                        string name = ExcelHelper.ReadExcelCell(worksheet, index, 2);
                        using (var transaction = await _context.Database.BeginTransactionAsync())
                        {
                            try
                            {
                                while (name != null)
                                {
                                    CreateTrainingProgramDTO trainingProgramDTO = new CreateTrainingProgramDTO();
                                    trainingProgramDTO.Name = ExcelHelper.ReadExcelCell(worksheet, index, 2).ToString();
                                    trainingProgramDTO.UserId = userId;
                                    trainingProgramDTO.StartTime = ExcelHelper.ReadExcelCell(worksheet, index, 3);
                                    trainingProgramDTO.Duration = int.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 4));
                                    trainingProgramDTO.TopicCode = ExcelHelper.ReadExcelCell(worksheet, index, 5);
                                    trainingProgramDTO.Status = 0;
                                    trainingProgramDTO.CreatedBy = (await _userService.GetUser(userId))?.Name;
                                    int count = ExcelHelper.GetMergedCellCount(worksheet, index, 2);
                                    List<CreateTrainingProgramSyllabusDTO> listTrainingSyllabus = new List<CreateTrainingProgramSyllabusDTO>();
                                    int[] check = new int[count];
                                    for (int i = 0; i < count; i++)
                                    {
                                        CreateTrainingProgramSyllabusDTO createTrainingProgramSyllabusDTO = new CreateTrainingProgramSyllabusDTO();
                                        var syllabusName = ExcelHelper.ReadExcelCell(worksheet, index, 6).ToString();
                                        createTrainingProgramSyllabusDTO.SyllabusId = await _syllabusRepo1.GetIdBySyllabusNameAndMaxVersion(syllabusName);
                                        if (createTrainingProgramSyllabusDTO.SyllabusId <= 0)
                                        {
                                            //throw new ArgumentException($"Not Found Syllabus Name: {syllabusName}");
                                            response.statusCode = HttpStatusCode.NotFound;
                                            response.Errormessge = ($"{EMS.EM64}: {syllabusName}");
                                            return response;
                                        }
                                        createTrainingProgramSyllabusDTO.Sequence = int.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 7));
                                        check[i] = createTrainingProgramSyllabusDTO.Sequence;
                                        index++;
                                        listTrainingSyllabus.Add(createTrainingProgramSyllabusDTO);
                                    }
                                    bool flag = StringHelper.IsConsecutive(check);
                                    if (flag)
                                    {
                                        trainingProgramDTO.SyllabusDTOs = listTrainingSyllabus.ToArray();
                                        response.Data.Add(trainingProgramDTO);
                                        name = ExcelHelper.ReadExcelCell(worksheet, index, 2);
                                    }
                                    else
                                    {
                                        //throw new ArgumentException("sequences must be consecutive!!");
                                        response.statusCode = HttpStatusCode.UnprocessableEntity;
                                        response.Errormessge = (EMS.EM96);
                                        return response;
                                    }
                                    //await Create(trainingProgramDTO);
                                    switch (importType)
                                    {
                                        case "allow":
                                            await Create(trainingProgramDTO);
                                            response.Errormessge = "Create successful";
                                            response.statusCode = HttpStatusCode.OK;
                                            break;
                                        case "replace":
                                            if (scan.ToUpper().Equals("TopicCode".ToUpper()))
                                            {
                                                //var trainingProgram = _context.TrainingPrograms.Find(trainingProgramDTO.TopicCode);
                                                var trainingProgram = await _context.TrainingPrograms.OrderByDescending(x => x.TrainingProgramCode).
                                                    Where(p => p.TopicCode.ToUpper().Equals(trainingProgramDTO.TopicCode.ToUpper())).FirstOrDefaultAsync();

                                                if (trainingProgram == null)
                                                {
                                                    await Create(trainingProgramDTO);
                                                    response.statusCode |= HttpStatusCode.OK;
                                                    response.Errormessge = "Create Successful";
                                                }
                                                else
                                                {
                                                    var updated = _mapper.Map(trainingProgramDTO, trainingProgram);
                                                    _context.Update(updated);
                                                    response.statusCode |= HttpStatusCode.OK;
                                                    response.Errormessge = "Update Successful";
                                                }
                                                await _context.SaveChangesAsync();
                                            }
                                            if (scan.ToUpper().Equals("Name".ToUpper()))
                                            {
                                                var trainingProgram = await _programRepo.Get().FirstOrDefaultAsync(x => x.Name.ToUpper().Equals(trainingProgramDTO.Name.ToUpper()));
                                                if (trainingProgram == null)
                                                {
                                                    await Create(trainingProgramDTO);
                                                    response.statusCode |= HttpStatusCode.OK;
                                                    response.Errormessge = "Create Successful";
                                                }
                                                else
                                                {
                                                    var updated = _mapper.Map(trainingProgramDTO, trainingProgram);
                                                    _context.Update(updated);
                                                    response.statusCode |= HttpStatusCode.OK;
                                                    response.Errormessge = "Update Successful";
                                                }
                                                await _context.SaveChangesAsync();
                                            }
                                            break;
                                    }
                                }
                                await transaction.CommitAsync();
                            }
                            catch (Exception)
                            {
                                await transaction.RollbackAsync();
                                response.statusCode = HttpStatusCode.UnprocessableEntity;
                                response.Errormessge = EMS.EM65;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
        // done
        public async Task<IActionResult> ChangeStatusTrainingProgram(int trainingProgramCode, int status)
        {
            var trainingProgram = await _programRepo.GetById(trainingProgramCode);
            if (trainingProgram == null) return new BadRequestObjectResult(EMS.EM53);
            trainingProgram.Status = status;
            _programRepo.Update(trainingProgram);
            await _programRepo.SaveChangesAsync();
            return new OkObjectResult(new { TrainingProgramCode = trainingProgramCode, Status = status });
        }
        // done
        public async Task<IActionResult> ViewDetailTrainingProgram(int id)
        {
            if (id <= 0) return new BadRequestObjectResult(EMS.EM54);
            var checkExist = await _programRepo.Get().AnyAsync(x => x.TrainingProgramCode == id);
            if (!checkExist)
            {
                return new BadRequestObjectResult(EMS.EM53 + id);
            }
            var trainingProgram = await ViewDetailTrainingProgramService(id);
            return new OkObjectResult(trainingProgram);
        }
        // done
        public async Task<IActionResult> DuplicationTrainingProgram(int id)
        {

            var originSys = await _programRepo.Get()
                .Include(x => x.TrainingProgramSyllabuses)
                .FirstOrDefaultAsync(x => x.TrainingProgramCode == id);

            if (originSys == null)
            {
                return new NotFoundObjectResult(EMS.EM53 + id);
            }

            var duplicate = _mapper.Map(originSys, new TrainingProgram());
            duplicate.TrainingProgramCode = new int();
            duplicate.CreatedDate = DateTime.Now;
            duplicate.ModifiedDate = DateTime.Now;
            duplicate.Status = 0;

            await _programRepo.AddAsync(duplicate);
            if (duplicate.TrainingProgramSyllabuses != null && duplicate.TrainingProgramSyllabuses.Any())
                foreach (var trainingProgramSyllabus in duplicate.TrainingProgramSyllabuses)
                {
                    trainingProgramSyllabus.Id = new int();
                    await _programSyllabusRepo.AddAsync(trainingProgramSyllabus);
                }

            await _context.SaveChangesAsync();

            var justDuplicateProgram = await _programRepo.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

            if (justDuplicateProgram == null) throw new Exception(EMS.EM100);

            var result = await ViewDetailTrainingProgram(justDuplicateProgram.TrainingProgramCode);


            return result;
        }

        // done
        public async Task<ViewDetailTrainingProgramDTO?> ViewDetailTrainingProgramService(int id)
        {
            var syllabuses = await _syllabusService.GetSyllabusCardWithProgramCode(id);
            var classes = await _classService.GetByTrainingProgramCode(id);
            var durationByHoursTrainingProgram = await _programSyllabusRepo.Get().Where(x => x.TrainingProgramCode == id).Select(x => x.Syllabus).SelectMany(x => x.TrainingUnits).SelectMany(x => x.TrainingContents).SumAsync(x => x.Duration);

            var trainingProgram = await _programRepo.Get().Where(x => x.TrainingProgramCode == id).Include(x => x.TrainingProgramSyllabuses).ThenInclude(x => x.Syllabus).ThenInclude(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).Where(x => x.TrainingProgramCode == id).FirstOrDefaultAsync();
            if (trainingProgram == null) return null;

            var returnedDetail = _mapper.Map<ViewDetailTrainingProgramDTO>(trainingProgram);
            returnedDetail.Outline = syllabuses;
            returnedDetail.AssignedClass = classes.ToArray();
            returnedDetail.DurationByHour = (float)(durationByHoursTrainingProgram ?? 0);

            return returnedDetail;
        }
        // done
        public async Task<bool> CreateTrainingProgramService(CreateTrainingProgramDTO programDTO)
        {
            var program = _mapper.Map<TrainingProgram>(programDTO);
            try
            {
                await _programRepo.AddAsync(program);
                await _programRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(EMS.EM101);
            }
            var generatedCode = (await _programRepo.Get().OrderBy(x => x.TrainingProgramCode).LastAsync());
            if (programDTO.ClassIds!=null&&programDTO.ClassIds.Any())
                foreach (int i in programDTO.ClassIds)
                {
                    var updatedClass = await _classRepo.GetById(i);
                    updatedClass.TrainingProgramCode = generatedCode.TrainingProgramCode;
                    _classRepo.Update(updatedClass);
                    await _classRepo.SaveChangesAsync();
                }
            if (programDTO.SyllabusDTOs!=null && programDTO.SyllabusDTOs.Any())
                try
                {
                    var programSyllabuses = programDTO?.SyllabusDTOs?.Select(x =>
                    {
                        var result = (_mapper.Map<TrainingProgramSyllabus>(x));
                        result.TrainingProgramCode = (int)generatedCode.TrainingProgramCode;
                        return result;

                    });
                    await _programSyllabusRepo.AddRangeAsync(programSyllabuses);
                    await _programSyllabusRepo.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in adding program syllabuses: " + ex);
                }
            return true;
        }
        private async Task<bool> checkSyllabusDelete(int trainingProgramId)
        {
            var trainingProgram = await _classRepo.Get().Where(x => x.TrainingProgramCode==trainingProgramId).FirstOrDefaultAsync();
            if (trainingProgram != null) return true;
            else return false;
        }
        public async Task<StatusCodeResponse<bool>> deleteTrainingProgram(int trainingProgramId)
        {
            var result = new StatusCodeResponse<bool>();
            var TrainingProgram = await _programRepo.GetById(trainingProgramId);
            if (TrainingProgram == null)
            {
                result.Data = false;
                result.Errormessge = EMS.EM53;
                result.statusCode = HttpStatusCode.NotFound;
                return result;
            }
            var check = await checkSyllabusDelete(trainingProgramId);
            if (check)
            {
                result.Data = false;
                result.Errormessge = EMS.EM103;
                result.statusCode = HttpStatusCode.BadRequest;
                return result;
            }
            else
            {
                var trainingSyllabus = await _programSyllabusRepo.Get().Where(x => x.TrainingProgramCode==trainingProgramId).ToListAsync();
                if (trainingSyllabus != null)
                {
                    foreach (var t in trainingSyllabus) { _programSyllabusRepo.Delete(t); await _programSyllabusRepo.SaveChangesAsync(); }
                }
                _programRepo.Delete(TrainingProgram);
                await _programRepo.SaveChangesAsync();
            }
            result.Data = true;
            result.Errormessge = "Successfully";
            result.statusCode=HttpStatusCode.OK;
            return result;
        }
    }
}

