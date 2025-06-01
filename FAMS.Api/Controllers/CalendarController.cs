using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using FAMS.Domain.Models.Dtos.Request;

namespace FAMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ICalendarService _calendarService;
        public CalendarController(ICalendarService calendarService, IClassService classService)
        {
            _calendarService = calendarService;
            _classService = classService;
        }
        [HttpGet("calendar/by-userid/{userid}/type/{type}")]
        public async Task<IActionResult> GetClass([FromRoute] int userid, DateTime? time, [FromRoute] string type)
        {
            var returnobj = await _calendarService.ViewTrainingCalendar(userid, type, time);
            return Ok(returnobj);
        }
        [HttpPut("calendar/By-ClassID")]
        public async Task<IActionResult> UpdateCalendarByClassID(CalenderUpdateDTO dto)
        {
            var returnobj = await _calendarService.UpdateCalenderByClassID(dto);
            return StatusCode((int)returnobj.statusCode, returnobj.Data != null ? returnobj.Data : returnobj.Errormessge);
        }

    }
}
