using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class TrainingProgramSyllabus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sequence { get; set; }

        public int SyllabusId { get; set; } 

        public int TrainingProgramCode { get; set; }

        public Syllabus? Syllabus { get; set; }

        public TrainingProgram? TrainingProgram { get; set; }
    }
}
