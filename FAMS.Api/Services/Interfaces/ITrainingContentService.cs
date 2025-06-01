using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface ITrainingContentService
    {
        public Task AddTrainingContent(TrainingContent trainingContent);
        Task<IActionResult> getTimeAllocation(int syllabusId);

        public Task RemoveTrainingContentById(int contentId);
        public Task UpdateTrainingUnit(TrainingContent content);
        public  Task AddTrainingContentBasic(TrainingContent content);
    }
}
