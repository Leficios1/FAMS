
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class TrainingUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitCode { get; set; }

        [MaxLength(150)]
        public string UnitName { get; set; } = null!;

        public int DayNumber { get; set; }


        public int SyllabusId { get; set; } 

        public Syllabus? Syllabus { get; set; }

        public ICollection<TrainingContent>? TrainingContents { get; set; }

        public ICollection<ClassTrainerUnit>? TrainerUnits { get; set; }
    }
}
