using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewListSyllabusDTO
    {
        public int Id { get; set; }
        public string? SyllabusName { get; set; } = string.Empty;

        public string? SyllabusCode {  get; set; } = string.Empty;

        public string? CreatedDate { get; set; } = string.Empty;
        
        public string? CreatedBy { get; set; } =string.Empty;

        public int DurationByDay { get; set; } = 0;

        public string[]? OutputStandards { get; set; } = new string[] { };

        public int PublishStatus { get; set; } = 0;
    }
}
