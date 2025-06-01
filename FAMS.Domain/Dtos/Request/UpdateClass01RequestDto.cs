using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class UpdateClass01RequestDto
    {
        public int Id { get; set; }
        public string? ClassName { get; set; } = null!;
        public string? ClassCode { get; set; } = null!;
        public string? Location { get; set; }
        public int Duration { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public DateTime? ClassTimeStart { get; set; }

        public DateTime? ClassTimeEnd { get; set; }
        public string? Attendee { get; set; }
        public string? FSU { get; set; }
        public string? ModifiedBy { get; set; }
        public int[]? AdminId { get; set; }
        public TrainerUnit[]? Trainers { get; set; }
        public int? TrainingProgramCode { get; set; }
        public List<DateTime>? DateAndTimeStudy { get; set; }


        
    }
  

}
