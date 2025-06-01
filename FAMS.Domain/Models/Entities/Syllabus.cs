using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FAMS.Domain.Models.Entities
{
    public class Syllabus
    {
        [Key]
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SyllabusCode { get; set; } = null!;

        [MaxLength(150)]
        public string SyllabusName { get; set; } = null!;

      
        public string? TechnicalRequirement { get; set; }

        public string? Version { get; set; }

        public int AttendeeNumber { get; set; }

        public string? CourseObjective { get; set; }

        public string? TrainingMaterials { get; set; }

        public string? TrainingPrinciples { get; set; }

        public string? Priority { get; set; }

        /// <summary>
        /// 3 states - Draft, Active, Inactive
        /// </summary>
        public int PublishStatus { get; set; }

        public int UserId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public ICollection<TrainingProgramSyllabus>? TrainingProgramSyllabuses { get; set; }

        public ICollection<SyllabusObjective>? SyllabusObjectives { get; set; }

        public ICollection<TrainingUnit>? TrainingUnits { get; set; }

        public User? User { get; set; } 

        public AssessmentScheme AssessmentScheme { get; set; } = null!;
    }
}
