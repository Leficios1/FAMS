using AutoMapper;
using FAMS.Api.Dtos;
using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Services
{
    public class TrainingUnitService : ITrainingUnitService
    {

        private readonly IBaseRepository<TrainingUnit> _trainingUnitRepo;
        private readonly IMapper _mapper;
 
        public TrainingUnitService(IBaseRepository<TrainingUnit> trainingUnitRepo, IMapper mapper)
        {
            _trainingUnitRepo = trainingUnitRepo;
            _mapper = mapper;
        }


        public async Task<TrainingUnit> AddTrainingUnit(TrainingUnit trainingUnit)
        {
            if (trainingUnit.UnitName == null) { throw new Exception($"Unit name is required."); }
            if (trainingUnit.DayNumber == null) { throw new Exception($"Day numbers error!"); }
            if (trainingUnit != null)
            {
                await _trainingUnitRepo.AddAsync(trainingUnit);
                await _trainingUnitRepo.SaveChangesAsync();
            }

            var result = (await _trainingUnitRepo.Find(tu => tu.SyllabusId == trainingUnit.SyllabusId))?.OrderByDescending(tu => tu.UnitCode).FirstOrDefault();
            if (result == null)
            {
                throw new Exception($"Not add training unit name: {trainingUnit.UnitName}");
            }
            return result;

        }

        public async Task DeleteTrainingUnit (int unitId)
        {
            var removedUnit = await _trainingUnitRepo.Get().FirstOrDefaultAsync(x=>x.UnitCode==unitId);
            if (removedUnit == null) throw new Exception("There is no unit that has code: " + unitId);
            var contents =  await _trainingUnitRepo.Get().Where(x=>x.UnitCode==unitId).Select(x=>x.TrainingContents).ToListAsync();
            if (contents != null && contents.Any()) throw new Exception("Can not delete this unit");
             _trainingUnitRepo.Delete(removedUnit);
            await _trainingUnitRepo.SaveChangesAsync();

        }

        public async Task UpdateTrainingUnit(TrainingUnit unit)
        {
            var updatedUnit = await _trainingUnitRepo.Get().FirstOrDefaultAsync(x=>x.UnitCode==unit.UnitCode);

            if (updatedUnit == null) throw new Exception("There is not unit that has code: "+unit.UnitCode);

            updatedUnit.UnitName = unit.UnitName;
            updatedUnit.DayNumber = unit.DayNumber; 
            
            _trainingUnitRepo.Update(updatedUnit);
            await _trainingUnitRepo.SaveChangesAsync();
        }
    }
}
