using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class DeleteDeliveryTypeDto
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
    }
}
