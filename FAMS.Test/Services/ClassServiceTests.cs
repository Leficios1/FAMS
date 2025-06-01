using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Api.Services;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using FAMS.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.FakeItEasy;
using Moq;

namespace FAMS.Test.Services
{
    [TestFixture]
    public class ClassServiceTests
    {
        private ClassService _classService;
        private Mock<IBaseRepository<Class>> _mockclassrepo;

        private Mock<IMapper> _mockMapper;
        [SetUp]
        public void SetUp()
        {
            _mockclassrepo = new Mock<IBaseRepository<Class>>();
            _mockMapper = new Mock<IMapper>();
            _classService = new ClassService(
                syllabusService: It.IsAny<ISyllabusService>(),
                Class: _mockclassrepo.Object,
                context: It.IsAny<FamsContext>(),
                classUser: It.IsAny<IBaseRepository<ClassUser>>(),
                trainingProgram: It.IsAny<IBaseRepository<TrainingProgram>>(),
                trainingProgramSyllabusRepo: It.IsAny<IBaseRepository<TrainingProgramSyllabus>>(),
                calendarRepo: It.IsAny<IBaseRepository<CalendarClass>>(),
                assessmentSchemeRepo: It.IsAny<IBaseRepository<AssessmentSchemeDTO>>(),
                mapper: _mockMapper.Object,
                userRepo: It.IsAny<IBaseRepository<User>>(),
                classTrainerUnitRepo: It.IsAny<IBaseRepository<ClassTrainerUnit>>()
            );
            _mockMapper.SetupMapperToClass();
        }

        [Test]
        public async Task ViewClassOnlist_returnOk()
        {
            var data = new List<Class>().BuildMock();
            _mockclassrepo.Setup(x => x.Get()).Returns(data);
            
            var result = await _classService.SearchClassOnList(1, 1, null, null, null, null, null, null, null, null, null, null);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        }

        [Test]
        public async Task ViewClassOnlist_returnNotFound()
        {
            string searchString = "Test string to search";   
            var data = new List<Class>().BuildMock();
            _mockclassrepo.Setup(x => x.Get()).Returns(data);

            var result = await _classService.SearchClassOnList(1, 1, searchString, null, null, null, null, null, null, null, null, null);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result);

            var valueSearch = ((OkObjectResult)result).Value;
            var listReturn = ((dynamic)valueSearch).List;
            Assert.IsEmpty(listReturn, "List should be null");

        }
        [Test]
        public async Task SearchClassOnList_Pagination_ReturnHavedValue()
        {
            _mockclassrepo.Setup(x => x.Get()).Returns(new Class[] { 
            new Class(){Id=1},
            new Class(){Id=2}
            }.BuildMock());

            var result = await _classService.SearchClassOnList(1,10);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var viewListResponse = (result as OkObjectResult).Value as ViewListResponse;

            Assert.IsTrue(viewListResponse.TotalPage == 1);
            Assert.IsTrue(viewListResponse.PageSize == 10); 
            Assert.IsTrue(viewListResponse.PageNumber==1);
            Assert.IsTrue(viewListResponse.List.Length <= 10);
        }
        [Test]
        public async Task SearchClassOnList_NoThing_ReturnValue()
        {
            _mockclassrepo.Setup(x => x.Get()).Returns(new Class[] {
            new Class(){Id=1},
            new Class(){Id=2}
            }.BuildMock());

            var result = await _classService.SearchClassOnList();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var viewListResponse = (result as OkObjectResult).Value as ViewListResponse;

            Assert.IsTrue(viewListResponse.TotalPage == 1);
            Assert.IsTrue(viewListResponse.PageSize == 10);
            Assert.IsTrue(viewListResponse.PageNumber == 1);
            Assert.IsTrue(viewListResponse.List.Length <= 10);
        }
        [Test]
        public async Task GetDetailClass_ReturnOk()
        {
            _mockclassrepo.Setup(x => x.Get()).Returns(new Class[] {
            new Class(){Id=1},
            new Class(){Id=2}
            }.BuildMock());
            var result = await _classService.ViewClassDetail(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var resultObject = (result as OkObjectResult).Value as ViewDetailClassDTO;
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject.Id == 1);
        }
        [Test]
        public async Task GetDetailClass_ReturnBadRequest()
        {
            _mockclassrepo.Setup(x => x.Get()).Returns(new Class[] {
            new Class(){Id=1},
            new Class(){Id=2}
            }.BuildMock());
            var result = await _classService.ViewClassDetail(-1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var resultObject = (result as BadRequestObjectResult).Value as string;
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject==EMS.EM50 + -1);
        }
    }
}
