using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class TrainingProgramResponeDTO
    {
        public int TrainingProgramCode { get; set; }
        public int SyllabusCode { get; set; }
        public string? SyllabusName { get; set; }
        public int SyllabusId { get; set; }
        public Syllabus? Syllabus { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public ICollection<TrainingContent>? TrainingContents { get; set; }
        public float? Duration { get; set; }
        public string Name { get; set; } = null!;
        public int Status { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public string? TopicCode { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTimeOffset CreatedDate { get; set; }
        public int? ClassId { get; set; } = null;
    }
}
