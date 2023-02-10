using AdvertPortal.Core.Repositories;
using AdvertPortal.Persistence.Repositories;

namespace AdvertPortal.Core
{
    public interface IUnitOfWork
    {
        IOffersRepository OffersRepository { get; }
        IUsersRepository UserRepository { get; }
        IObservedOffersRepository ObservedOffersRepository { get; }

        public void Complete();
    }
}
