using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
   public class TrainingUnitResponseDTO
    {
        public string UnitName { get; set; } = null!;
        public int DayNumber { get; set; }
        public int SyllabusId { get; set; }
        public int UnitCode { get; set; }
    }
}
