using AutoMapper;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.FakeItEasy;
using Moq;
namespace FAMS.Test.Services;
[TestFixture]
public class LearningObjectiveServiceTests
{
    private Mock<IBaseRepository<LearningObjective>> mockLearningObjectiveRepository;
    private Mock<IMapper> mockMapper;
    private Mock<IBaseRepository<SyllabusObjective>> mockSyllabusObjectiveRepository;
    private LearningObjectiveService learningObjectiveService;

    [SetUp]
    public void SetUp()
    {
        mockLearningObjectiveRepository = new Mock<IBaseRepository<LearningObjective>>();
        mockMapper = new Mock<IMapper>();
        mockSyllabusObjectiveRepository = new Mock<IBaseRepository<SyllabusObjective>>();
        learningObjectiveService = new LearningObjectiveService(mockLearningObjectiveRepository.Object, mockMapper.Object, mockSyllabusObjectiveRepository.Object);
    }

    [Test]
    public async Task GetLearningObjectiveAsync_ReturnsAllList()
    {
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO1", Description = "Learning Objective 1" },
            new LearningObjective { ObjectiveCode = "LO2", Description = "Learning Objective 2" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.GetLearningObjectiveAsync();
        Assert.IsNotNull(result);
        Assert.AreEqual(learningObjectives.Count, result.Count());
    }
    [Test]
    public async Task GetLearningObjectiveByCode_ReturnsOk()
    {
        string code = "LO1";
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO1", Description = "Learning Objective 1" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.GetLeaningObjectiveByCode(code);
        Assert.IsNotNull(result);
        Assert.AreEqual(learningObjectives,result);
    }
    [Test]
    public async Task GetLearningObjectiveByCode_ReturnsNotFoundCode()
    {
        string code = "LO1";
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO2", Description = "Learning Objective 1" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.GetLeaningObjectiveByCode(code);
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Equals(learningObjectives));
    }
    [Test]
    public async Task SearchLearningObjective_ReturnsAll()
    {
        var PageNumber = 1;
        var PageSize = 10;
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO1", Description = "Learning Objective 1" },
            new LearningObjective { ObjectiveCode = "LO2", Description = "Learning Objective 2" },
            new LearningObjective { ObjectiveCode = "LO3", Description = "Learning Objective 3" },
            new LearningObjective { ObjectiveCode = "LO4", Description = "Learning Objective 4" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.Search(PageNumber, PageSize, null);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var response = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, response.PageNumber);
        Assert.AreEqual(4, response.List.Length);
    }
    [Test]
    public async Task SearchLearningObjective_SearchInput_ReturnsAll()
    {
        var PageNumber = 1;
        var PageSize = 10;
        var searchInput = "LO1";
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO1", Description = "Learning Objective 1" },
            new LearningObjective { ObjectiveCode = "LO2", Description = "Learning Objective 2" },
            new LearningObjective { ObjectiveCode = "LO3", Description = "Learning Objective 3" },
            new LearningObjective { ObjectiveCode = "LO4", Description = "Learning Objective 4" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.Search(PageNumber, PageSize, searchInput);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var response = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, response.PageNumber);
        Assert.AreEqual(1, response.List.Length);
    }
    [Test]
    public async Task SearchLearningObjective_SearchDescription_ReturnsOk()
    {
        var PageNumber = 1;
        var PageSize = 10;
        var searchInput = "Learning Objective 1";
        var learningObjectives = new List<LearningObjective>
        {
            new LearningObjective { ObjectiveCode = "LO1", Description = "Learning Objective 1" },
            new LearningObjective { ObjectiveCode = "LO5", Description = "Learning Objective 1" },
            new LearningObjective { ObjectiveCode = "LO2", Description = "Learning Objective 2" },
            new LearningObjective { ObjectiveCode = "LO3", Description = "Learning Objective 3" },
            new LearningObjective { ObjectiveCode = "LO4", Description = "Learning Objective 4" }
        };
        var mock = learningObjectives.BuildMock();
        mockLearningObjectiveRepository.Setup(x => x.Get()).Returns(mock);
        var result = await learningObjectiveService.Search(PageNumber, PageSize, searchInput);
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var response = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, response.PageNumber);
        Assert.AreEqual(2, response.List.Length);
    }
}