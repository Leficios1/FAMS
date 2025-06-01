using AutoMapper;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Models.Entities;
using MockQueryable.FakeItEasy;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Services
{
    [TestFixture]   
    
    public class TrainingUnitServiceTests
    {
        private Mock<IBaseRepository<TrainingUnit>> _mockTrainingUnitRepo;
        private Mock<IMapper> _mockMapper;
        private TrainingUnitService _trainingUnitService; 
        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockTrainingUnitRepo= new Mock<IBaseRepository<TrainingUnit>>();
            _trainingUnitService = new TrainingUnitService(_mockTrainingUnitRepo.Object, _mockMapper.Object);
        }
        [Test]
        public async Task AddTrainingUnit_UnitNameIsNull()
        {
            _mockTrainingUnitRepo.Setup(x => x.AddAsync(It.IsAny<TrainingUnit>(), default)).Returns(Task.CompletedTask);
            _mockTrainingUnitRepo.Setup(x=>x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            _mockTrainingUnitRepo.Setup(x => x.Find(It.IsAny<Expression<Func<TrainingUnit, bool>>>(), It.IsAny<string>(), It.IsAny<Func<IQueryable<TrainingUnit>, IOrderedQueryable<TrainingUnit>>>())).ReturnsAsync((Expression<Func<TrainingUnit, bool>> predicate, string includeProperties, Func<IQueryable<TrainingUnit>, IOrderedQueryable<TrainingUnit>> orderBy) => new List<TrainingUnit>() { new TrainingUnit() { SyllabusId=1,UnitCode=1} });

            var newUnit = new TrainingUnit()
            {
                DayNumber = 2,
                SyllabusId=1
            };
            Assert.ThrowsAsync<Exception>(async () =>await _trainingUnitService.AddTrainingUnit(newUnit), $"Unit name is required.");
        }

        [Test]
        public async Task AddTrainingUnit_Success()
        {
            _mockTrainingUnitRepo.Setup(x => x.AddAsync(It.IsAny<TrainingUnit>(), default)).Returns(Task.CompletedTask);
            _mockTrainingUnitRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);
            _mockTrainingUnitRepo.Setup(x => x.Find(It.IsAny<Expression<Func<TrainingUnit, bool>>>(), It.IsAny<string>(), It.IsAny<Func<IQueryable<TrainingUnit>, IOrderedQueryable<TrainingUnit>>>())).ReturnsAsync((Expression<Func<TrainingUnit, bool>> predicate, string includeProperties, Func<IQueryable<TrainingUnit>, IOrderedQueryable<TrainingUnit>> orderBy) => new List<TrainingUnit>() { new TrainingUnit() { SyllabusId = 1, UnitCode = 1 } });

            var newUnit = new TrainingUnit()
            {   
                UnitName="tse",
                DayNumber = 2,
                SyllabusId = 1
            };
            var result = await _trainingUnitService.AddTrainingUnit(newUnit); 
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<TrainingUnit>(result);
        }
        [Test]
        public async Task DeleteTrainingUnit_NotFoundUnit()
        {
            _mockTrainingUnitRepo.Setup(x => x.Get()).Returns(new List<TrainingUnit>() { new TrainingUnit() { UnitCode = 1, TrainingContents = new List<TrainingContent>() { new TrainingContent() { Id = 1 } } } }.BuildMock());
            _mockTrainingUnitRepo.Setup(x => x.Delete(It.IsAny<TrainingUnit>())).Callback(() => { });
            _mockTrainingUnitRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingUnitService.DeleteTrainingUnit(2), ("There is no unit that has code: 2"));
        }
        [Test]
        public async Task DeleteTrainingUnit_HasContent_CannotDelete()
        {
            _mockTrainingUnitRepo.Setup(x => x.Get()).Returns(new List<TrainingUnit>() { new TrainingUnit() { UnitCode = 1, TrainingContents = new List<TrainingContent>() { new TrainingContent() { Id = 1 } } } }.BuildMock());
            _mockTrainingUnitRepo.Setup(x => x.Delete(It.IsAny<TrainingUnit>())).Callback(() => { });
            _mockTrainingUnitRepo.Setup(x => x.SaveChangesAsync(default)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _trainingUnitService.DeleteTrainingUnit(1), ("Can not delete this unit"));
        }
        [Test]
        public async Task UpdateTrainingUnit_NotFindUnitCode()
        {
            _mockTrainingUnitRepo.Setup(x => x.Get()).Returns(new List<TrainingUnit>() { new TrainingUnit() {  } }.BuildMock());
            Assert.ThrowsAsync<Exception>(async () => await _trainingUnitService.UpdateTrainingUnit(new TrainingUnit()
            {
                UnitCode=2
            }), "There is not unit that has code: 2");
        }
    }
}
