using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Core.Repositories.Interfaces;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using FAMS.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Drawing.Printing;
using System.Globalization;


namespace FAMS.Test.Services
{
    [TestFixture]
    public class TrainingProgramServiceTests
    {
        private Mock<ITrainingProgramService> _mockprogramservice;
        private Mock<IBaseRepository<TrainingProgram>> _mockprogramrepo;
        private Mock<IMapper> _mockMapper;
        private Mock<IBaseRepository<TrainingProgramSyllabus>> _programSyllabusRepoMock;
        private Mock<IBaseRepository<Syllabus>> _syllabusRepoMock;
        private Mock<IBaseRepository<Class>> _classRepoMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<ISyllabusRepo> _syllabusRepo1Mock;
        private Mock<IClassService> _classServiceMock;
        private Mock<ISyllabusService> _syllabusServiceMock;
        private Mock<FamsContext> _contextMock;
        private ITrainingProgramService _trainingProgramService;

        [SetUp]
        public void SetUp()
        {
            _mockprogramservice = new Mock<ITrainingProgramService>();
            _mockprogramrepo = new Mock<IBaseRepository<TrainingProgram>>();
            _mockMapper = new Mock<IMapper>();
            _programSyllabusRepoMock = new Mock<IBaseRepository<TrainingProgramSyllabus>>();
            _syllabusRepoMock = new Mock<IBaseRepository<Syllabus>>();
            _classRepoMock = new Mock<IBaseRepository<Class>>();
            _userServiceMock = new Mock<IUserService>();
            _syllabusRepo1Mock = new Mock<ISyllabusRepo>();
            _classServiceMock = new Mock<IClassService>();
            _syllabusServiceMock = new Mock<ISyllabusService>();
            _contextMock = new Mock<FamsContext>();
            _trainingProgramService = new TrainingProgramService(
            _mockprogramrepo.Object,
           _programSyllabusRepoMock.Object,
           _mockMapper.Object,
           _syllabusRepoMock.Object,
           _classRepoMock.Object,
           _contextMock.Object,
           _userServiceMock.Object,
           _syllabusRepo1Mock.Object,
          _classServiceMock.Object,
          _syllabusServiceMock.Object);

          _mockMapper.SetupMapperToTrainingProgram();
        }
        [Test]
        public async Task ChangStatusTrainingProgram_ReturnOK()
        {
            int id = 1;
            int status = 1;
            var training = new TrainingProgram
            {
                TrainingProgramCode = id,
                Status = 2
            };
            var mock = new List<TrainingProgram> { training }.BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);
            var result = await _trainingProgramService.ChangeStatusTrainingProgram(id, status);
            Assert.IsNotNull(result);
            Assert.IsFalse(training==result);
        }

