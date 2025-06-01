using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewOutlineSyllabus
    {
        public int DayNumber { get; set; }

        public TrainingUnitCard[]? TrainingUnits { get; set; }
    }
}
