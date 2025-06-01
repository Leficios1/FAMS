using FAMS.Domain.Models.Dtos.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class UpdateSyllabusRequest
    {
        [Required(ErrorMessage ="Id is required.")]
        public  int Id { get; set; }
        [Required(ErrorMessage = "Syllabus name is required.")]
        public string? SyllabusName { get; set; } 
        [Required(ErrorMessage = "Technical requirement is required.")]
        public string? TechnicalRequirement { get; set; }
        [Required(ErrorMessage = "Attendee number is required.")]
        public int AttendeeNumber { get; set; }
        [Required(ErrorMessage = "Course objective is required.")]
        public string? CourseObjective { get; set; }
        [Required(ErrorMessage = "Principles is required.")]
        public string? TrainingPrinciples { get; set; }
        [Required(ErrorMessage = "Priority is required.")]
        public string? Level { get; set; }
        public string? ModifiedBy { get; set; }

        [Required(ErrorMessage ="Output stardards is required.")]

        public UpdateAssessmentSchemeDTO? Schema { get; set; } = null;

        public UpdateOutlineDTO[] Outline { get; set; }
    }
  
    public class UpdateAssessmentSchemeDTO
    {
        public double Quiz { get; set; }
        public double Assignment { get; set; }
        public double Final { get; set; }
        public double FinalTheory { get; set; }
        public double FinalPractice { get; set; }

        public double Passing { get; set; }
    }
    public class UpdateOutlineDTO
    {
        public int DayNumber { get; set; } = 1;
        public UpdateTrainingUnitDTO[] TrainingUnits {  get; set; } 
    }
    public class UpdateTrainingUnitDTO
    {

        public int? UnitCode { get; set; }
        [Required(ErrorMessage ="Unit name is required.")]
        public string? UnitName { get; set; }
        [Required(ErrorMessage = "Day number is required.")]
        public UpdateTrainingContentDTO[] TrainingContents { get; set; }
    }

    public class UpdateTrainingContentDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Content name is required.")]
        public string? ContentName { get; set; }
        [Required(ErrorMessage = "Learning objective code is required.")]
        public string? LearningObjectiveCode { get; set; }
        public int DeliveryType { get; set; } = 1;
        public float Duration { get; set; } = 0;
        [Required(ErrorMessage = "Training format is required.")]
        
        public string? TrainingFormat { get; set; }
        public string? Note { get; set; }
        public MaterialDto[] Materials { get; set; } 
    }
}
