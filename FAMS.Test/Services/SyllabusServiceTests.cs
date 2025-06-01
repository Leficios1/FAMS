using AutoMapper;
using FAMS.Api.Dtos;
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
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Services
{
    [TestFixture]

    public class SyllabusServiceTests
    {
        private Mock<IBaseRepository<Syllabus>> _mockSyllabusRepository;
        private Mock<FamsContext> _mockContext;
        private Mock<IMapper> _mockMapper;
        private Mock<ISyllabusRepo> _mockSyllabusRepo;
        private Mock<IUserService> _mockUserService;
        private Mock<ITrainingUnitService> _mockTrainingUnitService;
        private Mock<ITrainingContentService> _mockTrainingContentService;
        private Mock<IBaseRepository<TrainingContent>> _mockTrainingContentRepo;
        private Mock<IBaseRepository<TrainingUnit>> _mockTrainingUnitRepo;
        private Mock<IBaseRepository<SyllabusObjective>> _mockSyllabusObjectiveRepo;
        private Mock<IBaseRepository<DeliveryType>> _mockDeliveryRepo;
        private Mock<IBaseRepository<LearningObjective>> _mockLearningObjectRepo;
        private Mock<IDeliveryTypeService> _mockDeliveryTypeService;
        private Mock<IBaseRepository<AssessmentScheme>> _mockSchemaRepo;
        private Mock<IMaterialService> _mockMaterialService;
        private Mock<IBaseRepository<Material>> _mockMaterialRepo;
        private Mock<IBaseRepository<ClassUser>> _mockClassUserRepo;
        private Mock<IBaseRepository<ClassTrainerUnit>> _mockClassTrainerUnitRepo;
        private SyllabusService _syllabusService;

        private List<Syllabus> data = new List<Syllabus>();

        [SetUp]
        public void Init()
        {
            _mockSyllabusRepository = new Mock<IBaseRepository<Syllabus>>();
            _mockContext = new Mock<FamsContext>();
            _mockMapper = new Mock<IMapper>();
            _mockSyllabusRepo = new Mock<ISyllabusRepo>();
            _mockUserService = new Mock<IUserService>();
            _mockTrainingUnitService = new Mock<ITrainingUnitService>();
            _mockTrainingContentService = new Mock<ITrainingContentService>();
            _mockTrainingContentRepo = new Mock<IBaseRepository<TrainingContent>>();
            _mockDeliveryRepo = new Mock<IBaseRepository<DeliveryType>>();
            _mockLearningObjectRepo = new Mock<IBaseRepository<LearningObjective>>();
            _mockSyllabusObjectiveRepo = new Mock<IBaseRepository<SyllabusObjective>>();
            _mockTrainingUnitRepo = new Mock<IBaseRepository<TrainingUnit>>();
            _mockMaterialService = new Mock<IMaterialService>();
            _mockMaterialRepo = new Mock<IBaseRepository<Material>>();
            _mockDeliveryTypeService = new Mock<IDeliveryTypeService>();
            _mockSchemaRepo = new Mock<IBaseRepository<AssessmentScheme>>();
            _mockClassUserRepo = new Mock<IBaseRepository<ClassUser>>();
            _mockClassTrainerUnitRepo = new Mock<IBaseRepository<ClassTrainerUnit>>();
            _syllabusService = new SyllabusService(
                _mockSyllabusRepository.Object,
                _mockContext.Object,
                _mockMapper.Object,
                _mockSyllabusRepo.Object,
                _mockUserService.Object,
                _mockTrainingUnitService.Object,
                _mockTrainingContentService.Object,
                _mockTrainingContentRepo.Object,
                _mockTrainingUnitRepo.Object,
                _mockSyllabusObjectiveRepo.Object,
                _mockDeliveryRepo.Object,
                _mockLearningObjectRepo.Object,
                _mockDeliveryTypeService.Object,
                _mockSchemaRepo.Object,
                _mockMaterialService.Object,
                _mockMaterialRepo.Object,
                _mockClassUserRepo.Object,
                _mockClassTrainerUnitRepo.Object);

            data = TestHelper.GetFakeDataSyllabus();

            _mockMapper.SetupMapperToSyllabus();

        }
        [Test]
        public async Task SearchSyllabus_SearchNoFields_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);            // Act
            var result = await _syllabusService.SearchSyllabus();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            Assert.AreEqual(1, viewListResponse.PageNumber);
            Assert.AreEqual(10, viewListResponse.PageSize);
            Assert.AreEqual(1, viewListResponse.TotalPage);
            Assert.AreEqual(3, viewListResponse.List.Length);

        }
        [Test]
        public async Task SearchSyllabus_SearchByOutputStandards_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);            // Act
            var result = await _syllabusService.SearchSyllabus(null, null, "LO01, LO03");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var viewList = viewListResponse.List as ViewListSyllabusDTO[];
            Assert.IsTrue(viewList.All(src => src.OutputStandards.Intersect(new string[] { "LO01", "LO03" }).Any()));

        }
        [Test]
        public async Task SearchSyllabus_SearchByDate_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);
            var result = await _syllabusService.SearchSyllabus(null, null, null, null, "03/12/2024", "03/22/2024");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);
            var viewList = viewListResponse.List as ViewListSyllabusDTO[];
            Assert.IsTrue(viewList.All(src => DateTime.ParseExact(src.CreatedDate, Value.DateFormat, CultureInfo.InvariantCulture).Date < DateTime.Parse("03/22/2024").Date && DateTime.ParseExact(src.CreatedDate, Value.DateFormat, CultureInfo.InvariantCulture).Date > DateTime.Parse("03/12/2024").Date));


        }
        [Test]
        public async Task SearchSyllabus_SearchBySearchString_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);            // Act
            var result = await _syllabusService.SearchSyllabus(null, null, null, "BP21");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var viewList = viewListResponse.List as ViewListSyllabusDTO[];
            Assert.IsTrue(viewList.All(src => src.SyllabusCode == "BP21"));
        }
        [Test]
        public async Task SearchSyllabus_OrderByTopicCode_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);            // Act
            var result = await _syllabusService.SearchSyllabus(null, null, null, null, null, null, "syllabuscode", "desc");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var viewList = viewListResponse.List as ViewListSyllabusDTO[];
            Assert.IsNotNull(viewList);
            Assert.IsTrue(viewList.Length > 1);

            for (int i = 1; i < viewList.Length; i++)
            {
                string currentSyllabusCode = viewList[i].SyllabusCode;
                string previousSyllabusCode = viewList[i - 1].SyllabusCode;
                Assert.IsTrue(string.Compare(currentSyllabusCode, previousSyllabusCode) <= 0,
                    $"Syllabus code {currentSyllabusCode} at position {i} is not less than or equal to {previousSyllabusCode}.");
            }
        }
        [Test]
        public async Task SearchSyllabus_Pagination_ReturnHavedValueList()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);            // Act
            var result = await _syllabusService.SearchSyllabus(1, 1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var viewListResponse = okResult.Value as ViewListResponse;
            Assert.IsNotNull(viewListResponse);

            var viewList = viewListResponse.List as ViewListSyllabusDTO[];
            Assert.IsNotNull(viewList);
            Assert.IsTrue(viewList.Length== 1);

        }
        [Test]
        public async Task GetOutlineById_ReturnHavedValueObject()
        {

            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            var result = await _syllabusService.GetOutLineSyllabusBySyllabusId(1);
            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].DayNumber==1);
            Assert.IsTrue(result[0].TrainingUnits!=null&&result[0].TrainingUnits.Length == 1);
            Assert.IsTrue(result[0].TrainingUnits != null && result[0].TrainingUnits.SelectMany(x => x.TrainingContents).Count() == 1);


        }
        [Test]
        public async Task GetOutlineById_ThrowExceptionByNotFoundId()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            var result = await _syllabusService.GetOutLineSyllabusBySyllabusId(1);
            Assert.ThrowsAsync<Exception>(async () => await _syllabusService.GetOutLineSyllabusBySyllabusId(-1));
        }
        [Test]
        public async Task GetDetailSyllabus_ReturnHavedValueObject()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            var result = await _syllabusService.GetDetailSyllabus(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var obj = okResult.Value as ViewDetailSyllabusDto;

            Assert.IsTrue(obj.Id == 1);

        }
        [Test]
        public async Task GetDetailSyllabus_ReturnNotFound()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            Assert.ThrowsAsync<Exception>(async () => await _syllabusService.GetOutLineSyllabusBySyllabusId(-1));
        }
        [Test]
        public async Task UpdateSyllabus_ReturnOk()
        {
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);
        }

        [Test]
        public async Task CreateSyllabusOtherScreen_ReturnOk()
        {
            AssessmentSchemeRequest assessmentScheme = new AssessmentSchemeRequest
            {
                SyllabusId = 18,
                Quiz = 100,
                Assignment = 0,
                Final = 0,
                FinalTheory = 100,
                FinalPractice = 0,
                Passing = 0,
                trainingPrinciple = "Test Training Principle"
            };
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            var result = await _syllabusService.CreateSyllabusOtherScreen(assessmentScheme);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateSyllabusOtherScreen_ReturnBadRequest()
        {
            int SyllabusID = 1000000000; // SyllabusID not exist
            AssessmentSchemeRequest assessmentScheme = new AssessmentSchemeRequest
            {
                SyllabusId = SyllabusID,
                Quiz = 100,
                Assignment = 0,
                Final = 0,
                FinalTheory = 100,
                FinalPractice = 0,
                Passing = 0,
                trainingPrinciple = "Test Training Principle"
            };
            var mock = data.BuildMock();
            _mockSyllabusRepository.Setup(src => src.Get()).Returns(mock);

            var result = await _syllabusService.CreateSyllabusOtherScreen(assessmentScheme);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest.Value);
            Assert.AreEqual(400, badRequest.StatusCode);
        }
    }

}
