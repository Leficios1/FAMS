using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewListClassDTO
    {
        public int Id { get; set; }
        public string? ClassName { get; set; } = string.Empty;
        public string? ClassCode { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public string? Location { get; set; } = string.Empty;
        public string? FSU { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? CreatedDate { get; set; } =  string.Empty;

        public string? Attendee { get; set; } = string.Empty;
        public string? StartDate { get; set; } = string.Empty;
        public string? EndDate { get; set; } = string.Empty;

        public string[] Trainers { get; set; } = new string[] { };
    }
}
