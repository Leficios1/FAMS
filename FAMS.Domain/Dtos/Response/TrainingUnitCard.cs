using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class TrainingUnitCard
    {
        public int UnitCode { get; set; } = 0;
        public string UnitName { get; set; } = "No Information"!;
        public float DurationByHour { get; set; } = 0;

        public TrainingContentCard[] TrainingContents { get; set; } = new TrainingContentCard[] { };
    }
}
