using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CreateDeliveryTypeDto
    {
        [Required()]
        public string? TypeName { get; set; }
    }
}
