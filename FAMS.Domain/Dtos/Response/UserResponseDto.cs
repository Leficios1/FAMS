using FAMS.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class UserResponseDto
    {
        public string PermissionId { get; set; } = null!;
        public string? Phone { get; set; }
        public string? DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string RoleName { get; set; } = null!;
        public bool Status { get; set; }
       
    }
}
