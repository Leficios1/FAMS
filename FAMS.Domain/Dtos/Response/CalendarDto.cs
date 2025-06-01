using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class CalendarDto
    {
        public DayCalendarDto[] dayCalendarDTOs { get; set; }
    }
    public class DayCalendarDto
    {
        public string  DayofWeek { get; set; }
        public SlotCalendarDto[] slotCalendarDTOs { get; set; }
    }
    public class SlotCalendarDto
    {
        public string ClassName { set; get; }
        public string Status { set; get; }
        public DateTime? Time { set; get; }
        public string ClassTime { set; get; }
        //"3:30-5:30"
        public string TrainingProgramName {  set; get; }
        public int CurentSlot { set; get; }
        public int TotalSlot { set; get;}
        public string TrainerName { set; get; }
        public string AdminName {  set; get; }
    }
}
