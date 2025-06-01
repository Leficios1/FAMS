using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Response;
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
    public class SyllabusControllerTest
    {
        private Mock<ISyllabusService> _mockSyllabusService;
        private Mock<IMapper> _mockMapper;
        private Mock<ITrainingUnitService> _mockUnitService;
        private Mock<ITrainingContentService> _mockContentService;
        private SyllabusController _syllabusController;
        

        [SetUp]
        public void Setup()
        {
            _mockSyllabusService = new Mock<ISyllabusService>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitService=new Mock<ITrainingUnitService>();
            _mockContentService = new Mock<ITrainingContentService>();
            _syllabusController = new SyllabusController(_mockSyllabusService.Object, _mockMapper.Object, _mockContentService.Object, _mockUnitService.Object);
        }

        /// <summary>
        /// Result: Error
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task SyllabucController_SearchSyllabusByInput_ReturnsOk()
        {
            //Arrange
            var syllabuses = new List<Syllabus>
            {
                new Syllabus
                {
                    SyllabusCode="SC04",
                    SyllabusName="Software Project",
                    TechnicalRequirement="non",
                    Version="1",
                    AttendeeNumber=100,
                    CourseObjective="Non",
                    TrainingMaterials="Non",
                    TrainingPrinciples="Non",
                    Priority="Non",
                    PublishStatus=1,
                    UserId=2,
                    CreatedBy="KhoaVo",
                    CreatedDate=new DateTime(2020,01,01),
                    ModifiedBy="Khoa",
                    ModifiedDate=DateTimeOffset.Now,
                },
                new Syllabus
                {
                    SyllabusCode="SC07",
                    SyllabusName="Software Initialization",
                    TechnicalRequirement="non",
                    Version="1",
                    AttendeeNumber=100,
                    CourseObjective="Non",
                    TrainingMaterials="Non",
                    TrainingPrinciples="Non",
                    Priority="Non",
                    PublishStatus=1,
                    UserId=2,
                    CreatedBy="KhoaVo",
                    CreatedDate=new DateTime(2020,01,01),
                    ModifiedBy="Khoa",
                    ModifiedDate=DateTimeOffset.Now,
                }
            };
            //Act
            /*var result = await _syllabusController.SearchSyllabusByInput("SC07");
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);*/
        }

    }
}
