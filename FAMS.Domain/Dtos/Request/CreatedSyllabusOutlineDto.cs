using FAMS.Domain.Models.Dtos.Response;

namespace FAMS.Api.Dtos
{
    public class CreatedSyllabusOutlineDto
    {
        public int Id { get; set; }
        public TrainingUnitDto[]? syllabusOutlines { get; set; }
    }
    public class TrainingUnitDto
    {
        public int UnitCode { get; set; }
        public string? UnitName { get; set; }
        public int? DayNumber { get; set; }
        public TrainingContentDto[]? TrainingContents { get; set; }
    }

    public class TrainingContentDto
    {
        public string? ContentName { get; set; }
        public string LearningObjectiveCode { get; set; }
        public int? DeliveryType { get; set; }
        public double? Duration { get; set; } 
        public string? TrainingFormat { get; set; }
        public string? Note { get; set; }
        public CMaterialDto[]? Materials { get; set; }
    }
    public class CMaterialDto
    {
        public string Title { get; set; }
        public DateTimeOffset createdOn { get; set; }
        public string createdBy { get; set; }
        public string Url { get; set; }
    }
}