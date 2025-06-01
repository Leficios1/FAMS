using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Services
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(int length);
    }
}
