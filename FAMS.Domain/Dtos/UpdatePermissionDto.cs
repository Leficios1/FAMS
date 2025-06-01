using FAMS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FAMS.Api.Dtos
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage ="Id is required!")]   
        public string? PermissionId { get; set; } 

        public string? RoleName { get; set; } 
        [Required(ErrorMessage = "Syllabus policy is required!"), EnumDataType(typeof(PermissionEnum), ErrorMessage ="The Syllabus Policy is not in the enum.")]
        public byte Syllabus { get; set; }
        [Required(ErrorMessage = "Training Program policy is required!"), EnumDataType(typeof(PermissionEnum), ErrorMessage = "The Training Program Policy is not in the enum.")]
        public byte TrainingProgram { get; set; }
        [Required(ErrorMessage = "Class policy is required!"), EnumDataType(typeof(PermissionEnum), ErrorMessage = "The Class Policy is not in the enum.")]
        public byte Class { get; set; }
        [Required(ErrorMessage = "Learning Material policy is required!"), EnumDataType(typeof(PermissionEnum), ErrorMessage = "The Learning Material Policy is not in the enum.")]
        public byte LearningMaterial { get; set; }
        [Required(ErrorMessage = "User Management policy is required!"), EnumDataType(typeof(PermissionEnum), ErrorMessage = "The User Management Policy is not in the enum.")]
        public byte UserManagement { get; set; }
    }
}
