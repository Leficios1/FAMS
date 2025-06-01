using FAMS.Domain.Dtos;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace FAMS.Api.Services.Interfaces
{
    public interface IDeliveryTypeService
    {
        public Task<IActionResult> Create(CreateDeliveryTypeDto createDeliveryTypeDto);
        public Task<IActionResult> Update(UpdateDeliveryTypeDto updateDeliveryTypeDto);
        public Task<IActionResult> Delete(string typename);
        public Task<IActionResult> GetDeliveryType();

        public Task<IActionResult> GetById(int id);
        public Task<int> GetIdByName(string name);
        public Task<IEnumerable<CountDeliveryTypesDTO>> GetPercentDeliveryType();
    }
}
