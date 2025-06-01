using FAMS.Api.Services;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Dtos;
using FAMS.Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FAMS.Api.Controllers
{

    [ApiController]

    public class DeliveryTypeController : BaseApiController
    {
        private readonly IDeliveryTypeService _deliveryTypeService;
        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService,ITrainingContentService trainingContentService)
        { 
         _deliveryTypeService = deliveryTypeService;
        }


        [HttpGet("delevery-types/get-all")]
        public async Task<IActionResult> Get()
        { 
          var result = await _deliveryTypeService.GetDeliveryType();
            return result;
        }

        [HttpPost("delivery-types")]
        public async Task<IActionResult> Create(CreateDeliveryTypeDto createDeliveryTypeDto)
        {
            var result = await _deliveryTypeService.Create(createDeliveryTypeDto);
            return result;
        }

        [HttpDelete("delivery-types")]
        public async Task<IActionResult> Delete(string typeName)
        {
            var result = await _deliveryTypeService.Delete(typeName);
            return result;
        }

        [HttpPut("delivery-types")]
        public async Task<IActionResult> Update(UpdateDeliveryTypeDto updateDeliveryTypeDto)
        {
            var result = await _deliveryTypeService.Update(updateDeliveryTypeDto);
            return Ok("Update Delivery Type Successfully!");
        }

        [HttpGet("delivery-types")]
        public async Task<ActionResult<IEnumerable<CountDeliveryTypesDTO>>> GetTotalDurationByDeliveryType()
        {
            var result = await _deliveryTypeService.GetPercentDeliveryType();
            return Ok(result);
        }
    }
}
