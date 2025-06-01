using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.FakeItEasy;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Services
{
    [TestFixture]

    public class TrainingContentServiceTests
    {
        private Mock<IBaseRepository<TrainingContent>> _mockTrainingContentRepo;
        private Mock<IBaseRepository<Material>> _mockMaterailRepo;
        private TrainingContentService _trainingContentService;
        public TrainingContentServiceTests()
        {
            _mockMaterailRepo = new Mock<IBaseRepository<Material>>();
            _mockTrainingContentRepo = new Mock<IBaseRepository<TrainingContent>>();
            _trainingContentService = new TrainingContentService(_mockTrainingContentRepo.Object, _mockMaterailRepo.Object);
        }

        [Test]
        public async Task AddTrainingContent_NotAddTrainingConte()
        {
            var newTrainingContent = new TrainingContent()
            {
                ContentName = "adasdad",
                DeliveryType = 1,
                Duration = 12,
                LearningObjectiveCode = "K4SD",
                Note = "đâsdad",
                TrainingFormat = "Online",
                UnitCode = 1

            };
            _mockTrainingContentRepo.Setup(x => x.AddAsync(It.IsAny<TrainingContent>(), default)).Returns(async (TrainingContent src, CancellationToken token) => await Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.AddTrainingContent(newTrainingContent), $"Not add training content: {newTrainingContent.ContentName}");
        }
        [Test]
        public async Task AddTrainingContent_ErrorFomat()
        {
            var newTrainingContent = new TrainingContent()
            {
                ContentName = "adasdad",
                DeliveryType = 1,
                Duration = 12,
                LearningObjectiveCode = "K4SD",
                Note = "đâsdad",
                UnitCode = 1

            };
            _mockTrainingContentRepo.Setup(x => x.AddAsync(It.IsAny<TrainingContent>(), default)).Returns(async (TrainingContent src, CancellationToken token) => await Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.AddTrainingContent(newTrainingContent), $"Training format can't not be empty!");
        }
        [Test]
        public async Task AddTrainingContent_ErrorDuration()
        {
            var newTrainingContent = new TrainingContent()
            {
                ContentName = "adasdad",
                DeliveryType = 1,
                Duration = -2,
                LearningObjectiveCode = "K4SD",
                Note = "đâsdad",
                TrainingFormat = "Online",
                UnitCode = 1

            };
            _mockTrainingContentRepo.Setup(x => x.AddAsync(It.IsAny<TrainingContent>(), default)).Returns(async (TrainingContent src, CancellationToken token) => await Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.AddTrainingContent(newTrainingContent), $"Duration can't not less than 0!");
        }
        [Test]
        public async Task AddTrainingContent_NullTrainingContentName()
        {
            var newTrainingContent = new TrainingContent()
            {
                DeliveryType = 1,
                Duration = 9,
                LearningObjectiveCode = "K4SD",
                Note = "đâsdad",
                TrainingFormat = "Online",
                UnitCode = 1

            };
            _mockTrainingContentRepo.Setup(x => x.AddAsync(It.IsAny<TrainingContent>(), default)).Returns(async (TrainingContent src, CancellationToken token) => await Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.AddTrainingContent(newTrainingContent), $"Training content name can't not be empty!");
        }
        [Test]
        public async Task AddTrainingContent_Success()
        {
            var newTrainingContent = new TrainingContent()
            {
                ContentName = "sdad",
                DeliveryType = 1,
                Duration = 9,
                LearningObjectiveCode = "K4SD",
                Note = "đâsdad",
                TrainingFormat = "Online",
                UnitCode = 1

            };
            _mockTrainingContentRepo.Setup(x => x.AddAsync(It.IsAny<TrainingContent>(), default)).Returns(async (TrainingContent src, CancellationToken token) => await Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            _mockTrainingContentRepo.Setup(x => x.Find(
     It.IsAny<Expression<Func<TrainingContent, bool>>>(),
     It.IsAny<string>(),
     It.IsAny<Func<IQueryable<TrainingContent>, IOrderedQueryable<TrainingContent>>>()
 )).ReturnsAsync(new List<TrainingContent>() { newTrainingContent }.AsEnumerable());
            Assert.DoesNotThrowAsync(async () => await _trainingContentService.AddTrainingContent(newTrainingContent));
        }
        [Test]
        public async Task GetTimeAllocation_OkObjectResult()
        {
            var newSyllabus = new Syllabus()
            {
                Id = 1
            };
            var newUnit = new TrainingUnit()
            {
                UnitCode = 1,
                Syllabus = newSyllabus
            };
            var newContents = new List<TrainingContent>()
            {
                new TrainingContent() {
                    Id=1, Duration =1, DeliveryType=1, TrainingUnit=newUnit}
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            var result = await _trainingContentService.getTimeAllocation(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);   
        }
        [Test]
        public async Task GetTimeAllocation_BadRequest_TotalDuration()
        {
            var newSyllabus = new Syllabus()
            {
                Id = 1
            };
            var newUnit = new TrainingUnit()
            {
                UnitCode = 1,
                Syllabus = newSyllabus
            };
            var newContents = new List<TrainingContent>()
            {
                new TrainingContent() {
                    Id=1,DeliveryType=1, TrainingUnit=newUnit}
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            var result = await _trainingContentService.getTimeAllocation(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Total duration is either null or zero");
        }
        [Test]
        public async Task GetTimeAllocation_BadRequest_NotFountContentId()
        {
            var newSyllabus = new Syllabus()
            {
                Id = 1
            };
            var newUnit = new TrainingUnit()
            {
                UnitCode = 1,
                Syllabus = newSyllabus
            };
            var newContents = new List<TrainingContent>()
            {
                new TrainingContent() {
                    Id=1,DeliveryType=1, TrainingUnit=newUnit}
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            var result = await _trainingContentService.getTimeAllocation(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "No training content found for syllabus ID: 2");
        }
        [Test]
        public async Task RemoveTrainingContentById_ReturnOk()
        {
            
            var newContents = new List<TrainingContent>()
            {
                new TrainingContent() {
                    Id=1,DeliveryType=1}
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            _mockTrainingContentRepo .Setup(x => x.Delete(It.IsAny<TrainingContent>())).Callback(void (TrainingContent content) => { });
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            Assert.DoesNotThrowAsync(async () => await _trainingContentService.RemoveTrainingContentById(1));

        }
        [Test]
        public async Task RemoveTrainingContentById_ThrowException()
        {

            var newContents = new List<TrainingContent>()
            {

            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.RemoveTrainingContentById(1), "There is no content that has id:"+1);

        }
        [Test]
        public async Task UpdateTrainingContent_ReturnOk()
        {
            var newContent = new TrainingContent()
            {
                Id = 1,
                DeliveryType = 1
            };
            var newContents = new List<TrainingContent>()
            {
                newContent
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            _mockTrainingContentRepo.Setup(x => x.Update(It.IsAny<TrainingContent>())).Callback(void (TrainingContent content) => { });
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            Assert.DoesNotThrowAsync(async () => await _trainingContentService.UpdateTrainingUnit(newContent));
        }
        [Test]
        public async Task UpdateTrainingContent_ReturnBadRequest()
        {
            var newContent = new TrainingContent()
            {
                Id = 1,
                DeliveryType = 1
            };
            var newContents = new List<TrainingContent>()
            {
                newContent
            };
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(newContents.BuildMock());
            _mockTrainingContentRepo.Setup(x => x.Update(It.IsAny<TrainingContent>())).Callback(void (TrainingContent content) => { });
            _mockTrainingContentRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            Assert.ThrowsAsync<Exception>(async () => await _trainingContentService.UpdateTrainingUnit(new TrainingContent() { Id=2}), "There is not unit that has code: 2");
        }
    }
}
