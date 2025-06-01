using AutoMapper;
using FAMS.Api.Services.Interfaces;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Constants;
using FAMS.Domain.Dtos;

using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Services
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IBaseRepository<DeliveryType> _deliveryRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Class> _classRepo;
        private readonly FamsContext _context;
        private readonly IBaseRepository<TrainingContent> _trainingContentRepo;

        public DeliveryTypeService(IBaseRepository<DeliveryType> deliveryType, IMapper mapper, IBaseRepository<Class> classRepo
            , FamsContext context, IBaseRepository<TrainingContent> trainingContentRepo)
        {
            _deliveryRepo = deliveryType;
            _mapper = mapper;
            _classRepo = classRepo;
            _context = context;
            _trainingContentRepo = trainingContentRepo;
        }

        public async Task<IActionResult> GetDeliveryType()
        {
            var types = await _deliveryRepo.Find();
            if (types == null)
            {
                return new OkObjectResult(EMS.EM87);
            }
            return new OkObjectResult(types);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var types = await _deliveryRepo.GetById(id);
            if (types == null)
            {
                return new OkObjectResult(EMS.EM87);
            }
            return new OkObjectResult(types);
        }
        public async Task<IActionResult> Create(CreateDeliveryTypeDto createDeliveryTypeDto)
        {
            var content = await _deliveryRepo.Get().Where(x => x.TypeName.ToUpper().Equals(createDeliveryTypeDto.TypeName.ToUpper())).FirstOrDefaultAsync();
            if (content != null) return new BadRequestObjectResult(EMS.EM105 + createDeliveryTypeDto.TypeName);
            var map = _mapper.Map<DeliveryType>(createDeliveryTypeDto);
            await _deliveryRepo.AddAsync(map);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Successfully");
        }

        public async Task<IActionResult> Delete(string typemane)
        {
            var deliveryType = await _deliveryRepo.Get().FirstOrDefaultAsync(x => x.TypeName.ToUpper().Equals(typemane.ToUpper()));
            if (deliveryType == null) return new BadRequestObjectResult(EMS.EM58 + typemane);
            _deliveryRepo.Delete(deliveryType);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Delete Successfully");
        }

        public async Task<IActionResult> Update(UpdateDeliveryTypeDto updateDeliveryTypeDto)
        {
            try
            {
                var type = await _deliveryRepo.GetById(updateDeliveryTypeDto.Id);
                if (type == null)
                {
                    return new BadRequestObjectResult("not found Delivery Name: " + updateDeliveryTypeDto.TypeName);
                }
                _mapper.Map(updateDeliveryTypeDto, type);
                _deliveryRepo.Update(type);
                await _deliveryRepo.SaveChangesAsync();
                return new OkObjectResult(type);
            }
            catch (Exception ex)
            {
                throw new Exception(EMS.EMS106);
            }
        }
        public async Task<int> GetIdByName(string name)
        {
            var types = await _context.DeliveryTypes.Where(x => x.TypeName.ToUpper().Equals(name.ToUpper())).FirstOrDefaultAsync();
            if (types == null)
            {
                return -1;
            }
            else return types.Id;
        }

        public async Task<IEnumerable<CountDeliveryTypesDTO>> GetPercentDeliveryType()
        {
            var dlvt = await _context.DeliveryTypes.ToListAsync();
            var totalDuration = await _context.TrainingContents.SumAsync(d => d.Duration ?? 0);

            var result = new List<CountDeliveryTypesDTO>();
            foreach (var item in dlvt)
            {
                var total = await _context.TrainingContents.Where(c => c.DeliveryType == item.Id).SumAsync(tc => tc.Duration ?? 0);
                var percentage = Math.Round((totalDuration == 0 ? 0 : (total / totalDuration) * 100), 2);

                result.Add(new CountDeliveryTypesDTO
                {
                    DeliveryTypeId = item.Id,
                    DeliveryTypeName = item.TypeName,
                    TotalDuration = total,
                    PercentageOfTotal = percentage
                });
            }
            return result;
        }
    }
}
