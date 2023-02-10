using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Core.Services
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetUserById(string userId);
    }
}
