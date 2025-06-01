using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public int ContentId { get; set; }
    }
}
