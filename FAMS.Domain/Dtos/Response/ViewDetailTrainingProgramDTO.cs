using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewDetailTrainingProgramDTO
    {
        public int TrainingProgramCode { get; set; }
        public string Name { get; set; } = "No Information";
        public string StartTime { get; set; } = "No ";
        public float DurationByHour { get; set; } = 0;
        public int DurationByDay { get; set; } = 0;
        public string Description { get; set; } ="No Information";
        public string TopicCode { get; set; } = "No Information";
        public int Status { get; set; } = 0;
        public string CreatedBy { get; set; } = "No Information";
        public string CreatedDate { get; set; } = "No Information"; 
        public string ModifiedBy { get; set; } = "No Information";
        public string ModifiedDate { get; set; } = "No Information";
        public ViewListClassDTO[] AssignedClass { get; set; } = new ViewListClassDTO[] { };
        public SyllabusCard[] Outline { get; set; } =new SyllabusCard[] { };
    }
}
