using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FAMS.Domain.Models.Entities
{
    public class TrainingProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainingProgramCode { get; set; }

        public string Name { get; set; } = null!;

        public int UserId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public float Duration { get; set; }

        public string? TopicCode { get; set; }

        public int Status { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTimeOffset CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public ICollection<TrainingProgramSyllabus>? TrainingProgramSyllabuses { get; set; }
        public ICollection<Class>? Classes { get; set; }

        public User? User { get; set; }
    }
}
