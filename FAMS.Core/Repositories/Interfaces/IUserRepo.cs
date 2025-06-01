using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Interfaces.Repositories
{
    public interface IUserRepo
    {
        Task<User> GetUserById(int id);
        public Task<IEnumerable<User>> GetAllUser();
    }
}
