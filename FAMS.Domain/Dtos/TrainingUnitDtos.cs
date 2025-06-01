using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos
{
    public class TrainingUnitDtos
    {
        public int UnitCode { get; set; }
        public int DayNumber { get; set; }
        public string UnitName { get; set; } = null!;
        public int SumDuration { get; set; }
        public List<TrainingContentDtos> TrainingContents { get; set; }
    }
}
