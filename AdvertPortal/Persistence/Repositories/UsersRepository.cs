using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Persistence.Repositories
{
    public class UsersRepository
    {
        private ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
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
