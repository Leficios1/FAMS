using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Dtos.Response
{
    public class DashboardDto
    {
        public int ActiveTrainer { get; set; }
        public int InactiveTrainer { get; set; }
        public int OpeningClass {  get; set; }
        public int PlanningClass {  get; set; }
        public int ActiveSyllabus {  get; set; }
        public int InactiveSyllabus { get; set; }
        public int ActiveTraining { get; set; }
        public int InactiveTraining { get; set; }
    }
}
