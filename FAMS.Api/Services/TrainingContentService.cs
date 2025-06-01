using FAMS.Core.Databases;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Services
{
    public class TrainingContentService : ITrainingContentService
    {
        private readonly IBaseRepository<TrainingContent> _trainingContentRepo;
        private readonly IBaseRepository<Material> _materialRepo; 
        public TrainingContentService(IBaseRepository<TrainingContent> trainingContentRepo
            , IBaseRepository<Material> materialRepo)
        {
            _trainingContentRepo = trainingContentRepo;
            _materialRepo = materialRepo;
        }

        public async Task AddTrainingContent(TrainingContent trainingContent)
        {
            if (trainingContent.TrainingFormat == null) { throw new Exception(EMS.EM89); }
            if (trainingContent.Duration == 0) { throw new Exception(EMS.EM90); }
            if (trainingContent.ContentName == null) { throw new Exception(EMS.EM91); }
            if (trainingContent != null)
            {
                await _trainingContentRepo.AddAsync(trainingContent);
                await _trainingContentRepo.SaveChangesAsync();
            }
            var result = (await _trainingContentRepo.Find(tc => tc.TrainingUnit == trainingContent.TrainingUnit))?.OrderByDescending(tc => tc.TrainingUnit).FirstOrDefault();
            if (result == null) { throw new Exception($"Not add training content: {trainingContent.ContentName}"); }
        }

        public async Task<IActionResult> getTimeAllocation(int syllabusId)
        {
            var list = await _trainingContentRepo.Get()
                .Where(x => x.TrainingUnit.Syllabus.Id == syllabusId)
                .ToListAsync();

            if (list.Any())
            {
                var totalDuration = list.Sum(x => x.Duration);

                if (totalDuration.HasValue && totalDuration.Value > 0)
                {
                    var Assignment_Lab = (float)list.Where(x => x.DeliveryType == 1).Sum(x => x.Duration) / totalDuration.Value;
                    var Concept_Lecture = (float)list.Where(x => x.DeliveryType == 2).Sum(x => x.Duration) / totalDuration.Value;
                    var Guide_Review = (float)list.Where(x => x.DeliveryType == 3).Sum(x => x.Duration) / totalDuration.Value;
                    var Test_Quiz = (float)list.Where(x => x.DeliveryType == 4).Sum(x => x.Duration) / totalDuration.Value;
                    var Exam = (float)list.Where(x => x.DeliveryType == 5).Sum(x => x.Duration) / totalDuration.Value;

                    var roundObject = new
                    {
                        Assignment_Lab = Math.Round(Assignment_Lab, 2),
                        Concept_Lecture = Math.Round(Concept_Lecture, 2),
                        Guide_Review = Math.Round(Guide_Review, 2),
                        Test_Quiz = Math.Round(Test_Quiz, 2),
                        Exam = Math.Round(Exam, 2)
                    };

                    return new OkObjectResult(roundObject);
                }
                else
                {
                    return new BadRequestObjectResult(EMS.EM92);
                }
            }
            else
            {
                return new BadRequestObjectResult(EMS.EM93 + syllabusId);
            }
        }


        public Task<IActionResult> GetByContentId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTrainingContentById(int contentId)
        {   
            var removedContent = await _trainingContentRepo.Get().FirstOrDefaultAsync(x=>x.Id==contentId);
            if (removedContent == null) throw new Exception(EMS.EM94 + contentId);
           
            _trainingContentRepo.Delete(removedContent);
            await _trainingContentRepo.SaveChangesAsync();
           
        }
        public async Task UpdateTrainingUnit(TrainingContent content)
        {
            var updatedContent = await _trainingContentRepo.Get().FirstOrDefaultAsync(x=>x.Id==content.Id);

            if (updatedContent == null) throw new Exception(EMS.EM95 + content.Id);

            updatedContent.Note = content.Note;
            updatedContent.UnitCode = content.UnitCode;
            updatedContent.Duration = content.Duration;
            updatedContent.ContentName = content.ContentName;
            updatedContent.DeliveryType = content.DeliveryType;
            updatedContent.Duration= content.Duration;
            updatedContent.TrainingFormat = content.TrainingFormat;
          

            _trainingContentRepo.Update(updatedContent);
            await _trainingContentRepo.SaveChangesAsync();
        }
        public async Task AddTrainingContentBasic(TrainingContent content)
        {
            if (content.TrainingFormat == null) { throw new Exception(EMS.EM89); }
            if (content.Duration == 0) { throw new Exception(EMS.EM90); }
            if (content.ContentName == null) { throw new Exception(EMS.EM91); }

            await _trainingContentRepo.AddAsync(content);
            await _trainingContentRepo.SaveChangesAsync();

            
        }
    }
}
