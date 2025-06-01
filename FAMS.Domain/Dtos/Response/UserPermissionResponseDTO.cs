using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class UserPermissionResponseDTO
    {
        public string PermissionId { get; set; } = null!;
        public byte TrainingProgram { get; set; }
        public byte UserManagement { get; set; }
        public byte LearningMaterial { get; set; }
        public byte Syllabus { get; set; }
        public byte Class { get; set; }
    }
}
