using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class SyllabusDtoResponse
    {
        public string? SyllabusCode { get; set; }
        public string? SyllabusName { get; set; }
        public string? CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public int Duration { get; set; }
        public IEnumerable<string>? OutStandards { get; set; }
        public byte PublishStatus { get; set; }
    }
}
