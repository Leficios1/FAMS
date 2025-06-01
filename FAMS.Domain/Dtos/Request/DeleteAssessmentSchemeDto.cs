using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class DeleteAssessmentSchemeDto
    {
        public int Id { get; set; }

        public int SyllabusId { get; set; }

        public double Quiz { get; set; }

        public double Assignment { get; set; }

        public double Final { get; set; }

        public double FinalTheory { get; set; }

        public double FinalPractice { get; set; }

        public double Passing { get; set; }
    }
}
