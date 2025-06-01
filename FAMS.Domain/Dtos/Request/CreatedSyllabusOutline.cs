using FAMS.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CreatedSyllabusOutline
    {
        public int Id { get; set; }
        public DayUnit[] dayUnits { get; set; }

    }
    public class DayUnit
    {
        public int DayNumber { get; set; }
        public TrainingUnitOutline[] trainingUnits { get; set; }
    }
    public class TrainingUnitOutline
    {
        public string? UnitName { get; set; }
        public TrainingContentDto[]? TrainingContents { get; set; }
    }
}
