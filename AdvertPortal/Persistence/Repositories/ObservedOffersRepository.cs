using AdvertPortal.Core;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.Repositories;

namespace AdvertPortal.Persistence.Repositories
{
    public class ObservedOffersRepository : IObservedOffersRepository
    {
        private IApplicationDbContext _context;
        public ObservedOffersRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void AddObservedStatus(int offerId, string userId)
        {
            var observedOffer = new ObservedOffer { OfferId = offerId, UserId = userId };
            _context.ObservedOffers.Add(observedOffer);
        }

        public void RemoveObservedStatus(int offerId, string userId)
        {
            var observedOfferToDelete = _context.ObservedOffers
                                .Single(x => x.OfferId == offerId && x.UserId == userId);
            _context.ObservedOffers.Remove(observedOfferToDelete);
        }

        public bool IsObserved(int offerId, string userId)
        {
            return _context.ObservedOffers.Any(x => x.OfferId == offerId && x.UserId == userId);
        }

        public void DeleteAllObserved(int id)
        {
            var observedOffersToDelete = _context.ObservedOffers.Where(x => x.OfferId == id).ToList();
            if(observedOffersToDelete.Any() )
            {
                foreach (var observedOffer in observedOffersToDelete)
                    {
                        _context.ObservedOffers.Remove(observedOffer);
                    }
            }
        }
    }
}
