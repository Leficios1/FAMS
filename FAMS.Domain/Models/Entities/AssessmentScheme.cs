using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class AssessmentScheme : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AssessmentSchemeId")]
        public int Id { get; set; }
        
        public int SyllabusId { get; set; }
        
        public double Quiz { get; set; }
        
        public double Assignment { get; set; }
        
        public double Final { get; set; }
        
        public double FinalTheory { get; set; }
        
        public double FinalPractice { get; set; }
        
        public double Passing { get; set; }

        public Syllabus? Syllabus { get; set; }
    }
}
