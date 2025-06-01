using AutoMapper;
using FAMS.Api.Services;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using MockQueryable.Moq;
using Moq;
using NSubstitute;
using NSubstitute.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Services
{
    [TestFixture]   
    
    public class MaterialServiceTests
    {
        private Mock<FamsContext> _mockDbContext;
        private Mock<IBaseRepository<Material>> _mockMaterialRepo;
        private Mock<IBaseRepository<TrainingContent>> _mockTrainingContentRepo;
        private Mock<IMapper> _mockMapper;
        private MaterialService _materialService; 
        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<FamsContext>();
            _mockMaterialRepo = new Mock<IBaseRepository<Material>>();
            _mockTrainingContentRepo= new Mock<IBaseRepository<TrainingContent>>();
            _mockMapper = new Mock<IMapper>();
            _materialService = new MaterialService(_mockDbContext.Object,_mockMaterialRepo.Object,_mockMapper.Object,_mockTrainingContentRepo.Object);

            _mockMapper.Setup(x => x.Map<MaterialDto>(It.IsAny<Material>())).Returns((Material material) => new MaterialDto
            {
                ContentId = material.ContentId,
                CreatedBy = material.createdBy,
                CreatedDate = material.createdOn.ToString(Value.DateFormat),
                Title = material.Title,
                Url = material.Url
            });
        
        }
        [Test]
        public async Task GetMaterial_ReturnOk()
        {
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { }.BuildMock());
            var result = await  _materialService.GetMaterials(); 

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GetMaterialById_ReturnNotFound()
        {
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { }.BuildMock());
            var result = await _materialService.GetMaterialById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        [Test]
        public async Task GetMaterialById_ReturnSuccess()
        {
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id=1} }.BuildMock());
            var result = await _materialService.GetMaterialById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task CreateMaterials_BadRequest_NotfoundCOntent()
        {
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(new List<TrainingContent>() { new TrainingContent() { Id = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.AddAsync(It.IsAny<Material>(), default)).Returns(Task.CompletedTask);
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var result = await _materialService.CreateMaterial(new Domain.Models.Dtos.Request.CreateMaterialDTO()
            {
                ContentId = 2,

            });

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task CreateMaterials_Success()
        {
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(new List<TrainingContent>() { new TrainingContent() { Id = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.AddAsync(It.IsAny<Material>(), default)).Returns(Task.CompletedTask);
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var result = await _materialService.CreateMaterial(new Domain.Models.Dtos.Request.CreateMaterialDTO()
            {
                ContentId = 1,

            });

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task DeleteMaterials_NotFoundContent()
        {
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Delete(It.IsAny<Material>())).Callback((Material material) => { });
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var result = await _materialService.DeleteMaterial(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task DeleteMaterials_Success()
        {
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Delete(It.IsAny<Material>())).Callback((Material material) => { });
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var result = await _materialService.DeleteMaterial(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GetByContentId_NotFoundTrainingContent()
        {
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(new List<TrainingContent>() { }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Delete(It.IsAny<Material>())).Callback((Material material) => { });
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            Assert.ThrowsAsync<Exception>(async ()=> await _materialService.GetByContentId(1));
            
        }
        [Test]
        public async Task GetByContentId_Success()
        {
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(new List<TrainingContent>() { new TrainingContent() { Id =1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Delete(It.IsAny<Material>())).Callback((Material material) => { });
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var result = await _materialService.GetByContentId(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable< MaterialDto>>(result);

        }
        [Test]
        public async Task DeleteRangeByContentId_NotFoundContent()
        {
            _mockTrainingContentRepo.Setup(x => x.Get()).Returns(new List<TrainingContent>() { }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Get()).Returns(new List<Material>() { new Material() { Id = 1, ContentId = 1 } }.BuildMock());
            _mockMaterialRepo.Setup(x => x.Delete(It.IsAny<Material>())).Callback((Material material) => { });
            _mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            Assert.ThrowsAsync<Exception>(async () => await _materialService.DeleteRangeByContentId(1));
        }
    }
}
