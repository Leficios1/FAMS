using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FAMS.Domain.Models.Dtos.Request
{
    public class UserRequestDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Permission ID is required")]
        public string PermissionId { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
       
        public string? Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public byte? Status { get; set; }
    }
}

