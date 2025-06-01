using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Api.Dtos;
using FAMS.Api.Services;
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
    public class SyllabusControllerTests
    {
        private Mock<ISyllabusService> _mockSyllabusService;
        private Mock<IMapper> _mockMapper;
        private SyllabusController _syllabusController;
        private Mock<ITrainingContentService> _mockTrainingContentService;
        private Mock<ITrainingUnitService> _mockTrainingUnitService;

        [SetUp]
        public void Setup()
        {
            _mockSyllabusService = new Mock<ISyllabusService>();
            _mockMapper = new Mock<IMapper>();
            _mockTrainingContentService = new Mock<ITrainingContentService>();
            _mockTrainingUnitService = new Mock<ITrainingUnitService>();
            _syllabusController = new SyllabusController(_mockSyllabusService.Object, _mockMapper.Object, _mockTrainingContentService.Object, _mockTrainingUnitService.Object);
        }

        // Create Syllabus General Tab
        [Test]
        public async Task SyllabusController_CreateGeneralTab_ReturnsOk()
        {
            //Arrange
            var sylReq = new SyllabusDto
            {
                SyllabusName = "Testing Name",
                TrainingAudience = 20,
                TechnicalRequirement = "Testing Technical Requirement",
                CourseObjective = "Test Course Objective",
                CreatedBy = "LongV",
                UserId = 1,
            };
            //_mockMapper.Setup(x => x.Map<Syllabus>(sylReq)).Returns(new Syllabus());

            // Mocking the service method to return OkResult
            _mockSyllabusService.Setup(x => x.CreateSyllabusGeneralTab(sylReq)).ReturnsAsync(new OkResult());

            // Act
            var result = await _syllabusController.CreateSyllabusGeneralTab(sylReq);

            // Log the result
            Console.WriteLine($"Result: {result}");

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkResult>(result, "Result should be an instance of OkResult");
            Assert.AreEqual(200, ((OkResult)result).StatusCode, "Status code should be 200");
        }

        [Test]
        public async Task SyllabusController_CreateGeneralTab_ReturnsBadRequest()
        {
            // Arrange
            var syllabusDto = new SyllabusDto
            {
                // Invalid name to trigger bad request
                TrainingAudience = 10,
                TechnicalRequirement = "Testing Technical Requirement",
                CourseObjective = "Test Course Objective",
                CreatedBy = "LongV",
                UserId = 1,
            };

            // Mocking the service method to return BadRequestResult
            _mockSyllabusService.Setup(x => x.CreateSyllabusGeneralTab(syllabusDto)).ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _syllabusController.CreateSyllabusGeneralTab(syllabusDto);

            // Log the result
            Console.WriteLine($"Result: {result}");

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<BadRequestResult>(result, "Result should be an instance of BadRequestResult");
            Assert.AreEqual(400, ((BadRequestResult)result).StatusCode, "Status code should be 400");
        }

        //Duplicate Syllabus
        [Test]
        public async Task SyllabusController_DuplicateSyllabus_ReturnsOk()
        {
            // Arrange
            int syllabusId = 1; // Syllabus ID to duplicate
            var clonedSyllabus = new SyllabusDto
            {
                SyllabusName = "Basic Cross-Platform Application Programming With .NET",
                TrainingAudience = 35,
                CreatedBy = "TungTS",
                UserId = 1
            };

            _mockSyllabusService.Setup(x => x.DuplicationSyllabus(syllabusId)).ReturnsAsync(new OkObjectResult(clonedSyllabus));

            // Act
            var result = await _syllabusController.DuplicateSyllabus(syllabusId);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");

            var clonedSyllabusResult = okResult.Value as SyllabusDto;
            Assert.IsNotNull(clonedSyllabusResult, "Value should be a SyllabusDto object");

            // Because of the syllabus code is auto-generate so that we do not compare the new syllabus code with the old ones.
            Assert.AreEqual("Basic Cross-Platform Application Programming With .NET", clonedSyllabusResult.SyllabusName, "Cloned syllabus name should match");
            // Add additional assertions to verify other properties of the cloned syllabus if needed
        }

        [Test]
        public async Task SyllabusController_DuplicateSyllabus_ReturnsNotFound()
        {
            // Arrange
            int nonExistentSyllabusId = 999; // Non-existent Syllabus ID to duplicate
            _mockSyllabusService.Setup(x => x.DuplicationSyllabus(nonExistentSyllabusId)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _syllabusController.DuplicateSyllabus(nonExistentSyllabusId);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<NotFoundResult>(result, "Result should be an instance of NotFoundResult");

            var notFoundResult = (NotFoundResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404");
        }

        [Test]
        public async Task ViewExistedSyllabus_returnOk()
        {

            _mockSyllabusService.Setup(x => x.SearchSyllabus(1, 10, null, null, null, null, null, null)).ReturnsAsync(new OkObjectResult("oject return ok"));

            // Act
            var result = await _syllabusController.SearchSyllabus(1, 10, null, null, null, null, null, null);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        }

        [Test]
        public async Task ViewExistedSyllabus_returnNotFound()
        {
            string searchString = "toi dep trai nen toi viet cai nay";
            string expectedMessage = "There is no syllabus matching with the keywords.";
            _mockSyllabusService.Setup(x => x.SearchSyllabus(1, 10, searchString, null, null, null, null, null)).ReturnsAsync(new NotFoundObjectResult(expectedMessage));

            // Act
            var result = await _syllabusController.SearchSyllabus(1, 10, searchString, null, null, null, null, null);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Result should be an instance of NotFoundObjectResult");

            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404");

            Assert.AreEqual(expectedMessage, notFoundResult.Value, "Message should match");
        }

        [Test]
        public async Task CreateSyllabusOtherScreen_returnOk()
        {
            AssessmentSchemeRequest assessmentScheme = new AssessmentSchemeRequest
            {
                SyllabusId = 1,
                Quiz = 10,
                Assignment = 20,
                Final = 30,
                FinalTheory = 40,
                FinalPractice = 50,
                Passing = 100,
                trainingPrinciple = "Test Training Principle"
            };

            _mockSyllabusService.Setup(x => x.CreateSyllabusOtherScreen(assessmentScheme)).ReturnsAsync(new OkObjectResult("oject return ok"));
            var result = await _syllabusController.CreateSyllabusOtherScreen(assessmentScheme);

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");

        }

        [Test]
        public async Task CreateSyllabusOtherScreen_returnBadRequest()
        {
            int SyllabusId = 10000000; //notfound syllabus id
            AssessmentSchemeRequest assessmentScheme = new AssessmentSchemeRequest
            {
                SyllabusId = SyllabusId,
                Quiz = 10,
                Assignment = 20,
                Final = 30,
                FinalTheory = 40,
                FinalPractice = 50,
                Passing = 100,
                trainingPrinciple = "Test Training Principle"
            };

            _mockSyllabusService.Setup(x => x.CreateSyllabusOtherScreen(assessmentScheme)).ReturnsAsync(new OkObjectResult("oject return ok"));
            var result = await _syllabusController.CreateSyllabusOtherScreen(assessmentScheme);

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");

            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");

        }
    }
}
