using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using FAMS.Domain.Models.Entities;
using FAMS.Api.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FAMS.Core.Repositories.Interfaces;
using FAMS.Domain.Models.Dtos.Request;

using Microsoft.IdentityModel.Tokens;
using FAMS.Domain.Models.Dtos.Response;
using System.Drawing.Printing;


using OfficeOpenXml;
using FAMS.Core.Helpers;
using System.Xml;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using FAMS.Domain.Models.Dtos;
using FAMS.Api.Services.Interfaces;
using System.Net;
using System.Net.WebSockets;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using FAMS.Domain.Constants;
using Microsoft.OpenApi.Validations;
using System.Globalization;
using FAMS.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FAMS.Api.Services
{
    public class SyllabusService : ISyllabusService
    {
        private readonly IBaseRepository<Syllabus> _syllabus;
        private readonly FamsContext _context;
        private readonly ISyllabusRepo _syllabusRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITrainingUnitService _trainingUnitService;
        private readonly ITrainingContentService _trainingContentService;
        private readonly IBaseRepository<TrainingContent> _trainingContentRepo;
        private readonly IBaseRepository<TrainingUnit> _trainingUnitRepo;
        private readonly IBaseRepository<SyllabusObjective> _syllabusObjectiveRepo;
        private readonly IBaseRepository<DeliveryType> _deliveryRepo;
        private readonly IBaseRepository<LearningObjective> _learningObjectRepo;
        private readonly IMaterialService _materialService;
        private readonly IBaseRepository<Material> _materialRepo;
        private readonly IBaseRepository<ClassUser> _classUserRepo;
        private readonly IBaseRepository<ClassTrainerUnit> _trainerUnitRepo;
        IBaseRepository<AssessmentScheme> _schemaRepo;


        private readonly IDeliveryTypeService _deliveryTypeService;

        public SyllabusService(IBaseRepository<Syllabus> syllabus, FamsContext context, IMapper mapper, ISyllabusRepo syllabusRepo, IUserService userService
            , ITrainingUnitService trainingUnitService, ITrainingContentService trainingContentService, IBaseRepository<TrainingContent> trainingContentRepo
            , IBaseRepository<TrainingUnit> trainingUnitRepo, IBaseRepository<SyllabusObjective> syllabusObjectiveRepo, IBaseRepository<DeliveryType> deliveryRepo,
             IBaseRepository<LearningObjective> learningObjectRepo, IDeliveryTypeService deliveryTypeService, IBaseRepository<AssessmentScheme> schemaRepo, IMaterialService materialService,
             IBaseRepository<Material> materialRepo, IBaseRepository<ClassUser> classUserRepo, IBaseRepository<ClassTrainerUnit> trainerUnitRepo)
        {
            _syllabus = syllabus;
            _context = context;
            _mapper = mapper;
            _syllabusRepo = syllabusRepo;
            _userService = userService;
            _trainingUnitService = trainingUnitService;
            _trainingContentService = trainingContentService;
            _trainingContentRepo = trainingContentRepo;
            _deliveryRepo = deliveryRepo;
            _learningObjectRepo = learningObjectRepo;
            _syllabusObjectiveRepo = syllabusObjectiveRepo;
            _deliveryTypeService = deliveryTypeService;
            _schemaRepo = schemaRepo;
            _materialService = materialService;
            _trainingUnitRepo = trainingUnitRepo;
            _materialRepo = materialRepo;
            _classUserRepo = classUserRepo;
            _trainerUnitRepo = trainerUnitRepo;
        }


        public async Task<Syllabus> GetSyllabusById(int id)
        {
            return await _syllabus.Get().Include(a => a.AssessmentScheme).Include(z => z.TrainingUnits).ThenInclude(y => y.TrainingContents).Where(x => x.Id == id).SingleOrDefaultAsync();

        }

        private async Task<bool> CheckDuplicateSyllabusId(int id)
        {
            var result = await _syllabus.Get().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }
        //done
        public async Task<IActionResult> SearchSyllabus(int? PageNumber = null, int? PageSize = null, string? outputStandardStrings = null, string? searchString = null, string? createdDateBegin = null, string? createdDateEnd = null, string? orderBy = null, string? typeSort = null)
        {
            var query = ((await _syllabus.Get().Include(x => x.SyllabusObjectives).Include(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ToListAsync())).Select(x => _mapper.Map<ViewListSyllabusDTO>(x));

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower().Trim();
                query = query.Where(x => (string.IsNullOrEmpty(searchString) || (x.SyllabusCode != null && x.SyllabusCode.ToLower().Contains(searchString))
                                       || (x.SyllabusName != null && x.SyllabusName.ToLower().Contains(searchString))
                                       || (x.CreatedBy != null && x.CreatedBy.ToLower().Contains(searchString))
                                       || (x.OutputStandards != null && x.OutputStandards.Any(y => y.ToLower().Contains(searchString)))
                                       || (x.DurationByDay.ToString().Equals(searchString))));
            }

            if (!string.IsNullOrEmpty(createdDateBegin))
            {
                var createdDateFrom = DateTime.Parse(createdDateBegin);
                query = query.Where(x => x.CreatedDate != null ? DateTime.ParseExact(x.CreatedDate, Value.DateFormat, CultureInfo.InvariantCulture).Date >= createdDateFrom.Date : true);
            }

            if (!string.IsNullOrEmpty(createdDateEnd))
            {
                var createdDateTo = DateTime.Parse(createdDateEnd);
                query = query.Where(x => x.CreatedDate != null ? DateTime.ParseExact(x.CreatedDate, Value.DateFormat, CultureInfo.InvariantCulture).Date <= createdDateTo.Date : true);
            }

            if (!string.IsNullOrEmpty(outputStandardStrings))
            {
                string[] outputStandards = DivideString(outputStandardStrings).Select(x => x.ToLower()).ToArray();
                if (outputStandards.Length > 0)
                {
                    outputStandards = outputStandards.Select(x => x.ToLower()).ToArray();
                    query = query.Where(x => x.OutputStandards != null && outputStandards.Intersect(x.OutputStandards.Select(x => x.ToLower())) != null && outputStandards.Intersect(x.OutputStandards.Select(x => x.ToLower())).Any()).ToList();
                }
            }

            var totalItems = query.Count();

            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "syllabuscode":
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.SyllabusCode) : query.OrderBy(x => x.SyllabusCode);
                        break;
                    case "syllabusname":
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.SyllabusName) : query.OrderBy(x => x.SyllabusName);
                        break;
                    case OrderByConstant.SortBy_CreatedBy:
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.CreatedBy) : query.OrderBy(x => x.CreatedBy);
                        break;
                    case OrderByConstant.SortBy_CreatedDate:
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate);
                        break;
                    case "durationbyday":
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.DurationByDay) : query.OrderBy(x => x.DurationByDay);
                        break;
                    case OrderByConstant.SortBy_OutputStandards:
                        query = typeSort == OrderByConstant.OrderByDESC ? query.OrderByDescending(x => x.OutputStandards.Length) : query.OrderBy(x => x.OutputStandards.Length);
                        break;
                }
            }

            var number = PageNumber.HasValue && PageNumber.Value > 0 ? PageNumber.Value : 1;
            var size = PageSize.HasValue && PageSize.Value > 0 ? PageSize.Value : 10;
            query = query.Skip((number - 1) * size).Take(size);
            var totalPages = (int)Math.Ceiling((double)totalItems / size);

            return new OkObjectResult(new ViewListResponse
            {
                TotalPage = totalPages,
                PageNumber = number,
                PageSize = size,
                List = query.ToArray()
            });
        }
        private IEnumerable<string> DivideString(string name)
        {
            var names = name.Split(',').Select(x => x.Trim().ToLower());
            return names;
        }
        //done
        public async Task CreateSyllabusOtherScreen(AssessmentScheme assessment)
        {
            await _schemaRepo.AddAsync(assessment);
            await _schemaRepo.SaveChangesAsync();
        }

        public async Task<IActionResult> CreateSyllabusGeneralTab(SyllabusDto sysllabus)
        {
            var sys = _mapper.Map<Syllabus>(sysllabus);
            sys.Version = "0.1";
            sys.Priority = sysllabus.Level;
            await _syllabus.AddAsync(sys);
            await _syllabus.SaveChangesAsync();

            return new OkObjectResult(sys);
        }

        public async Task<IActionResult> CreateSyllabusOtherScreen(AssessmentSchemeRequest assessment)
        {
            var syllabus = await _syllabus.Get()
                .FirstOrDefaultAsync(x => x.Id == assessment.SyllabusId);
            if (syllabus == null) return new BadRequestObjectResult(EMS.EM52);
            syllabus.TrainingPrinciples = assessment.trainingPrinciple;


            if (assessment.Quiz + assessment.Assignment + assessment.Final != 100)
            {
                return new BadRequestObjectResult(EMS.EM33);
            }

            if (assessment.FinalTheory + assessment.FinalPractice != 100)
            {
                return new BadRequestObjectResult(EMS.EM33);
            }

            var mapperObject = _mapper.Map<AssessmentScheme>(assessment);
            mapperObject.Id = new int();

            _context.Update(syllabus);
            await _context.AddAsync(mapperObject);
            await _context.SaveChangesAsync();

            var result = await GetDetailSyllabus(assessment.SyllabusId);
            return result;
        }


        public async Task<IActionResult> CreateSyllabusFull(CreateSyllabusDto createSyllabusDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var res1 = await CreateSyllabusGeneralTab(createSyllabusDto.GeneralTab);
                    if (res1 is not OkObjectResult)
                    {
                        throw new Exception("cannot save general");
                    }
                    /*var result = await _context.Syllabuses.FirstOrDefaultAsync(x => x.SyllabusCode.ToLower().Equals(createSyllabusDto.GeneralTab.SyllabusCode.ToLower()) && x.SyllabusName.ToLower().Equals(createSyllabusDto.GeneralTab.SyllabusName.ToLower())
                    && x.TechnicalRequirement.ToLower().Contains(createSyllabusDto.GeneralTab.TechnicalRequirement.ToLower()));*/
                    var result = await _syllabus.Get().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    var syllabusId = result.Id;
                    var assessmentMapper = _mapper.Map<AssessmentSchemeRequest>(createSyllabusDto.OtherScreen);
                    assessmentMapper.SyllabusId = syllabusId;
                    CreatedSyllabusOutline outline = new CreatedSyllabusOutline();
                    outline.Id = syllabusId;
                    outline.dayUnits = createSyllabusDto.DayUnits;

                    var res2 = await CreateSyllabusOutline(outline);
                    if (res2.Data == null)
                    {
                        throw new Exception(res2.Errormessge);
                    }
                    if (createSyllabusDto.DayUnits.Any())
                    {
                        foreach (var objectivecode in createSyllabusDto.DayUnits.SelectMany(x => x.trainingUnits).SelectMany(x => x.TrainingContents).Select(x => x.LearningObjectiveCode))
                        {
                            await _context.SyllabusObjectives.AddAsync(new SyllabusObjective
                            {
                                ObjectiveCode = objectivecode,
                                SyllabusId = syllabusId
                            });
                        }
                    }
                    var res3 = await CreateSyllabusOtherScreen(assessmentMapper);

                    if (res3 is BadRequestObjectResult)
                    {
                        var errMessage = res3 as BadRequestObjectResult;
                        var txrt = errMessage.Value.ToString();
                        throw new Exception(txrt);
                    }
                    await transaction.CommitAsync();
                    return new OkObjectResult("add success");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        public async Task<StatusCodeResponse<Syllabus>> ImportExcel(IFormFile file, int userId, string importType, string? scan)
        {
            var response = new StatusCodeResponse<Syllabus>();
            response.Data = new Syllabus();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (file == null || file.Length <= 0)
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.Errormessge = (EMS.EM34);
                return response;
            }

            // Ensure that the uploaded file is an Excel file
            if (!Path.GetFileNameWithoutExtension(file.FileName).Equals("Template_Import_Syllabus", StringComparison.OrdinalIgnoreCase) ||
                !Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                //throw new ArgumentException("Please upload a valid Excel file with the name 'Template_Import_Syllabus.xlsx'.");
                response.statusCode = HttpStatusCode.BadRequest;
                response.Errormessge = (EMS.EM63);
                return response;
            }
            //var syllabus = new Syllabus();
            var assessmentdto = new AssessmentSchemeRequest();
            var learningobject = new LearningObjective();
            var syllbusobject = new SyllabusObjective();
            var assessment = new AssessmentScheme();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;
                    //Import table Syllabus
                    response.Data.TechnicalRequirement = worksheet.Cells["c2"].Value?.ToString();
                    response.Data.SyllabusName = worksheet.Cells[3, 3].Value.ToString();
                    response.Data.SyllabusCode = worksheet.Cells[4, 3].Value.ToString();
                    response.Data.Version = worksheet.Cells[5, 3].Value?.ToString();
                    response.Data.AttendeeNumber = int.Parse(worksheet.Cells[6, 3].Value?.ToString() ?? "0");
                    response.Data.CourseObjective = ExcelHelper.ReadExcelCell(worksheet, 16, 3);
                    response.Data.TrainingMaterials = ExcelHelper.CombineRowsAndCells(worksheet, 17, 19, 3, 4);
                    response.Data.TrainingPrinciples = ExcelHelper.CombineRowsAndCells(worksheet, 25, 31, 3, 4);
                    response.Data.Priority = worksheet.Cells[7, 3].Value?.ToString();
                    response.Data.PublishStatus = 0;
                    response.Data.UserId = userId;
                    response.Data.CreatedBy = (await _userService.GetUser(userId))?.Name;
                    response.Data.CreatedDate = DateTime.Now;
                    var flag = await _syllabusRepo.GetSyllabusIdBySyllabusCodeAndVersion(response.Data.SyllabusCode, response.Data.Version) != 0;
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            switch (importType)
                            {
                                case "allow":
                                    if (flag)
                                    {
                                        response.Data.Version = Math.Round((double.Parse(await _syllabusRepo.GetMaxVersionBySyllabusCode(response.Data.SyllabusCode)) + 0.1), 1).ToString();
                                        await _context.Syllabuses.AddAsync(response.Data);
                                        await _syllabus.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        await _context.Syllabuses.AddAsync(response.Data);
                                        await _syllabus.SaveChangesAsync();
                                    }
                                    break;
                                case "replace":
                                    if (scan.ToUpper().Equals("Code".ToUpper()))
                                    {
                                        if (response.Data.Version != null)
                                        {
                                            var existingSyllabus = new StatusCodeResponse<Syllabus>();
                                            existingSyllabus.Data = await _context.Syllabuses.Where(s => s.SyllabusCode.ToUpper().Equals(response.Data.SyllabusCode.ToUpper())
                                                                                           && s.Version.Equals(response.Data.Version)).FirstOrDefaultAsync();
                                            if (existingSyllabus != null)
                                            {
                                                existingSyllabus.Data.TechnicalRequirement = response.Data.TechnicalRequirement;
                                                existingSyllabus.Data.SyllabusName = response.Data.SyllabusName;
                                                existingSyllabus.Data.AttendeeNumber = response.Data.AttendeeNumber;
                                                existingSyllabus.Data.CourseObjective = response.Data.CourseObjective;
                                                existingSyllabus.Data.TrainingMaterials = response.Data.TrainingMaterials;
                                                existingSyllabus.Data.TrainingPrinciples = response.Data.TrainingPrinciples;
                                                existingSyllabus.Data.Priority = response.Data.Priority;
                                                existingSyllabus.Data.PublishStatus = 0;
                                                existingSyllabus.Data.UserId = userId;
                                                existingSyllabus.Data.CreatedBy = (await _userService.GetUser(userId))?.Name;
                                                existingSyllabus.Data.CreatedDate = DateTime.Now;
                                                //_mapper.Map(existingSyllabus.Data, response.Data);
                                                _context.Update(existingSyllabus.Data);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            response.statusCode = HttpStatusCode.NotFound;
                                            response.Errormessge = EMS.EM64;
                                            return response;
                                        }
                                    }
                                    else if (scan.ToUpper().Equals("Name".ToUpper()))
                                    {
                                        //Same
                                        if (response.Data.Version != null)
                                        {
                                            var existingSyllabus = new StatusCodeResponse<Syllabus>();
                                            existingSyllabus.Data = await _context.Syllabuses.Where(s => s.SyllabusName.ToUpper().Equals(response.Data.SyllabusName.ToUpper())
                                                                                           && s.Version.Equals(response.Data.Version)).FirstOrDefaultAsync();
                                            if (existingSyllabus != null)
                                            {
                                                existingSyllabus.Data.TechnicalRequirement = response.Data.TechnicalRequirement;
                                                existingSyllabus.Data.SyllabusName = response.Data.SyllabusName;
                                                existingSyllabus.Data.AttendeeNumber = response.Data.AttendeeNumber;
                                                existingSyllabus.Data.CourseObjective = response.Data.CourseObjective;
                                                existingSyllabus.Data.TrainingMaterials = response.Data.TrainingMaterials;
                                                existingSyllabus.Data.TrainingPrinciples = response.Data.TrainingPrinciples;
                                                existingSyllabus.Data.Priority = response.Data.Priority;
                                                existingSyllabus.Data.PublishStatus = 0;
                                                existingSyllabus.Data.UserId = userId;
                                                existingSyllabus.Data.CreatedBy = (await _userService.GetUser(userId))?.Name;
                                                existingSyllabus.Data.CreatedDate = DateTime.Now;
                                                //_mapper.Map(existingSyllabus.Data, response.Data);
                                                _context.Update(existingSyllabus.Data);
                                                response.Data.Id = existingSyllabus.Data.Id;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            response.statusCode = HttpStatusCode.NotFound;
                                            response.Errormessge = EMS.EM64;
                                            return response;
                                        }
                                    }
                                    break;
                            }
                            //Import table LearningObjective and SyllabusObjective
                            for (int row = 9; row <= 15; row++)
                            {
                                string Objectcode = worksheet.Cells[row, 4].Value?.ToString();
                                if (Objectcode != null)
                                {
                                    var existingLearningObjective = await _context.LearningObjectives
                                        .FirstOrDefaultAsync(s => s.ObjectiveCode.Equals(Objectcode));
                                    var learningObjective = new LearningObjective();
                                    if (existingLearningObjective == null)
                                    {
                                        learningObjective.ObjectiveCode = worksheet.Cells[row, 4].Value?.ToString();
                                        learningObjective.Description = ExcelHelper.ReadExcelCell(worksheet, row, 5);
                                        await _context.LearningObjectives.AddAsync(learningObjective);
                                    }
                                    var syllabusObjective = new SyllabusObjective
                                    {
                                        ObjectiveCode = Objectcode,
                                        SyllabusId = await _syllabusRepo.GetSyllabusIdBySyllabusCodeAndVersion(response.Data.SyllabusCode, response.Data.Version),
                                    };
                                    switch (importType)
                                    {
                                        case "allow":
                                            await _context.SyllabusObjectives.AddAsync(syllabusObjective);
                                            await _context.SaveChangesAsync();
                                            //await transaction.CommitAsync();
                                            break;
                                        case "replace":
                                            var syllabusId = await _syllabusRepo.GetSyllabusIdBySyllabusCodeAndVersion(response.Data.SyllabusCode, response.Data.Version);
                                            var existingSyllabusObject = await _context.SyllabusObjectives.Where(s => s.SyllabusId == syllabusId).FirstOrDefaultAsync();
                                            if (existingLearningObjective != null)
                                            {
                                                existingLearningObjective.Description = ExcelHelper.ReadExcelCell(worksheet, row, 5);
                                                _context.Update(existingLearningObjective);
                                            }
                                            else
                                            {
                                                await _context.LearningObjectives.AddAsync(learningObjective);
                                            }
                                            if (existingSyllabusObject != null)
                                            {
                                                existingSyllabusObject.ObjectiveCode = Objectcode;
                                                _context.Update(existingSyllabusObject);
                                            }
                                            else
                                            {
                                                await _context.SyllabusObjectives.AddAsync(syllabusObjective);
                                            }
                                            await _context.SaveChangesAsync();
                                            break;
                                    }
                                }
                                else
                                {
                                    //response.statusCode = HttpStatusCode.UnprocessableEntity;
                                    //response.Errormessge = "Learning Objectives is missing!";
                                    //return response;
                                    break;
                                }
                            }
                            //Import table AssessmentSchemeRequest
                            assessment.Quiz = Math.Round(float.Parse(worksheet.Cells["E20"].Value.ToString()) * 100, 2);
                            assessment.Assignment = Math.Round(float.Parse(worksheet.Cells["E21"].Value.ToString()) * 100, 2);
                            assessment.FinalTheory = Math.Round(float.Parse(worksheet.Cells["E22"].Value.ToString()) * 100, 2);
                            assessment.FinalPractice = Math.Round(float.Parse(worksheet.Cells["E23"].Value.ToString()) * 100, 2);
                            assessment.Final = 100 - (assessment.Quiz + assessment.Assignment);
                            assessment.Passing = Math.Round(float.Parse(worksheet.Cells["E24"].Value.ToString()) * 100, 2);
                            if(assessment.FinalTheory + assessment.FinalPractice != 100)
                            {
                                response.statusCode = HttpStatusCode.BadRequest;
                                response.Errormessge = EMS.EM33;
                                return response;
                            }
                            switch (importType)
                            {
                                case "allow":
                                    assessment.SyllabusId = await _syllabusRepo.GetSyllabusIdBySyllabusCodeAndVersion(response.Data.SyllabusCode, response.Data.Version);
                                    await _schemaRepo.AddAsync(assessment);
                                    await _schemaRepo.SaveChangesAsync();
                                    //await transaction.CommitAsync();
                                    break;
                                case "replace":
                                    var syllabusid = await _syllabusRepo.GetSyllabusIdBySyllabusCodeAndVersion(response.Data.SyllabusCode, response.Data.Version);
                                    var existingAssessment = await _context.AssessmentSchemes.Where(x => x.SyllabusId == syllabusid).FirstOrDefaultAsync();
                                    if (existingAssessment != null)
                                    {
                                        existingAssessment.Quiz = assessment.Quiz;
                                        existingAssessment.Assignment = assessment.Assignment;
                                        existingAssessment.FinalTheory = assessment.FinalTheory;
                                        existingAssessment.FinalPractice = assessment.FinalPractice;
                                        existingAssessment.Final = assessment.Final;
                                        existingAssessment.Passing = assessment.Passing;
                                        _schemaRepo.Update(existingAssessment);
                                        await _schemaRepo.SaveChangesAsync();
                                    }
                                    break;
                            }
                            await transaction.CommitAsync();
                            response.statusCode = HttpStatusCode.OK;
                            response.Errormessge = "Successful";
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
            return response;
        }
        public async Task<StatusCodeResponse<CreatedSyllabusOutlineDto>> ImportTrainingExcel(IFormFile file, int syllabusId, string importType)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var response = new StatusCodeResponse<CreatedSyllabusOutlineDto>();
            response.Data = new CreatedSyllabusOutlineDto();
            //var trainingSyllabus = new CreatedSyllabusOutlineDto();
            response.Data.Id = syllabusId;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["<Topics Code>_ScheduleDetail"];
                    if (worksheet == null)
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.Errormessge = EMS.EM66;
                        return response;
                    }
                    int index = 3;
                    string UnitName = null;
                    List<TrainingUnitDto> trainingUnits = new List<TrainingUnitDto>();
                    UnitName = ExcelHelper.ReadExcelCell(worksheet, index, 2);
                    using (var transaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            while (UnitName != null)
                            {
                                TrainingUnitDto trainingUnit = new TrainingUnitDto();
                                trainingUnit.UnitName = UnitName;
                                trainingUnit.DayNumber = int.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 3));
                                trainingUnits.Add(trainingUnit);
                                int count = ExcelHelper.GetMergedCellCount(worksheet, index, 2);
                                List<TrainingContentDto> contentDtos = new List<TrainingContentDto>();
                                for (int i = 0; i < count; i++)
                                {
                                    TrainingContentDto trainingContent = new TrainingContentDto();
                                    trainingContent.ContentName = ExcelHelper.ReadExcelCell(worksheet, index, 4);
                                    trainingContent.LearningObjectiveCode = ExcelHelper.ReadExcelCell(worksheet, index, 5);
                                    var check = await _context.LearningObjectives.Where(x => x.ObjectiveCode.ToUpper().
                                                Equals(trainingContent.LearningObjectiveCode.ToUpper())).FirstOrDefaultAsync();
                                    if (check == null)
                                    {
                                        response.statusCode = HttpStatusCode.NotFound;
                                        response.Errormessge = EMS.EM67;
                                        return response;
                                    }
                                    int Deliver = await _deliveryTypeService.GetIdByName((ExcelHelper.ReadExcelCell(worksheet, index, 6)));
                                    if (Deliver < 0)
                                    {
                                        response.statusCode = HttpStatusCode.NotFound;
                                        response.Errormessge = EMS.EM68;
                                        return response;
                                    }
                                    trainingContent.DeliveryType = Deliver;
                                    trainingContent.Duration = double.Parse(ExcelHelper.ReadExcelCell(worksheet, index, 7));
                                    trainingContent.TrainingFormat = ExcelHelper.ReadExcelCell(worksheet, index, 8);
                                    trainingContent.Note = ExcelHelper.ReadExcelCell(worksheet, index, 9);
                                    index++;
                                    contentDtos.Add(trainingContent);
                                }
                                switch (importType)
                                {
                                    case "allow":
                                        trainingUnit.TrainingContents = contentDtos.ToArray();
                                        response.Data.syllabusOutlines = trainingUnits.ToArray();
                                        await this.AddSyllabusOutline(response.Data);
                                        UnitName = ExcelHelper.ReadExcelCell(worksheet, index, 2);
                                        break;
                                    case "replace":
                                        var existingUnit = await _context.TrainingUnits.FirstOrDefaultAsync(u => u.UnitName.ToUpper().Equals(UnitName.ToUpper()) && u.SyllabusId == syllabusId);
                                        if (existingUnit != null)
                                        {
                                            existingUnit.DayNumber = (int)trainingUnit.DayNumber;
                                            foreach (var content in contentDtos)
                                            {
                                                var existingContent = await _context.TrainingContents.FirstOrDefaultAsync(c => c.ContentName.ToUpper().Equals(content.ContentName.ToUpper()) && c.UnitCode == existingUnit.UnitCode);
                                                if (existingContent != null)
                                                {
                                                    existingContent.LearningObjectiveCode = content.LearningObjectiveCode;
                                                    existingContent.DeliveryType = (int)content.DeliveryType;
                                                    existingContent.Duration = (float)content.Duration;
                                                    existingContent.TrainingFormat = content.TrainingFormat;
                                                    existingContent.Note = content.Note;
                                                    _trainingContentRepo.Update(existingContent);
                                                }
                                                else
                                                {
                                                    existingContent = new TrainingContent
                                                    {
                                                        ContentName = content.ContentName,
                                                        LearningObjectiveCode = content.LearningObjectiveCode,
                                                        DeliveryType = (int)content.DeliveryType,
                                                        Duration = (float)content.Duration,
                                                        TrainingFormat = content.TrainingFormat,
                                                        Note = content.Note,
                                                        UnitCode = existingUnit.UnitCode
                                                    };
                                                    _context.TrainingContents.Add(existingContent);
                                                }
                                            }

                                            await _context.SaveChangesAsync();
                                            UnitName = ExcelHelper.ReadExcelCell(worksheet, index, 2);
                                            response.statusCode = HttpStatusCode.OK;
                                            response.Errormessge = "Update Successful";
                                        }
                                        else
                                        {
                                            response.statusCode = HttpStatusCode.NotFound;
                                            response.Errormessge = EMS.EM69;
                                        }
                                        break;
                                }
                            }
                            response.statusCode = HttpStatusCode.OK;
                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            response.statusCode = HttpStatusCode.BadRequest;
                            response.Errormessge = EMS.EM70;
                        }
                    }
                }
            }
            return response;
        }

        public async Task<IActionResult> DuplicationSyllabus(int id)
        {
            var originSys = await _syllabus.Get().Include(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ThenInclude(x => x.Materials)
                           .Include(x => x.AssessmentScheme).Include(x => x.SyllabusObjectives).FirstOrDefaultAsync(x => x.Id == id);
            if (originSys == null || originSys == default)
            {
                return new NotFoundObjectResult(EMS.EM52 + id);
            }

            var duplicate = originSys;
            duplicate.Id = new int();
            duplicate.CreatedDate = DateTime.Now;
            duplicate.PublishStatus = 0;
            duplicate.ModifiedDate = DateTime.Now;
            duplicate.Version = "1";
            await _syllabus.AddAsync(duplicate);

            if (duplicate.SyllabusObjectives != null && duplicate.SyllabusObjectives.Any())
                foreach (var syllabusObjective in duplicate.SyllabusObjectives)
                {
                    var newSyllabusObjective = syllabusObjective;
                    newSyllabusObjective.Id = new int();
                    await _syllabusObjectiveRepo.AddAsync(newSyllabusObjective);
                }
            if (duplicate.AssessmentScheme != null)
            {
                var schema = duplicate.AssessmentScheme;
                schema.Id = new int();
                await _schemaRepo.AddAsync(schema);
            }

            foreach (var trainingunit in duplicate.TrainingUnits)
            {
                var newUnit = trainingunit;
                newUnit.UnitCode = new int();
                await _trainingUnitRepo.AddAsync(newUnit);

                foreach (var trainingContent in newUnit.TrainingContents)
                {
                    var newContent = trainingContent;
                    newContent.Id = new int();
                    await _trainingContentRepo.AddAsync(newContent);
                    foreach (var material in newContent.Materials)
                    {
                        var newMaterial = material;
                        newMaterial.Id = new int();
                        await _materialRepo.AddAsync(newMaterial);
                    }

                }
            }

            await _syllabus.SaveChangesAsync();

            var justDuplicatedSyllabus = await _syllabus.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
            if (justDuplicatedSyllabus == null) return new BadRequestObjectResult(EMS.EM71);

            var detail = await GetDetailSyllabus(justDuplicatedSyllabus.Id);
            return detail;
        }


        public async Task<StatusCodeResponse<bool>> ImportSyllabusByExcel(IFormFile file, int userId, string importType, string? scan)
        {
            var syllabus = new StatusCodeResponse<Syllabus>();
            var result = new StatusCodeResponse<bool>();
            syllabus = await this.ImportExcel(file, userId, importType, scan);
            if (syllabus.statusCode != HttpStatusCode.OK)
            {
                result.Data = false;
                result.statusCode = syllabus.statusCode;
                result.Errormessge = syllabus.Errormessge;
                return result;
            }
            var createdSyllabusOutlineDto = new StatusCodeResponse<CreatedSyllabusOutlineDto>();
            createdSyllabusOutlineDto = await this.ImportTrainingExcel(file, syllabus.Data.Id, importType);
            if (createdSyllabusOutlineDto.statusCode != HttpStatusCode.OK)
            {
                result.Data = false;
                result.statusCode = createdSyllabusOutlineDto.statusCode;
                result.Errormessge = createdSyllabusOutlineDto.Errormessge;
                return result;
            }
            //var mapper = _mapper.Map<CreatedSyllabusOutlineDto>(syllabus.createdSyllabusOutlineDto.Data);
            result.Data = true;
            result.statusCode = HttpStatusCode.OK;
            result.Errormessge = "Successful";
            return result;
        }

        public async Task<ViewDetailSyllabusDto> AddSyllabusOutline(CreatedSyllabusOutlineDto syllabusRequest)
        {
            try
            {
                Syllabus syllabus = await _syllabusRepo.GetSyllabusByIdAsync(syllabusRequest.Id);
                if (syllabus is null) throw new Exception(EMS.EM72);
                // Get list SyllabusOutline -> for each elements in list
                foreach (TrainingUnitDto syllabusOutline in syllabusRequest.syllabusOutlines)
                {

                    var nTrainingUnit = _mapper.Map<TrainingUnit>(syllabusOutline);
                    nTrainingUnit.SyllabusId = syllabusRequest.Id;

                    var NewUnit = await _trainingUnitService.AddTrainingUnit(nTrainingUnit);

                    if (NewUnit is null)
                    {
                        throw new Exception(EMS.EM73);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
            var result = await GetDetailSyllabus(syllabusRequest.Id);
            var status = result as OkObjectResult;
            var ReturnSyllabus = status.Value ?? new ViewDetailSyllabusDto();
            return (ViewDetailSyllabusDto)ReturnSyllabus;
        }

        // done
        public async Task<IActionResult> GetDetailSyllabus(int syllabusId)
        {
            var outline = await GetOutLineSyllabusBySyllabusId(syllabusId);

            var detailSyllabus = await _syllabus.Get().Include(x => x.SyllabusObjectives).Include(x => x.AssessmentScheme).Where(x => x.Id == syllabusId).FirstOrDefaultAsync();

            if (detailSyllabus == null)
            {
                return new BadRequestObjectResult(EMS.EM52 + syllabusId);
            }
            var returnedDetail = _mapper.Map<ViewDetailSyllabusDto>(detailSyllabus);

            returnedDetail.AssessmentScheme = detailSyllabus.AssessmentScheme != null ? _mapper.Map<AssessmentSchemeDTO>(detailSyllabus.AssessmentScheme) : new AssessmentSchemeDTO() { };
            returnedDetail.Outline = outline;

            return new OkObjectResult(returnedDetail);
        }
        public async Task<bool> DeleteSyllabus(int syllabusId)
        {
            var syllabus = await _syllabus.Get().FirstOrDefaultAsync(x => x.Id == syllabusId);
            if (syllabus == null)
            {
                throw new ArgumentException(EMS.EM64);
            }
            var check = await _syllabus.Get().SelectMany(x => x.TrainingProgramSyllabuses).AnyAsync(x => x.SyllabusId == syllabusId);
            if (check)
            {
                throw new ArgumentException(EMS.EM74);
            }
            var syllabusObj = await _syllabusObjectiveRepo.Get().Where(s => s.SyllabusId==syllabusId).ToListAsync();
            if (syllabusObj != null) { _context?.SyllabusObjectives.RemoveRange(syllabusObj); }
            var unit = await _trainingUnitRepo.Get().Include(x => x.TrainingContents).Include(x=>x.TrainerUnits)
                .Where(u => u.SyllabusId == syllabusId).ToListAsync();
            var content = unit.SelectMany(x => x.TrainingContents);
            var trainer = unit.SelectMany(x => x.TrainerUnits);
            var material = content.SelectMany(x => x.Materials);
            var schema = await _schemaRepo.Get().Where(x => x.SyllabusId == syllabusId).FirstOrDefaultAsync();
            if (material == null && material.Any()) _context?.Materials.RemoveRange(material);
            if (content != null && content.Any()) _context?.TrainingContents?.RemoveRange(content);
            if (trainer != null && trainer.Any()) _context?.ClassTrainerUnits.RemoveRange(trainer);
            if (unit != null && unit.Any()) _context?.TrainingUnits?.RemoveRange(unit);
            if (schema != null) _schemaRepo.Delete(schema);
            _syllabus.Delete(syllabus);
            await _syllabus.SaveChangesAsync();
            return true;
        }
        // done
        public async Task<SyllabusCard[]> GetSyllabusCardWithProgramCode(int programCode)
        {
            var syllabusCards = await _syllabus.Get().SelectMany(x => x.TrainingProgramSyllabuses).Include(x => x.Syllabus).ThenInclude(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents)
                                         .Where(x => x.TrainingProgramCode == programCode)
                                        .ToListAsync();

            return syllabusCards.Select(x => _mapper.Map<SyllabusCard>(x)).ToArray();

        }
        // done
        public async Task<ViewOutlineSyllabus[]> GetOutLineSyllabusBySyllabusId(int syllabusId)
        {
            List<ViewOutlineSyllabus> viewOutlineSyllabi = new List<ViewOutlineSyllabus>();
            var syllabus = await _syllabus.Get().Include(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ThenInclude(x => x.Materials).FirstOrDefaultAsync(x => x.Id == syllabusId);
            if (syllabus == null) throw new Exception(EMS.EM52 + syllabusId);
            if (syllabus.TrainingUnits == null || !syllabus.TrainingUnits.Any()) return new ViewOutlineSyllabus[] { };
            var dayNumbers = syllabus.TrainingUnits.Select(x => x.DayNumber).OrderBy(x => x).Distinct();
            foreach (var dayNumber in dayNumbers)
            {
                List<TrainingUnitCard> unitCardsList = new List<TrainingUnitCard>();
                var trainingUnit = syllabus.TrainingUnits.Where(x => x.DayNumber == dayNumber).ToList();
                ViewOutlineSyllabus outline = new ViewOutlineSyllabus() { DayNumber = dayNumber };
                foreach (var unit in trainingUnit)
                {
                    var contentCards = (syllabus.TrainingUnits.SelectMany(x => x.TrainingContents).Where(x => x.UnitCode == unit.UnitCode)).Select(x => _mapper.Map<TrainingContentCard>(x)).ToArray();
                    TrainingUnitCard unitCard = _mapper.Map<TrainingUnitCard>(unit);
                    unitCard.TrainingContents = contentCards;
                    unitCardsList.Add(unitCard);
                }
                outline.TrainingUnits = unitCardsList.ToArray();
                viewOutlineSyllabi.Add(outline);
            }
            return viewOutlineSyllabi.ToArray();
        }

        public async Task<IActionResult> UpdateSyllabus(UpdateSyllabusRequest request)
        {
            var originSyllabus = await _syllabus.Get().Include(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ThenInclude(x => x.Materials).Include(x => x.SyllabusObjectives).Include(x => x.AssessmentScheme)
                                                      .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (originSyllabus == null) return new BadRequestObjectResult(EMS.EM52 + request.Id);

            _mapper.Map(request, originSyllabus);
            originSyllabus.Version = (Double.Parse(originSyllabus.Version ?? "0") + 0.1).ToString();
            _syllabus.Update(originSyllabus);

            if ((await UpdateOutlineOnly(request.Outline, request.Id)) == false) return new BadRequestObjectResult(EMS.EM75);

            var schema = _mapper.Map<AssessmentScheme>(request.Schema);
            var originSchema = await _schemaRepo.Get().Where(x => x.SyllabusId == request.Id).FirstOrDefaultAsync();

            if (originSchema == null)
            {
                schema.Id = new int();
                schema.SyllabusId = request.Id;

                await _schemaRepo.AddAsync(schema);
            }
            else
            {
                originSchema.Quiz = schema.Quiz;
                originSchema.Assignment = schema.Assignment;
                originSchema.FinalPractice = schema.FinalPractice;
                originSchema.Final = schema.Final;
                originSchema.FinalTheory = schema.FinalTheory;
                originSchema.Passing = schema.Passing;
                _schemaRepo.Update(originSchema);
            }

            var oldSyllbausObjectives = await _syllabusObjectiveRepo.Get().Where(x => x.SyllabusId == request.Id).ToListAsync();
            _context.RemoveRange(oldSyllbausObjectives);

            var syllabusObjectives = await _syllabus.Get().Where(x=>x.Id== request.Id).SelectMany(x=>x.TrainingUnits).SelectMany(x=>x.TrainingContents).Select(x=>x.LearningObjectiveCode).Distinct()
                .Select(x => new SyllabusObjective { Id = new int(), ObjectiveCode = x, SyllabusId = request.Id }).ToListAsync();
            syllabusObjectives.ToList().ForEach(x => originSyllabus.SyllabusObjectives?.Add(x));

            _syllabusObjectiveRepo.AddRangeAsync(syllabusObjectives);

            await _syllabus.SaveChangesAsync();

            return await GetDetailSyllabus(request.Id);
        }

        private async Task<bool> UpdateOutlineOnly(UpdateOutlineDTO[] dto, int syllabusId)
        {
            try
            {
                var originSyllabus = await _syllabus.Get().Include(x => x.TrainingUnits).ThenInclude(x => x.TrainingContents).ThenInclude(x => x.Materials).Include(x => x.TrainingUnits).ThenInclude(x => x.TrainerUnits)
                                                          .FirstOrDefaultAsync(x => x.Id == syllabusId);
                if (originSyllabus == null) return false;

                var removedTrainingContentId = originSyllabus?.TrainingUnits?.SelectMany(x => x.TrainingContents).Select(x => x.Id).Except(dto.SelectMany(x => x.TrainingUnits).SelectMany(x => x.TrainingContents).Select(x => x.Id ?? 0));
                var removedTrainingUnitCode = originSyllabus?.TrainingUnits?.Select(x => x.UnitCode).Except(dto.SelectMany(x => x.TrainingUnits).Select(x => x.UnitCode ?? 0));
                var removedMaterialUrl = originSyllabus?.TrainingUnits?.SelectMany(x => x.TrainingContents).SelectMany(x => x.Materials).Select(x => x.Url).Except(dto.SelectMany(x => x.TrainingUnits).SelectMany(x => x.TrainingContents).SelectMany(x => x.Materials).Select(x => x.Url));
                if (removedMaterialUrl != null && removedMaterialUrl.Any())
                {
                    var removedMaterial = originSyllabus?.TrainingUnits?.SelectMany(x => x.TrainingContents).SelectMany(x => x.Materials).Where(x => removedMaterialUrl.Contains(x.Url));
                    if (removedMaterial != null) foreach (var material in removedMaterial)
                        {
                            _materialRepo.Delete(material);
                            await _materialRepo.SaveChangesAsync();
                        }
                }
                if (removedTrainingContentId != null && removedTrainingContentId.Any())
                {
                    var removedTrainingContent = originSyllabus?.TrainingUnits?.SelectMany(x => x.TrainingContents).Where(x => removedTrainingContentId.Contains(x.Id));

                    if (removedTrainingContent != null) foreach (var content in removedTrainingContent)
                        {
                            _trainingContentRepo.Delete(content);
                            await _trainingContentRepo.SaveChangesAsync();
                        }
                }
                if (removedTrainingUnitCode != null && removedTrainingUnitCode.Any())
                {
                    var removedTrainerUnit = originSyllabus?.TrainingUnits?.SelectMany(x => x.TrainerUnits).Where(x => removedTrainingUnitCode.Contains(x.UnitCode));
                    if (removedTrainerUnit != null) foreach (var trainer in removedTrainerUnit)
                        {
                            _trainerUnitRepo.Delete(trainer);
                            await _trainerUnitRepo.SaveChangesAsync();
                        }
                    var removedTrainingUnit = originSyllabus?.TrainingUnits?.Where(x => removedTrainingUnitCode.Contains(x.UnitCode));
                    if (removedTrainingUnit != null) foreach (var unit in removedTrainingUnit)
                        {
                            _trainingUnitRepo.Delete(unit);
                            await _trainingUnitRepo.SaveChangesAsync();
                        }
                }

                foreach (var day in dto)
                {
                    if (day.TrainingUnits != null && day.TrainingUnits.Any())
                        foreach (var unit in day.TrainingUnits)
                        {

                            var oldUnit = await _trainingUnitRepo.Get().Include(x => x.TrainingContents).ThenInclude(x => x.Materials).FirstOrDefaultAsync(x => x.UnitCode == unit.UnitCode);
                            var unitCode = 0;
                            if (oldUnit != null)
                            {
                                oldUnit.UnitName = unit.UnitName ?? oldUnit.UnitName;
                                oldUnit.SyllabusId = syllabusId;
                                oldUnit.DayNumber = day.DayNumber;
                                _trainingUnitRepo.Update(oldUnit);
                                await _trainerUnitRepo.SaveChangesAsync();
                                unitCode = oldUnit.UnitCode;
                            }
                            else
                            {
                                var newUnit = _mapper.Map<TrainingUnit>(unit);
                                newUnit.DayNumber = day.DayNumber;
                                newUnit.SyllabusId = syllabusId;
                                newUnit.TrainingContents = null;
                                await _trainingUnitRepo.AddAsync(newUnit);
                                await _trainerUnitRepo.SaveChangesAsync();
                                unitCode = await _trainingUnitRepo.Get().OrderByDescending(x => x.UnitCode).Select(x => x.UnitCode).FirstOrDefaultAsync();

                            }
                            if (unit.TrainingContents != null && unit.TrainingContents.Any())
                                foreach (var content in unit.TrainingContents)
                                {
                                    var oldContent = await _trainingContentRepo.Get().Include(x => x.Materials).FirstOrDefaultAsync(x => x.Id == content.Id);
                                    var contentId = 0;
                                    if (oldContent != null)
                                    {
                                        _mapper.Map(content, oldContent);
                                        oldContent.UnitCode = unitCode;
                                        contentId = oldContent.Id;
                                        _trainingContentRepo.Update(oldContent);
                                        await _trainingContentRepo.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        var newContent = _mapper.Map<TrainingContent>(content);
                                        newContent.UnitCode = unitCode;
                                        await _trainingContentRepo.AddAsync(newContent);
                                        await _trainingContentRepo.SaveChangesAsync();
                                        contentId = await _trainingContentRepo.Get().OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
                                    }
                                    if (content.Materials != null && content.Materials.Any())
                                    {
                                        foreach (var material in content.Materials)
                                        {
                                            var oldMaterial = originSyllabus.TrainingUnits.SelectMany(x => x.TrainingContents).SelectMany(x => x.Materials).FirstOrDefault(x => x.Url.Equals(material.Url));
                                            if (oldMaterial == null)
                                            {
                                                var newMaterial = _mapper.Map<Material>(material);
                                                newMaterial.ContentId = contentId;
                                                await _materialRepo.AddAsync(newMaterial);
                                                await _materialRepo.SaveChangesAsync();
                                            }
                                        }
                                    }
                                }
                        }

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw new Exception($"{e}");
            }

        }

        public async Task<StatusCodeResponse<ViewDetailSyllabusDto>> CreateSyllabusOutline(CreatedSyllabusOutline createdSyllabusOutline)
        {
            CreatedSyllabusOutlineDto createSyllabusDto = new CreatedSyllabusOutlineDto();
            var result = new StatusCodeResponse<ViewDetailSyllabusDto>();
            createSyllabusDto.Id = createdSyllabusOutline.Id;
            List<TrainingUnitDto> UnitDto = new List<TrainingUnitDto>();
            foreach (var day in createdSyllabusOutline.dayUnits)
            {
                foreach (var trainingUnit in day.trainingUnits)
                {
                    var trainingUnitDto = _mapper.Map<TrainingUnitDto>(trainingUnit);
                    trainingUnitDto.DayNumber = day.DayNumber;
                    UnitDto.Add(trainingUnitDto);
                }
            }
            createSyllabusDto.syllabusOutlines = UnitDto.ToArray();
            var returnobj = await AddSyllabusOutline(createSyllabusDto);
            if (returnobj != null)
            {
                result.statusCode = HttpStatusCode.OK;
                result.Data = returnobj;
                result.Errormessge = "successful";
                return result;
            }
            else
            {
                result.statusCode = HttpStatusCode.BadRequest;
                result.Data = null;
                result.Errormessge = EMS.EM76;
                return result;
            }
        }

        public async Task<IActionResult> ChangeStatusSyllabus(int syllabusCode, int status)
        {
            var syllabus = await _syllabus.GetById(syllabusCode);
            if (syllabus == null) return new BadRequestObjectResult(EMS.EM52);
            syllabus.PublishStatus = status;
            _syllabus.Update(syllabus);
            await _syllabus.SaveChangesAsync();
            return new OkObjectResult(new { SyllabusCode = syllabusCode, Status = status });
        }
    }
}



