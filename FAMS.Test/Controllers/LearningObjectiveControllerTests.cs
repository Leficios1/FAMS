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

namespace FAMS.Test.Controllers;
public class LearningObjectiveControllerTests
{
    private Mock<ILearningObjectiveService> _mockLearningObjective;
    private Mock<IMapper> _mockMapper;
    private LearningObjectiveController _learningObjectiveController;

    [SetUp]
    public void SetUp()
    {
        _mockLearningObjective = new Mock<ILearningObjectiveService>();
        _mockMapper = new Mock<IMapper>();
        _learningObjectiveController = new LearningObjectiveController(_mockLearningObjective.Object, _mockMapper.Object);
    }
    [Test]
    public async Task LearningObjectiveController_GetLearningObjective_ReturnsOk()
    {
        // Arrange
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "1", Description = "Objective 1" },
            new LearningObjective { ObjectiveCode = "2", Description = "Objective 2" }
        };
        _mockLearningObjective.Setup(repo => repo.GetLearningObjectiveAsync()).ReturnsAsync(learningObjectives);
        // Act
        var result = await _learningObjectiveController.GetLearningObjective();
        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        Assert.AreEqual(learningObjectives, okResult.Value, "Returned value should be the list of learning objectives");
    }
    [Test]
    public async Task LearningObjectiveController_GetLearningObjectiveByCode_ReturnsOk()
    {
        // Arrange
        var code = "1";
        var learningObjectives = new List<LearningObjective>
    {
        new LearningObjective { ObjectiveCode = "1", Description = "Objective 1" },
        new LearningObjective { ObjectiveCode = "2", Description = "Objective 2" }
    };
        _mockLearningObjective.Setup(repo => repo.GetLeaningObjectiveByCode(code)).ReturnsAsync(learningObjectives);
        // Act
        var result = await _learningObjectiveController.GetbyCode(code);
        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        Assert.AreEqual(learningObjectives, okResult.Value, "Returned value should be the list of learning objectives");
    }
    [Test]
    public async Task LearningObjectiveController_Search_ReturnsOk()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var searchInput = "keyword";
        var learningObjectives = new List<LearningObjective>
    {
        new LearningObjective { ObjectiveCode = "1", Description = "Objective 1" },
        new LearningObjective { ObjectiveCode = "2", Description = "Objective 2" }
    };
        var expectedResult = new
        {
            totalPages = 1,
            PageNumber = pageNumber,
            PageSize = pageSize,
            list = learningObjectives
        };
        _mockLearningObjective.Setup(repo => repo.Search(pageNumber, pageSize, searchInput)).ReturnsAsync(new OkObjectResult("Return Ok"));
        // Act
        var result = await _learningObjectiveController.Search(pageNumber, pageSize, searchInput);
        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkObjectResult");
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
    }
    [Test]
    public async Task LearningObjectiveController_Search_NotFound()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var searchInput = "keyword";
        var learningObjectives = new List<LearningObjective>
    {
        new LearningObjective { ObjectiveCode = "1", Description = "Objective 1" },
        new LearningObjective { ObjectiveCode = "2", Description = "Objective 2" }
    };
        var expectedResult = new
        {
            totalPages = 1,
            PageNumber = pageNumber,
            PageSize = pageSize,
            list = learningObjectives
        };
        _mockLearningObjective.Setup(repo => repo.Search(pageNumber, pageSize, searchInput)).ReturnsAsync(new NotFoundObjectResult("Return Not found"));
        // Act
        var result = await _learningObjectiveController.Search(pageNumber, pageSize, searchInput);
        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.IsInstanceOf<NotFoundObjectResult>(result, "Result should be an instance of NotFoundResult");
        var notFoundResult = (NotFoundObjectResult)result;
        Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 200");
    }
}