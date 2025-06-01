using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class UserPermission
    {
        [Key]
        [Column(TypeName = "char(2)")]
        public string PermissionId { get; set; } = null!;

        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        public byte Syllabus { get; set; }

        public byte TrainingProgram { get; set; }

        public byte Class { get; set; }

        public byte LearningMaterial { get; set; }

        public byte UserManagement { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
