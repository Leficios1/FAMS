using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Dtos
{
    public class CountDeliveryTypesDTO
    {
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
        public float TotalDuration { get; set; }
        public double PercentageOfTotal { get; set; }
    }
}
