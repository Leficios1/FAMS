using AutoMapper;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Helper
{
    public static class TestHelper
    {

        public static Mock<IMapper> SetupMapperToSyllabus(this Mock<IMapper> _mockMapper)
        {
            _mockMapper.Setup(m => m.Map<ViewListSyllabusDTO>(It.IsAny<Syllabus>()))
                                .Returns((Syllabus source) => new ViewListSyllabusDTO
                                {

                                    Id = source.Id,
                                    SyllabusName = source.SyllabusName,
                                    CreatedBy = source.CreatedBy,
                                    CreatedDate = source.CreatedDate.ToString(Value.DateFormat),
                                    PublishStatus = source.PublishStatus,
                                    SyllabusCode = source.SyllabusCode,
                                    DurationByDay = (int)(source.TrainingUnits.SelectMany(x => x.TrainingContents).Sum(x => x.Duration) ?? 0),
                                    OutputStandards = source.SyllabusObjectives.Select(x => x.ObjectiveCode).ToArray() ?? new string[] { }
                                });
            _mockMapper.Setup(m => m.Map<TrainingContentCard>(It.IsAny<TrainingContent>())).Returns((TrainingContent content) => new TrainingContentCard
            {
                ContentName = content.ContentName,
                Id = content.Id,
                DeliveryType = content.DeliveryType,
                Duration = (float)(content.Duration ?? 0),
                LearningObjectiveCode = content.LearningObjectiveCode,
                Note = content.Note,
                TrainingFormat = content.TrainingFormat,
            });
            _mockMapper.Setup(m => m.Map<TrainingUnitCard>(It.IsAny<TrainingUnit>())).Returns((TrainingUnit unit) => new TrainingUnitCard
            {
                UnitCode = unit.UnitCode,
                UnitName = unit.UnitName,
                TrainingContents = unit.TrainingContents.Select(x => _mockMapper.Object.Map<TrainingContentCard>(x)).ToArray(),
                DurationByHour = (float)(unit.TrainingContents.Select(x => x.Duration).Sum() ?? 0)
            });
            _mockMapper.Setup(m => m.Map<AssessmentSchemeDTO>(It.IsAny<AssessmentScheme>())).Returns((AssessmentScheme schema) => new AssessmentSchemeDTO
            {
                Quiz = schema.Quiz,
                Assignment = schema.Assignment,
                Final = schema.Final,
                FinalPractice = schema.FinalPractice,
                FinalTheory = schema.FinalTheory,
                Passing = schema.Passing
            });
            _mockMapper.Setup(m => m.Map<ViewDetailSyllabusDto>(It.IsAny<Syllabus>())).Returns((Syllabus syllabus) =>
            {
                return new ViewDetailSyllabusDto
                {
                    CourseObjective = syllabus.CourseObjective,
                    CreatedBy = syllabus.CreatedBy,
                    CreatedDate = syllabus.CreatedDate.ToString(Value.DateFormat),
                    Id = syllabus.Id,
                    Level = syllabus.Priority,
                    ModifiedBy = syllabus.ModifiedBy,
                    ModifiedDate = syllabus.ModifiedDate?.ToString(Value.DateFormat),
                    Version = syllabus.Version,
                    TrainingPrinciples = syllabus.TrainingPrinciples,
                    TrainingMaterials = syllabus.TrainingMaterials,
                    PublishStatus = syllabus.PublishStatus,
                    SyllabusCode = syllabus.SyllabusCode,
                    SyllabusName = syllabus.SyllabusName,
                    TechnicalRequirement = syllabus.TechnicalRequirement,
                    OutpuStandards = syllabus.SyllabusObjectives.Select(x => x.ObjectiveCode).ToArray(),
                    AssessmentScheme = new AssessmentSchemeDTO() { },
                    Outline = new ViewOutlineSyllabus[] { }
                };
            });
            return _mockMapper;
        }
        public static List<Syllabus> GetFakeDataSyllabus()
        {
            return new Syllabus[]
                {
                new Syllabus {
                    Id = 1,
                    SyllabusName="Basic Code",
                    SyllabusCode="BP21",
                    CreatedDate = DateTime.Parse("03/21/2024"),
                    CreatedBy = "Trinh Huu Tuan",
                    PublishStatus=1,
                    CourseObjective="ádasd",
                    AttendeeNumber=1,
                    ModifiedBy="dấdasdasd",
                    TechnicalRequirement="ádasdasd",
                    ModifiedDate=DateTime.Now,
                    Priority="adasdasD",
                    TrainingMaterials="addasdasd",
                    TrainingPrinciples="adadasdasđá",
                    Version="1.0",
                    AssessmentScheme= new AssessmentScheme()
                    {
                    Assignment=1,
                    Final=1,
                    FinalPractice=1,
                    FinalTheory=1,
                    Passing=1,
                    Quiz=1,
                    Id=1,
                    SyllabusId=1
                    },
                    SyllabusObjectives = new SyllabusObjective[]{
                     new SyllabusObjective(){Id=1, ObjectiveCode="LO01",SyllabusId=1
                     },
                     new SyllabusObjective(){Id=2,ObjectiveCode = "LO02", SyllabusId=1}
                    },
                    TrainingUnits = new TrainingUnit[]
                    {
                        new TrainingUnit()
                        {
                            SyllabusId=1,
                            DayNumber=1,
                            UnitCode=1,
                            UnitName="Introduce",
                            TrainingContents= new TrainingContent[] {
                            new TrainingContent(){
                                Id=1,
                                Duration=20,
                                DeliveryType=1,
                                LearningObjectiveCode="LO01",
                                TrainingFormat="Onlinde",
                                UnitCode=1}
                            }
                        }
                    }
                },
                new Syllabus {
                    Id = 2,
                    SyllabusName="Art Digital",
                    SyllabusCode="Art21",
                    CreatedDate = DateTime.Parse("02/21/2024"),
                    CreatedBy = "Nguyen Hoang Duy",
                    PublishStatus=1,
                    SyllabusObjectives = new SyllabusObjective[]{
                     new SyllabusObjective(){Id=3, ObjectiveCode="LO02",SyllabusId=1
                     },
                     new SyllabusObjective(){Id=4,ObjectiveCode = "LO03", SyllabusId=1}
                    },
                    TrainingUnits = new TrainingUnit[]
                    {
                        new TrainingUnit()
                        {
                            SyllabusId=2,
                            DayNumber=1,
                            UnitCode=2,
                            UnitName="Basic",
                            TrainingContents= new TrainingContent[] {
                            new TrainingContent(){
                                Id=2,
                                Duration=30,
                                DeliveryType=1,
                                LearningObjectiveCode="LO01",
                                TrainingFormat="Onlinde",
                                UnitCode=1}
                            }
                        }
                    }
                },

                new Syllabus {
                    Id = 3,
                    SyllabusName="Ok",
                    SyllabusCode="BPs21",
                    CreatedDate = DateTime.Parse("04/21/2024"),
                    CreatedBy = "Vo Huu Tuan",
                    PublishStatus=1,
                    SyllabusObjectives = new SyllabusObjective[]{
                     new SyllabusObjective(){Id=5, ObjectiveCode="LO03",SyllabusId=1
                     },
                     new SyllabusObjective(){Id=6,ObjectiveCode = "LO04", SyllabusId=1}
                    },
                    TrainingUnits = new TrainingUnit[]
                    {
                        new TrainingUnit()
                        {
                            SyllabusId=3,
                            DayNumber=1,
                            UnitCode=3,
                            UnitName="Practice",
                            TrainingContents= new TrainingContent[] {
                            new TrainingContent(){
                                Id=3,
                                Duration=40,
                                DeliveryType=1,
                                LearningObjectiveCode="LO01",
                                TrainingFormat="Onlinde",
                                UnitCode=1}
                            }
                        }
                    }
                },

                }.ToList();
        }

        public static void SetupMapperToTrainingProgram(this Mock<IMapper> _mockMapper)
        {
            _mockMapper.Setup(x => x.Map<ViewListTrainingProgramDTO>(It.IsAny<TrainingProgram>())).Returns((TrainingProgram src) => new ViewListTrainingProgramDTO()
            {
                TrainingProgramCode = src.TrainingProgramCode,
                Name = src.Name,
                Status = src.Status,
                CreatedBy = src.CreatedBy,
                CreatedDate = src.CreatedDate.ToString(Value.DateFormat),
                DurationByDay = (int)(src.Duration),
                DurationByHour = (int)(src.TrainingProgramSyllabuses.SelectMany(x => x.Syllabus.TrainingUnits).SelectMany(x => x.TrainingContents).Sum(x => x.Duration) ?? 0)
            });
            _mockMapper.Setup(x => x.Map<ViewDetailTrainingProgramDTO>(It.IsAny<TrainingProgram>())).Returns((TrainingProgram src) => new ViewDetailTrainingProgramDTO()
            {
                CreatedBy = src.CreatedBy,
                CreatedDate = src.CreatedDate.ToString(Value.DateFormat),
                Description = "No Info",
                ModifiedBy = src.ModifiedBy,
                ModifiedDate = src.ModifiedDate.Value.ToString(Value.DateFormat),
                Name = src.Name,
                StartTime = src.StartTime.ToString(Value.DateFormat),
                Status = src.Status,
                TopicCode = src.TopicCode,
                TrainingProgramCode = src.TrainingProgramCode
            });
            _mockMapper.Setup(x => x.Map<TrainingProgram>(It.IsAny<CreateTrainingProgramDTO>())).Returns((CreateTrainingProgramDTO src) => new TrainingProgram()
            {
                TrainingProgramCode = 3,
                Name= src.Name
            }) ;
        }

        public static List<TrainingProgram> GetFakeTrainingProgram()
        {
            return  new TrainingProgram[]
            {
                 new TrainingProgram()
                 {
                     TrainingProgramCode=1,
                     UserId=1,
                     CreatedBy="Trinh Huu Tuan",
                     CreatedDate= DateTime.Parse("2024-01-03"),
                     Duration=21,
                     ModifiedBy="Trinhh Huu Tuan",
                     ModifiedDate=DateTime.Parse("2024-01-03"),
                     StartTime= DateTimeOffset.Now,
                     Status=1,
                     Name="asa 1",
                     TopicCode="HNM222",
                     TrainingProgramSyllabuses=new TrainingProgramSyllabus[]
                     {
                         new TrainingProgramSyllabus(){
                         Id=1,
                         Sequence=1,
                         TrainingProgramCode=1,
                         SyllabusId=1,
                         Syllabus=  new Syllabus {
                    Id = 1,
                    SyllabusName="Basic Code",
                    SyllabusCode="BP21",
                    CreatedDate = DateTime.Parse("03/21/2024"),
                    CreatedBy = "Trinh Huu Tuan",
                    PublishStatus=1,
                    CourseObjective="ádasd",
                    AttendeeNumber=1,
                    ModifiedBy="dấdasdasd",
                    TechnicalRequirement="ádasdasd",
                    ModifiedDate=DateTime.Now,
                    Priority="adasdasD",
                    TrainingMaterials="addasdasd",
                    TrainingPrinciples="adadasdasđá",
                    Version="1.0",
                    AssessmentScheme= new AssessmentScheme()
                    {
                    Assignment=1,
                    Final=1,
                    FinalPractice=1,
                    FinalTheory=1,
                    Passing=1,
                    Quiz=1,
                    Id=1,
                    SyllabusId=1
                    },
                    SyllabusObjectives = new SyllabusObjective[]{
                     new SyllabusObjective(){Id=1, ObjectiveCode="LO01",SyllabusId=1
                     },
                     new SyllabusObjective(){Id=2,ObjectiveCode = "LO02", SyllabusId=1}
                    },
                    TrainingUnits = new TrainingUnit[]
                    {
                        new TrainingUnit()
                        {
                            SyllabusId=1,
                            DayNumber=1,
                            UnitCode=1,
                            UnitName="Introduce",
                            TrainingContents= new TrainingContent[] {
                            new TrainingContent(){
                                Id=1,
                                Duration=20,
                                DeliveryType=1,
                                LearningObjectiveCode="LO01",
                                TrainingFormat="Onlinde",
                                UnitCode=1}
                            }
                        }
                    }
                }
                         }
                     }
                 },
                 new TrainingProgram()
                 {
                     TrainingProgramCode=2,
                     UserId=1,
                     CreatedBy="Trinh Huu Tuan",
                     CreatedDate= DateTime.Parse("2024-01-02"),
                     Duration=21,
                     ModifiedBy="Trinhh Huu Tuan",
                     ModifiedDate=DateTime.Parse("2024-01-02"),
                     StartTime= DateTimeOffset.Now.AddDays(7),
                     Status=1,
                     Name="Testing 2",
                     TopicCode="HBF222",
                     TrainingProgramSyllabuses=new TrainingProgramSyllabus[]
                     {
                         new TrainingProgramSyllabus(){
                         Id=1,
                         TrainingProgramCode=2,
                         Sequence=1,
                         SyllabusId=1,
                         Syllabus=  new Syllabus {
                    Id = 1,
                    SyllabusName="Basic Code",
                    SyllabusCode="BP21",
                    CreatedDate = DateTime.Parse("03/21/2024"),
                    CreatedBy = "Trinh Huu Tuan",
                    PublishStatus=1,
                    CourseObjective="ádasd",
                    AttendeeNumber=1,
                    ModifiedBy="dấdasdasd",
                    TechnicalRequirement="ádasdasd",
                    ModifiedDate=DateTime.Now,
                    Priority="adasdasD",
                    TrainingMaterials="addasdasd",
                    TrainingPrinciples="adadasdasđá",
                    Version="1.0",
                    AssessmentScheme= new AssessmentScheme()
                    {
                    Assignment=1,
                    Final=1,
                    FinalPractice=1,
                    FinalTheory=1,
                    Passing=1,
                    Quiz=1,
                    Id=1,
                    SyllabusId=1
                    },
                    SyllabusObjectives = new SyllabusObjective[]{
                     new SyllabusObjective(){Id=1, ObjectiveCode="LO01",SyllabusId=1
                     },
                     new SyllabusObjective(){Id=2,ObjectiveCode = "LO02", SyllabusId=1}
                    },
                    TrainingUnits = new TrainingUnit[]
                    {
                        new TrainingUnit()
                        {
                            SyllabusId=1,
                            DayNumber=1,
                            UnitCode=1,
                            UnitName="Introduce",
                            TrainingContents= new TrainingContent[] {
                            new TrainingContent(){
                                Id=1,
                                Duration=20,
                                DeliveryType=1,
                                LearningObjectiveCode="LO01",
                                TrainingFormat="Onlinde",
                                UnitCode=1}
                            }
                        }
                    }
                }
                         }
                     }
                 },

            }.ToList();
  
        }

        public static void SetupMapperToClass(this Mock<IMapper> _mockMapper)
        {
            _mockMapper.Setup(x=>x.Map<ViewListClassDTO>(It.IsAny<Class>())).Returns((Class obj)=>new ViewListClassDTO
            {
                Id=obj.Id,
                Attendee=obj.Attendee,
                ClassCode=obj.ClassCode,
                ClassName=obj.ClassName,
                CreatedBy=obj.CreatedBy,
                CreatedDate=obj.CreatedDate.ToString(Value.DateFormat),
                Duration=obj.Duration,
                EndDate=obj.EndDate.ToString(Value.DateFormat),
                FSU=obj.FSU,
                Location=obj.Location,
                StartDate=obj.EndDate.ToString(Value.DateFormat),
                Status=obj.Status,
                Trainers=new string[] {}
            });
            _mockMapper.Setup(x => x.Map<ViewDetailClassDTO>(It.IsAny<Class>())).Returns((Class obj) => new ViewDetailClassDTO()
            {
                Attendee = obj.Attendee,
                ClassCode = obj.ClassCode,
                ClassName = obj.ClassName,
                CreatedBy = obj.CreatedBy,
                DurationByDay = obj.Duration,
                CreatedDate = obj.CreatedDate.ToString(Value.DateFormat),
                EndDate = obj.EndDate.ToString(Value.DateFormat),
                FSU = obj.FSU,
                Location = obj.Location,
                Id = obj.Id,
                StartDate = obj.StartDate.ToString(Value.DateFormat),
                Status = obj.Status
            }); ;
        }
    }
}
