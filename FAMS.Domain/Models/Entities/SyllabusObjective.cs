using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class SyllabusObjective : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SyllabusObjectiveId")]
        public int Id { get; set; }

        public int SyllabusId { get; set; }

        public string ObjectiveCode { get; set; } = null!;

        public Syllabus? Syllabus { get; set; }

        public LearningObjective? LearningObjective { get; set; }
    }
}
