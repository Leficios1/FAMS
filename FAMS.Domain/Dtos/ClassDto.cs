using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos
{
    public class ClassDto
    {
        public string ClassName { get; set; } = null!;
        public string? ClassCode { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int Duration { get; set; }
        public byte Status { get; set; }
        public string Location { get; set; }
        public string? FSU { get; set; }
    }
}
