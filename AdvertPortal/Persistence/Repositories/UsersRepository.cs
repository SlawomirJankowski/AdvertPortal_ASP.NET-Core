using AdvertPortal.Core;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.Repositories;

namespace AdvertPortal.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private IApplicationDbContext _context;
        public UsersRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
