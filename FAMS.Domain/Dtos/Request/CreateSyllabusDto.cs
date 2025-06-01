using FAMS.Api.Dtos;
using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CreateSyllabusDto
    {
        public SyllabusDto GeneralTab { get; set; }
        public DayUnit[] DayUnits { get; set; }
        public NormalAssessmentSchemeRequest OtherScreen { get; set; }
    }

    public class NormalAssessmentSchemeRequest
    {
        public double Quiz { get; set; }

        public double Assignment { get; set; }

        public double Final { get; set; }

        public double FinalTheory { get; set; }

        public double FinalPractice { get; set; }

        public double Passing { get; set; }

        public string trainingPrinciple { get; set; }
    }

}
