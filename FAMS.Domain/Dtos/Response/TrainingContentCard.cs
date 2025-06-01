using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class TrainingContentCard
    {
        public int Id { get; set; } = 0;

        public string LearningObjectiveCode { get; set; } =string.Empty;


        public string ContentName { get; set; } = string.Empty;

        public int DeliveryType { get; set; } = 1;

        public float Duration { get; set; } = 0;

        public string TrainingFormat { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public MaterialDto[] Materials { get; set; }= new MaterialDto[0];
    }
}
