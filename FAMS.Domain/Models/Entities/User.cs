using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMS.Domain.Models.Entities
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [MaxLength(12)]
        public string? Phone { get; set; }

        [MaxLength(50)]
        public string Password { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        [Url]
        public string? AvatarUrl { get; set; } = string.Empty!;

        [MaxLength(10)]
        public string? Gender { get; set; }

        public string PermissionId { get; set; } = null!;

        public bool Status { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public UserPermission? UserPermission { get; set; }

        public ICollection<Syllabus>? Syllabuses { get; set; }

        public ICollection<ClassUser>? ClassUsers { get; set; }

        public ICollection<TrainingProgram>? TrainingPrograms { get; set; }
        public ICollection<ClassTrainerUnit>? TrainerUnits { get; set; }
    }
}
