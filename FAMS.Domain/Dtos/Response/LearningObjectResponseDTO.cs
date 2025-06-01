using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class LearningObjectResponseDTO
    {
        public int SyllabusId { get; set; }
        public int UnitCode { get; set; }
        public ICollection<TrainingContent>? TrainingContents { get; set; }
        public ICollection<SyllabusObjective>? SyllabusObjectives { get; set; }
        public string ObjectiveCode { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
