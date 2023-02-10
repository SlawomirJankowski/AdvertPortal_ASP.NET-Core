using AdvertPortal.Core;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.Services;

namespace AdvertPortal.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _unitOfWork.UserRepository.GetUserById(userId);
        }

    }
}
