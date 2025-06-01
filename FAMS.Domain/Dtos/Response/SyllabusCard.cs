using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class SyllabusCard
    {
        public int Sequence { get; set; }
        public int Id { get; set; }
        
        public string SyllabusName { get; set; } = string.Empty;

        public string SyllabusCode { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public int PublishStatus { get; set; } 

        public string ModifiedBy { get; set; } = string.Empty;

        public string ModifiedDate { get; set; } = string.Empty;

        public int DurationByDay { get; set; } 

        public float DurationByHour { get; set; } 

    }
}
