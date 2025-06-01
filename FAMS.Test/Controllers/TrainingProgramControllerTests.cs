using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Api.Dtos;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Controllers
{
    public class TrainingProgramControllerTests
    {
        private Mock<ITrainingProgramService> _mockTrainingProgramService;
        private Mock<IAuthService> mockAuthService;
        private TrainingProgramController _trainingProgramController;

        [SetUp]
        public void Setup()
        {
            _mockTrainingProgramService = new Mock<ITrainingProgramService>();
            mockAuthService = new Mock<IAuthService>();
            _trainingProgramController = new TrainingProgramController(_mockTrainingProgramService.Object,mockAuthService.Object);
        }

        [Test]
        public async Task UpdateTraingProgram_ReturnOk()
        {
            TrainingProgramDtoRequest trainingProgramDtoRequest = new TrainingProgramDtoRequest
            {
                TrainingProgramCode = 1,
                Name = "Test",
                UserId = 1,
                StartTime = DateTimeOffset.Now,
                Duration = 1,
                TopicCode = "Test",
                Status = 1,               
                ModifiedBy = "Test",
                ModifiedDate = DateTimeOffset.Now,
                TrainingProgramSyllabus = new CreateTrainingProgramSyllabusDTO[] {
                    new CreateTrainingProgramSyllabusDTO
                    {
                        Sequence = 1,
                        SyllabusId = 1
                    }
                }
            };
            _mockTrainingProgramService.Setup(x => x.UpdateTrainingProgram(trainingProgramDtoRequest)).ReturnsAsync(trainingProgramDtoRequest);
            var result = _trainingProgramController.UpdateTrainingProgram(trainingProgramDtoRequest);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task ChangeStatusTrainingProgram_ValidTrainingProgram_ReturnOk()
        {
            // Arrange
            int trainingProgramCode = 1;
            int status = 2;
            _mockTrainingProgramService.Setup(x => x.ChangeStatusTrainingProgram(trainingProgramCode, status))
                .ReturnsAsync(new OkObjectResult(new { TrainingProgramCode = trainingProgramCode, Status = status }));
            // Act
            var result = await _trainingProgramController.ChangeStatusTrainingProgram(trainingProgramCode, status);
            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(200, (result as OkObjectResult)?.StatusCode, "StatusCode should be 200");
        }

        [Test]
        public async Task ChangeStatusTrainingProgram_InvalidTrainingProgram_ReturnBadRequest()
        {
            // Arrange
            int trainingProgramCode = 12;
            int status = 2;
            _mockTrainingProgramService.Setup(x => x.ChangeStatusTrainingProgram(trainingProgramCode, status))
                .ReturnsAsync(new BadRequestObjectResult("TrainingProgram is not found!"));
            // Act
            var result = await _trainingProgramController.ChangeStatusTrainingProgram(trainingProgramCode, status);
            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(400, (result as BadRequestObjectResult)?.StatusCode, "StatusCode should be 400");
        }

        [Test]
         public async Task DuplicateTrainingProgram_ReturnsOk()
        {
            // Arrange
            int id = 1; // Assuming a valid ID
                        // Mock the result of successful duplication
            _mockTrainingProgramService.Setup(x => x.DuplicationTrainingProgram(id))
                .ReturnsAsync(new OkObjectResult("List of duplicated training programs"));

            // Act
            var result = await _trainingProgramController.Duplicate(id);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
            // Add more assertions as needed
        }

        [Test]
        public async Task DuplicateTrainingProgram_ReturnsBadRequest_NullId()
        {
            // Arrange
            int id = 0; // Assuming null ID
            _mockTrainingProgramService.Setup(x => x.DuplicationTrainingProgram(id)).ReturnsAsync(new BadRequestObjectResult("SyllabusId is not existed !"));

            // Act
            var result = await _trainingProgramController.Duplicate(id);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Result should be an instance of BadRequestObjectResult");

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(400, badRequestResult.StatusCode, "Status code should be 400");
        }
        [Test]
        public async Task SearchTrainingProgram_ReturnOk()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var searchString = "test";
            var startDateBegin = "2024-01-01";
            var startDateEnd = "2024-01-31";
            var sortBy = "name";
            var typeSort = "asc";

            var trainingPrograms = new List<TrainingProgram>
    {
        new TrainingProgram { TrainingProgramCode = 1, Name = "Test 1" },
        new TrainingProgram { TrainingProgramCode = 2, Name = "Test 2" }
    };
            var expectedlist = new
            {
                TotalPages = 1,
                PageNumber = pageNumber,
                PageSize = pageSize,
                List = trainingPrograms,
            };
            _mockTrainingProgramService.Setup(x => x.SearchTrainingProgram(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, typeSort))
                .ReturnsAsync(new OkObjectResult("Ok"));
            // Act
            var result = await _trainingProgramController.Get(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, typeSort);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }

        [Test]
        public async Task SearchTrainingProgram_ReturnNotFound()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var searchString = "nonexistent";
            var startDateBegin = "2024-01-01";
            var startDateEnd = "2024-01-31";
            var sortBy = "name";
            var typeSort = "asc";
            var trainingPrograms = new List<TrainingProgram>
    {
        new TrainingProgram { TrainingProgramCode = 1, Name = "Test 1" },
        new TrainingProgram { TrainingProgramCode = 2, Name = "Test 2" }
    };
            var expectedlist = new
            {
                TotalPages = 1,
                PageNumber = pageNumber,
                PageSize = pageSize,
                List = trainingPrograms,
            };
            _mockTrainingProgramService.Setup(x => x.SearchTrainingProgram(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, typeSort))
                .ReturnsAsync(new NotFoundObjectResult("Not found"));
            // Act
            var result = await _trainingProgramController.Get(pageNumber, pageSize, searchString, startDateBegin, startDateEnd, sortBy, typeSort);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
        }
    }
}
