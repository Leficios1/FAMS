using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewListTrainingProgramDTO
    {
        public int TrainingProgramCode  { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? CreatedDate { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public int DurationByDay { get; set; }

        public int Status { get; set; }

        public int DurationByHour { get; set; }
    }
}
