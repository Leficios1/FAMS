using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Entities
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string? Title { get; set; } = null;
        public DateTimeOffset createdOn { get; set; }
        public string? createdBy { get; set; } = null; 
        public string? Url { get; set; } = null;
        public virtual TrainingContent? TrainingContent { get; set; }
    }
}