        [Test]
        public async Task ChangStatusTrainingProgram_ReturnNotFoundId()
        {
            int id = 99;
            int status = 1;
            var training = new TrainingProgram
            {
                TrainingProgramCode = 1,
                Status = 1
            };
            var mock = new List<TrainingProgram>().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);
            var result = await _trainingProgramService.ChangeStatusTrainingProgram(id, status);
            Assert.IsNotNull(result);
            Assert.IsEmpty(mock.ToList());
        }

        [Test]
        public async Task SearchTrainingProgram_ReturnsOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var searchString = "test";
            var startDateBegin = "2022-01-01";
            var startDateEnd = "2022-12-31";
            var sortBy = "Name";
            var typeSort = "asc";
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { TrainingProgramCode = 1, TopicCode = "TP001", Name = "Test Program 1", CreatedBy = "User1", Duration = 5, CreatedDate = new DateTime(2022, 1, 1), Status = 1 },
                new TrainingProgram { TrainingProgramCode = 2, TopicCode = "TP002", Name = "Test Program 2", CreatedBy = "User2", Duration = 10, CreatedDate = new DateTime(2022, 2, 1), Status = 2 }
            };
            var mock = trainingPrograms.BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);
            var result = await _trainingProgramService.SearchTrainingProgram(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, typeSort);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
            var response = (ViewListResponse)okResult.Value;
            Assert.AreEqual(1, response.PageNumber);
            Assert.AreEqual(10, response.PageSize);
            Assert.AreEqual(1, response.TotalPage);
            Assert.AreEqual(2, response.List.Length);
        }
        [Test]
        public async Task SearchTrainingProgram_SearchBySearchString_ReturnsOk()
        {
            // Arrange
            var searchString = "1";
            var sortBy = "TrainingProgramCode";
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { TrainingProgramCode = 1, TopicCode = "TP001", Name = "Test Program 1", CreatedBy = "User1", Duration = 5, CreatedDate = new DateTime(2022, 1, 1), Status = 1 },
                new TrainingProgram { TrainingProgramCode = 2, TopicCode = "TP002", Name = "Test Program 2", CreatedBy = "User2", Duration = 10, CreatedDate = new DateTime(2022, 2, 1), Status = 2 }
            };
            var mock = trainingPrograms.BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);
            var result = await _trainingProgramService.SearchTrainingProgram(null, null, searchString, null, null, sortBy, null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
            var response = (ViewListResponse)okResult.Value;
            Assert.AreEqual(1, response.List.Length);
        }
        [Test]
        public async Task SearchTrainingProgram_SearchNoInput_ReturnsOk()
        {
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { TrainingProgramCode = 1, TopicCode = "TP001", Name = "Test Program 1", CreatedBy = "User1", Duration = 5, CreatedDate = new DateTime(2022, 1, 1), Status = 1 },
                new TrainingProgram { TrainingProgramCode = 2, TopicCode = "TP002", Name = "Test Program 2", CreatedBy = "User2", Duration = 10, CreatedDate = new DateTime(2022, 2, 1), Status = 2 },
                new TrainingProgram { TrainingProgramCode = 3, TopicCode = "TP003", Name = "Test Program 3", CreatedBy = "User3", Duration = 10, CreatedDate = new DateTime(2022, 2, 1), Status = 2 }
            };
            var mock = trainingPrograms.BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);
            var result = await _trainingProgramService.SearchTrainingProgram(null, null, null, null, null, null, null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
            var response = (ViewListResponse)okResult.Value;
            Assert.AreEqual(1, response.PageNumber);
            Assert.AreEqual(3, response.List.Length);
        }

        [Test]
        public async Task DuplicateTrainingProgram()
        {
            int id = 2;
            var mock = new List<TrainingProgram>().BuildMock();
            _mockprogramrepo.Setup(x => x.Get())
                .Returns(mock);

            
        }

        [Test]
        public async Task UpdateTrainingProgram()
        {
            // Arrange
            var trainingRequest = new TrainingProgramDtoRequest
            {
                TrainingProgramCode = 1,
                Name = "Test",
                UserId = 1,
                StartTime = DateTimeOffset.UtcNow,
                Duration = 1,
                TopicCode = "Test",
                Status = 1,
                ModifiedBy = "Test",
                ModifiedDate = DateTimeOffset.UtcNow,
                TrainingProgramSyllabus = new CreateTrainingProgramSyllabusDTO[]
                {
                    new CreateTrainingProgramSyllabusDTO
                    {
                        Sequence = 1,
                        SyllabusId = 1
                    }
                }
            };
            var mock = new List<TrainingProgram>().BuildMock();
            // Mock the result of successful update
            _mockprogramrepo.Setup(x => x.Get()).Returns(mock);

            // Act
            var result = await _trainingProgramService.UpdateTrainingProgram(trainingRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<TrainingProgramDtoRequest>(result);
        }
        [Test]
        public async Task SearchTrainingProgram_SearchString_ReturnHavedValue()
        {
            // Arrange
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);

            // Act
            var result = await _trainingProgramService.SearchTrainingProgram(null, null, "test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultStatus = result as OkObjectResult;
            Assert.IsNotNull(resultStatus);

            var viewListResponse = resultStatus.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var trainingPrograms = viewListResponse.List as ViewListTrainingProgramDTO[];
            Assert.IsNotNull(trainingPrograms);
           Assert.IsTrue(trainingPrograms.All(x=>x.Name.ToLower().Contains("test")));
        }

        [Test]
        public async Task SearchTrainingProgram_Date_ReturnHavedValue()
        {
            // Arrange
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);

            // Act
            var result = await _trainingProgramService.SearchTrainingProgram(null, null, null, "2024-01-03", "2024-01-03");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultStatus = result as OkObjectResult;
            Assert.IsNotNull(resultStatus);

            var viewListResponse = resultStatus.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var trainingPrograms = viewListResponse.List as ViewListTrainingProgramDTO[];
            Assert.IsNotNull(trainingPrograms);

            Assert.IsTrue(trainingPrograms.All(x => x.CreatedDate.Equals("03/01/2024")));
        }

        [Test]
        public async Task SearchTrainingProgram_NoCondition_ReturnHavedValue()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            var result = await _trainingProgramService.SearchTrainingProgram();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultStatus = result as OkObjectResult;
            Assert.IsNotNull(resultStatus);
            var viewListReponse = resultStatus.Value as ViewListResponse;
            Assert.IsNotNull(viewListReponse);
            var trainingProgram = viewListReponse.List as ViewListTrainingProgramDTO[];
            Assert.IsNotNull(trainingProgram);
            Assert.IsTrue(viewListReponse.TotalPage == 1);
            Assert.IsTrue(viewListReponse.PageNumber == 1);
            Assert.IsTrue(viewListReponse.PageSize == 10);   
            Assert.IsTrue(trainingProgram.Any()&&trainingProgram.Length<10);
        }
        [Test]
        public async Task SearchTrainingProgram_Pagination_ReturnHavedValue()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            var result = await _trainingProgramService.SearchTrainingProgram(1,1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultStatus = result as OkObjectResult;
            Assert.IsNotNull(resultStatus);
            var viewListReponse = resultStatus.Value as ViewListResponse;
            Assert.IsNotNull(viewListReponse);
            var trainingProgram = viewListReponse.List as ViewListTrainingProgramDTO[];
            Assert.IsNotNull(trainingProgram);
            Assert.IsTrue(viewListReponse.TotalPage == 2);
            Assert.IsTrue(viewListReponse.PageNumber == 1);
            Assert.IsTrue(viewListReponse.PageSize == 1);
            Assert.IsTrue(trainingProgram.Any() && trainingProgram.Length ==1);
        }
        [Test]
        public async Task SearchTrainingProgram_Sort_ReturnHavedValue()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            var result = await _trainingProgramService.SearchTrainingProgram(null, null, null, null, null, "trainingprogramcode", "desc");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultStatus = result as OkObjectResult;
            Assert.IsNotNull(resultStatus);
            var viewListReponse = resultStatus.Value as ViewListResponse;
            Assert.IsNotNull(viewListReponse);
            var trainingProgram = viewListReponse.List as ViewListTrainingProgramDTO[];
            Assert.IsNotNull(trainingProgram);
            for (int i = 1; i < trainingProgram.Length; i++)
            {
                int currentSyllabusCode = trainingProgram[i].TrainingProgramCode;
                int previousSyllabusCode = trainingProgram[i - 1].TrainingProgramCode;
                Assert.IsTrue(currentSyllabusCode<= previousSyllabusCode);
            }
        }
        [Test]
        public async Task GetDetailTrainingProgramByCode_Success()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _syllabusServiceMock.Setup(x => x.GetSyllabusCardWithProgramCode(It.IsAny<int>())).ReturnsAsync(new SyllabusCard[] { new SyllabusCard() { Id=1,ModifiedBy="2"}
            });
            _classServiceMock.Setup(x=>x.GetByTrainingProgramCode(It.IsAny<int>())).ReturnsAsync(new ViewListClassDTO[] { });
            _programSyllabusRepoMock.Setup(x => x.Get()).Returns(new List<TrainingProgramSyllabus>() { }.BuildMock());
            var result = await _trainingProgramService.ViewDetailTrainingProgram(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var resultStatus = result as OkObjectResult;
            var obj = resultStatus.Value as ViewDetailTrainingProgramDTO;
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.TrainingProgramCode == 1);


        }
        [Test]
        public async Task GetDetailTrainingProgramByCode_NotSuccess_InvalidId()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _syllabusServiceMock.Setup(x => x.GetSyllabusCardWithProgramCode(It.IsAny<int>())).ReturnsAsync(new SyllabusCard[] { new SyllabusCard() { Id=1,ModifiedBy="2"}
            });
            _classServiceMock.Setup(x => x.GetByTrainingProgramCode(It.IsAny<int>())).ReturnsAsync(new ViewListClassDTO[] { });
            _programSyllabusRepoMock.Setup(x => x.Get()).Returns(new List<TrainingProgramSyllabus>() { }.BuildMock());
            var result = await _trainingProgramService.ViewDetailTrainingProgram(-1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var resultStatus = result as BadRequestObjectResult;
            var obj = resultStatus.Value as string;
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj==EMS.EM54);
        }
        [Test]
        public async Task GetDetailTrainingProgramByCode_NotSuccess_NotFound()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _syllabusServiceMock.Setup(x => x.GetSyllabusCardWithProgramCode(It.IsAny<int>())).ReturnsAsync(new SyllabusCard[] { new SyllabusCard() { Id=1,ModifiedBy="2"}
            });
            _classServiceMock.Setup(x => x.GetByTrainingProgramCode(It.IsAny<int>())).ReturnsAsync(new ViewListClassDTO[] { });
            _programSyllabusRepoMock.Setup(x => x.Get()).Returns(new List<TrainingProgramSyllabus>() { }.BuildMock());
            var result = await _trainingProgramService.ViewDetailTrainingProgram(33);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var resultStatus = result as BadRequestObjectResult;
            var obj = resultStatus.Value as string;
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj == EMS.EM53 + 33);
        }
        [Test]
        public async Task CreateTrainingProgram_Success()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _mockprogramrepo.Setup(x => x.AddAsync(It.IsAny<TrainingProgram>(), default)).Callback((TrainingProgram src ,CancellationToken token)=> _mockprogramrepo.Setup(x => x.Get()).Returns(data.Append(src)));
            _classRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) =>null);
            _classRepoMock.Setup(x => x.Update(It.IsAny<Class>())).Callback((Class classs)=>{}) ;
            _programSyllabusRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);
            _programSyllabusRepoMock.Setup(x => x.Update(It.IsAny<TrainingProgramSyllabus>())).Callback((TrainingProgramSyllabus classs) => { });
            _classRepoMock.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _programSyllabusRepoMock.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _mockprogramrepo.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _syllabusServiceMock.Setup(x => x.GetSyllabusCardWithProgramCode(It.IsAny<int>())).ReturnsAsync(new SyllabusCard[] { new SyllabusCard() { Id=1,ModifiedBy="2"}
            });
            _classServiceMock.Setup(x => x.GetByTrainingProgramCode(It.IsAny<int>())).ReturnsAsync(new ViewListClassDTO[] { });
            _programSyllabusRepoMock.Setup(x => x.Get()).Returns(new List<TrainingProgramSyllabus>() { }.BuildMock());

            CreateTrainingProgramDTO obj = new CreateTrainingProgramDTO() 
            {
                UserId = 1,
                CreatedBy = "2",
                Duration=2,
                Name="aaaaaaaa",
                StartTime=DateTime.Now.ToString(Value.DateFormat),
                Status=1,
                TopicCode="ASS",
            };
            var result = await _trainingProgramService.Create(obj);
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task CreateTrainingProgram_NoSuccess_NotFoundSyllabus()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _mockprogramrepo.Setup(x => x.AddAsync(It.IsAny<TrainingProgram>(), default)).Callback((TrainingProgram src, CancellationToken token) => _mockprogramrepo.Setup(x => x.Get()).Returns(data.Append(src)));
            _classRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);
            _classRepoMock.Setup(x => x.Update(It.IsAny<Class>())).Callback((Class classs) => { });
            _programSyllabusRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);
            _programSyllabusRepoMock.Setup(x => x.Update(It.IsAny<TrainingProgramSyllabus>())).Callback((TrainingProgramSyllabus classs) => { });
            _classRepoMock.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _programSyllabusRepoMock.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _mockprogramrepo.Setup(x => x.SaveChangesAsync(default)).Callback((CancellationToken token) => { });
            _syllabusServiceMock.Setup(x => x.GetSyllabusCardWithProgramCode(It.IsAny<int>())).ReturnsAsync(new SyllabusCard[] { new SyllabusCard() { Id=1,ModifiedBy="2"}
            });
            _classServiceMock.Setup(x => x.GetByTrainingProgramCode(It.IsAny<int>())).ReturnsAsync(new ViewListClassDTO[] { });
            _programSyllabusRepoMock.Setup(x => x.Get()).Returns(new List<TrainingProgramSyllabus>() { }.BuildMock());

            CreateTrainingProgramDTO obj = new CreateTrainingProgramDTO()
            {
                UserId = 1,
                CreatedBy = "2",
                Duration = 2,
                Name = "aaaaaaaa",
                StartTime = DateTime.Now.ToString(Value.DateFormat),
                Status = 1,
                TopicCode = "ASS",
                ClassIds = new[] { 1, 2},
                SyllabusDTOs = new CreateTrainingProgramSyllabusDTO[] { new CreateTrainingProgramSyllabusDTO() { Sequence=1,SyllabusId=1} }
            };
            var result = await _trainingProgramService.Create(obj);
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var mess = (result as BadRequestObjectResult).Value as string;
            Assert.AreEqual(mess, EMS.EM53 + 1);
        }
        [Test]
        public async Task CreateTrainingProgram_NoSuccess_NotFoundClass()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _mockprogramrepo.Setup(x => x.AddAsync(It.IsAny<TrainingProgram>(), default)).Callback((TrainingProgram src, CancellationToken token) => _mockprogramrepo.Setup(x => x.Get()).Returns(data.Append(src)));
            _classRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);
            _programSyllabusRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);

            CreateTrainingProgramDTO obj = new CreateTrainingProgramDTO()
            {
                UserId = 1,
                CreatedBy = "2",
                Duration = 2,
                Name = "aaaaaaaa",
                StartTime = DateTime.Now.ToString(Value.DateFormat),
                Status = 1,
                TopicCode = "ASS",
                ClassIds = new[] { 1 }
            };
            var result = await _trainingProgramService.Create(obj);
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var mess = (result as BadRequestObjectResult).Value as string;
            Assert.AreEqual(mess, EMS.EM50 + 1);
        }
        [Test]
        public async Task CreateTrainingProgram_NoSuccess_InvalidClassIds()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _mockprogramrepo.Setup(x => x.AddAsync(It.IsAny<TrainingProgram>(), default)).Callback((TrainingProgram src, CancellationToken token) => _mockprogramrepo.Setup(x => x.Get()).Returns(data.Append(src)));
            _classRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);
            _programSyllabusRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);

            CreateTrainingProgramDTO obj = new CreateTrainingProgramDTO()
            {
                UserId = 1,
                CreatedBy = "2",
                Duration = 2,
                Name = "aaaaaaaa",
                StartTime = DateTime.Now.ToString(Value.DateFormat),
                Status = -1,
                TopicCode = "ASS",
                ClassIds = new[] { -1 }
            };
            var result = await _trainingProgramService.Create(obj);
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var mess = (result as BadRequestObjectResult).Value as string;
            Assert.AreEqual(mess, EMS.EM54 + (-1).ToString());
        }
        [Test]
        public async Task CreateTrainingProgram_NoSuccess_StartDateInValid()
        {
            var data = TestHelper.GetFakeTrainingProgram().BuildMock();
            _mockprogramrepo.Setup(x => x.Get()).Returns(data);
            _mockprogramrepo.Setup(x => x.AddAsync(It.IsAny<TrainingProgram>(), default)).Callback((TrainingProgram src, CancellationToken token) => _mockprogramrepo.Setup(x => x.Get()).Returns(data.Append(src)));
            _classRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).ReturnsAsync((object a, CancellationToken token) => new Class() { StartDate=DateTimeOffset.Parse("2024-01-01")});
            _programSyllabusRepoMock.Setup(x => x.GetById(It.IsAny<object>(), default)).Returns((object a, CancellationToken token) => null);

            CreateTrainingProgramDTO obj = new CreateTrainingProgramDTO()
            {
                UserId = 1,
                CreatedBy = "2",
                Duration = 2,
                Name = "aaaaaaaa",
                StartTime = DateTime.Now.ToString(Value.DateFormat),
                Status = -1,
                TopicCode = "ASS",
                ClassIds = new[] { 1 }
            };
            var result = await _trainingProgramService.Create(obj);
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var mess = (result as BadRequestObjectResult).Value as string;
            Assert.AreEqual(mess, "The registered class must be not started. "+1);
        }
        [Test]
        public async Task Duplicate_BadRequet()
        {

        }
    }
}
