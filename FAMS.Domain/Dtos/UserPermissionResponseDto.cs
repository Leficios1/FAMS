using System.ComponentModel.DataAnnotations;

namespace FAMS.Api.Dtos
{
    public class UserPermissionResponseDto
    {
        public string PermissionId { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public byte Syllabus { get; set; }

        public byte TrainingProgram { get; set; }

        public byte Class { get; set; }

        public byte LearningMaterial { get; set; }

        public byte UserManagement { get; set; }
    }
}
