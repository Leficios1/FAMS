using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Entities
{
    public class ClassTrainerUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int UnitCode { get; set; }
        public string Location { get; set; } = string.Empty;
        public int TrainerId { get; set; }
        public int ClassId { get; set; }

        public User? Trainer { get; set; }
        public TrainingUnit? TrainingUnit { get; set; }

        public Class? Class { get; set; }

    }
}
