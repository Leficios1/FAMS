using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewListUserDTO
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        
        public DateTime? DOB {  get; set; }

        public string? Gender { get; set; }

        public int PermissionId { get; set; }

        public string? Rolename { get; set; }
    }
}
