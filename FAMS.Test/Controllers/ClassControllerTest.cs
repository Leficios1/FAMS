using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Web.Http.Results;

namespace FAMS.Test.Controllers
{
    public class ClassControllerTest
    {
        private Mock<IClassService> _mockClassService;
        private Mock<IMapper> _mockMapper;
        private ClassController _classController;

        [SetUp]
        public void SetUp()
        {
            _mockClassService = new Mock<IClassService>();
            _mockMapper = new Mock<IMapper>();
            _classController = new ClassController(_mockClassService.Object);
        }

        [Test]
        public async Task SearchClassonList_returnsOK()
        {
            _mockClassService.Setup(x => x.SearchClassOnList(1, 1,null,null,null,null,null,null,null,null,null,null,null)).ReturnsAsync(new OkObjectResult(""));
            var result = await _classController.SearchClassOnList(1, 1, null, null, null, null, null, null, null, null, null, null);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        }

        [Test]
        public async Task SearchClassonList_returnsNotFound()
        {
            _mockClassService.Setup(x => x.SearchClassOnList(1, 1, "SC01", null, null, null, null, null, null, null, null, null, null)).ReturnsAsync(new NotFoundObjectResult("There's no record matching with your criteria"));
            var result = await _classController.SearchClassOnList(1, 1, "SC01", null, null, null, null, null, null, null, null, null, null);

            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Result should be an instance of NotFoundObjectResult");
            var okResult = (NotFoundObjectResult)result;
            Assert.AreEqual(404, okResult.StatusCode);
            Assert.AreEqual("There's no record matching with your criteria", okResult.Value);
        }

        /*[Test]
        public async Task UpdateClass01_ReturnsOK()
        {
            // Arrange
            var classUpdateDto = new UpdateClass01RequestDto
            {
                Id = 10,
                ClassName = "New Class Name",
                Location = "New Location",
                ClassTimeStart = new DateTime(2024, 1, 30),
                ClassTimeEnd = new DateTime(2024, 2, 1),
                FSU = "New FSU",
                UserIds = new List<int> { 1, 2 },
                TrainingProgramCode = 1,
                DateAndTimeStudy = new List<DateTime> { new DateTime(2024, 2, 1), new DateTime(2024, 2, 3) }
            };

            _mockClassService.Setup(x => x.UpdateClass01(classUpdateDto)).ReturnsAsync(new OkObjectResult(""));

            // Act
            var result = await _classController.UpdateClass01(classUpdateDto);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        }*/

        [Test]
        public async Task UpdateClass01_ReturnsNotFound()
        {
            // Arrange
            var classUpdateDto = new UpdateClass01RequestDto
            {
                Id = 999, //Not existed
            };

            _mockClassService.Setup(x => x.UpdateClass01(classUpdateDto)).ReturnsAsync(new NotFoundObjectResult(""));

            // Act
            var result = await _classController.UpdateClass01(classUpdateDto);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Result should be an instance of NotFoundObjectResult");

            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404");
        }
        [Test]
        public async Task CreateClass_ReturnsOk()
        {
            // Arrange
            var createClassDto = new CreateClassDto
            {
                TrainingProgramCode = 1,
                ClassName = "English101",
                Duration = 60,
                Status = "Scheduled",
                Location = "HCM",
                FSU = "FHM",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(30),
                ClassTimeStart = DateTime.Parse("10:00 AM"),
                ClassTimeEnd = DateTime.Parse("12:00 PM"),
                CreatedBy = "Manh",
                CreatedDate = DateTimeOffset.Now,
      
                AdminId = new int[] { 2 , 4 },
                Calendar = new DateTime[]
                {
                    DateTime.Parse("2024-01-11"),
                    DateTime.Parse("2024-01-13")
                }
            };
            _mockClassService.Setup(x => x.CreateClass(createClassDto)).ReturnsAsync(new OkObjectResult("Class created successfully"));

            // Act
            var result = await _classController.CreateClass(createClassDto);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
            Assert.AreEqual("Class created successfully", okResult.Value, "Response message should be 'Class created successfully'");
        }
        [Test]
        public async Task CreateClass_ReturnsBadRequest()
        {
            // Arrange
            var createClassDto = new CreateClassDto
            {
                TrainingProgramCode = 1,
                ClassName = "English101",
                Duration = 60,
                Status = "Planning",
                Location = "HCM",
                FSU = "FHM",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(30),
                ClassTimeStart = DateTime.Parse("10:00 AM"),
                ClassTimeEnd = DateTime.Parse("12:00 PM"),
                CreatedBy = "Manh",
                CreatedDate = DateTimeOffset.Now,
               
                AdminId = new int[] { 2, 4 },
                Calendar = new DateTime[]
                {
                    DateTime.Parse("2024-01-11"),
                    DateTime.Parse("2024-01-13")
                }
            };
            _mockClassService.Setup(x => x.CreateClass(createClassDto)).ReturnsAsync(new BadRequestObjectResult("Class created unsuccessfully"));
            // Act
            var result = await _classController.CreateClass(createClassDto);
            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Result should be an instance of BadRequestObjectResult");
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(400, badRequestResult.StatusCode, "Status code should be 400");
            Assert.AreEqual("Class created unsuccessfully", badRequestResult.Value, "Response message should be 'Class created unsuccessfully'");
        }
    }
}
