using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CreateTrainingProgramDTO 
    {
        [Required(ErrorMessage = "Program name is required.")]
        public string Name { get; set; } = null!;

        public int UserId { get; set; }
        [Required(ErrorMessage = "General information is required.")]
        public string? StartTime { get; set; } = string.Empty;
        [Required(ErrorMessage = "General information is required.")]
        public float Duration { get; set; }
        [Required(ErrorMessage = "General information is required.")]
        public string TopicCode { get; set; } = null!;

        [Required(ErrorMessage = "General information is required.")]
        public int Status { get; set; } = 0;
        [Required(ErrorMessage = "General information is required.")]
        public string CreatedBy { get; set; } = null!;
        


        public int[]? ClassIds { get; set; } = new int[] { };

        [Required(ErrorMessage = "List of syllabuses is required.")]
        public CreateTrainingProgramSyllabusDTO[]? SyllabusDTOs { get; set; } = new CreateTrainingProgramSyllabusDTO[] { };
    }

    public class CreateTrainingProgramSyllabusDTO
    {
        [Required(ErrorMessage = "Syllabus id is required.")]
        public int SyllabusId { get; set; }
        [Required(ErrorMessage = "Sequence is required.")]
        public int Sequence { get; set; }
    }
}
