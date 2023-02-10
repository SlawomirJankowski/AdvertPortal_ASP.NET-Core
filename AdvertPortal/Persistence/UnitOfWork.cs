using AdvertPortal.Core;
using AdvertPortal.Core.Repositories;
using AdvertPortal.Persistence.Repositories;

namespace AdvertPortal.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            OffersRepository = new OffersRepository(context);
            UserRepository = new UsersRepository(context);
            ObservedOffersRepository= new ObservedOffersRepository(context);
        }

        public IOffersRepository OffersRepository { get; set; }
        public IUsersRepository UserRepository { get; set; }
        public IObservedOffersRepository ObservedOffersRepository { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
