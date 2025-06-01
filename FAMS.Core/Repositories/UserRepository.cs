using FAMS.Core.Databases;
using FAMS.Core.Exceptions;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepo
    {
        private readonly FamsContext _context;

        public UserRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                .Include(user => user.UserPermission)
                .FirstOrDefaultAsync(user => user.Id == id) ?? throw new BusinessException("User is not found!");    
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            var result= await _context.Users
              .Include(user => user.UserPermission).ToListAsync();
            return result;
        }
    }
}
