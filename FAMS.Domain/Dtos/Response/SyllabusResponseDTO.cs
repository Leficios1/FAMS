using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class SyllabusResponseDTO
    {
        public string SyllabusCode { get; set; } = null!;
        public string SyllabusName { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public int TrainingAudience { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int Duration { get; set; }
        public IEnumerable<string> OutStandards { get; set; } = null!;
        public byte PublishStatus { get; set; }
        public string? Version { get; set; }
        public string? TechnicalRequirement { get; set; }
        public string? CourseObjective { get; set; }
        public int UserId { get; set; }
    }
}
