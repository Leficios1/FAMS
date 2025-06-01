using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FAMS.Domain.Models.Entities
{
    public class Class : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int TrainingProgramCode { get; set; }


        public string ClassName { get; set; } = null!;


        public string ClassCode { get; set; }

        public int Duration { get; set; }

        public string Status { get; set; }

        public string? Location { get; set; }
        public DateTime? ClassTimeEnd { get; set; }

        public string Attendee { get; set; } = string.Empty;

        public DateTime? ClassTimeStart { get; set; }

        public string FSU { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTimeOffset CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }

        public ICollection<ClassUser>? Admins { get; set; }

        public TrainingProgram? TrainingProgram { get; set; }

        public virtual ICollection<CalendarClass>? CalendarClasses { get; set; }

        public virtual ICollection<ClassTrainerUnit>? TrainerUnits { get; set; }

    }
}
