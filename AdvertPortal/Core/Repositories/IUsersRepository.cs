using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Core.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<ApplicationUser> GetAll();

        ApplicationUser GetUserById(string userId);
       
    }
}
