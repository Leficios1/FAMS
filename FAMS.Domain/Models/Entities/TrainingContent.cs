using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class TrainingContent : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TrainingContentId")]
        public int Id { get; set; }

        [MaxLength(4)]
        public string LearningObjectiveCode { get; set; } = null!;

        public int UnitCode { get; set; }

        public string ContentName { get; set; } = null!;

        public int DeliveryType { get; set; }

        public float? Duration { get; set; }

        public string? TrainingFormat { get; set; }

        public string? Note { get; set; }

        public TrainingUnit? TrainingUnit { get; set; }

        public LearningObjective? LearningObjective { get; set; }

        public DeliveryType? Delivery { get; set; }

        public virtual ICollection<Material> Materials { get; set; }    
    }
}
