using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class TrainingContentResponseDTO
    {
        public string? TrainingFormat { get; set; }
        public float? Duration { get; set; }
        public string ContentName { get; set; } = null!;
        public int DeliveryType { get; set; }
        public TrainingUnit? TrainingUnit { get; set; }

    }
}
