using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface ITrainingUnitService
    {
        Task<TrainingUnit> AddTrainingUnit(TrainingUnit trainingUnit);
        public Task UpdateTrainingUnit(TrainingUnit unit);
        public Task DeleteTrainingUnit(int unitId);
    }
}
