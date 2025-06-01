using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class LearningObjective
    {
        [Key]
        [MaxLength(4)]
        public string ObjectiveCode { get; set; } = null!;

        [MaxLength(200)]
        public string Description { get; set; } = null!;
        
        public ICollection<SyllabusObjective>? SyllabusObjectives { get; set; }

        public ICollection<TrainingContent>? TrainingContents { get; set; }
    }
}
