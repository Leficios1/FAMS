using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewDetailSyllabusDto
    {
        public int Id { get; set; }
        public string SyllabusCode { get; set; } = null!;
        public string SyllabusName { get; set; } = null!;
        public int PublishStatus { get; set; }
        public string? Level { get; set; }
        public string? TechnicalRequirement { get; set; }
        public string? TrainingMaterials { get; set; }
        public string? TrainingPrinciples { get; set; }
        public string? CourseObjective { get; set; }
        public string? Version { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string[]? OutpuStandards { get; set; } = new string[] { };
        public AssessmentSchemeDTO AssessmentScheme { get; set; } = null!;
        public ViewOutlineSyllabus[]? Outline { get; set; } = new ViewOutlineSyllabus[] { };

    }
    public class AssessmentSchemeDTO
    {
        public int SyllabusId { get; set; }

        public double Quiz { get; set; }

        public double Assignment { get; set; }

        public double Final { get; set; }

        public double FinalTheory { get; set; }

        public double FinalPractice { get; set; }

        public double Passing { get; set; }
    }
}
