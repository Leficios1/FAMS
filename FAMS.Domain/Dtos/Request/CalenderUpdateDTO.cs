using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class CalenderUpdateDTO
    {
        public int  ClassID { get; set; }
        public List<DateTime> DateAndTimeStudy { get; set; }
    }
}
