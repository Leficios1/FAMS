namespace FAMS.Api.Dtos
{
    public class SyllabusDto
    {
        public string SyllabusCode { get; set; } = null!;
        public string SyllabusName { get; set; } = null!;
        public int TrainingAudience { get; set; }
        public string? TechnicalRequirement { get; set; }
        public string? CourseObjective { get; set; }
        public string? Level { get; set; }
        public string CreatedBy { get; set; } = null!;
        public int UserId { get; set; }

    }
}
