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
    public class TrainingProgramDtoRequest
    {
        public int TrainingProgramCode { get; set; }

        public string Name { get; set; } = null!;

        public int UserId { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public float Duration { get; set; }

        public string? TopicCode { get; set; }

        public int Status { get; set; }

        [JsonIgnore]
        public string? ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset? ModifiedDate { get; set; } = DateTimeOffset.UtcNow;
        public CreateTrainingProgramSyllabusDTO[] TrainingProgramSyllabus { get; set; }

    }
}
