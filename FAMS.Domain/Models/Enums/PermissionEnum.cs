 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Enums
{
    public enum PermissionEnum
    {
        FullAccess = 5,
        DeniedAccess = 1,
        ExceptImportSyllabus = 2,
        Create = 3,
        Modify = 4
    }
}
