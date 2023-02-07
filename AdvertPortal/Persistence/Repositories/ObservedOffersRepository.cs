using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Persistence.Repositories
{
    public class ObservedOffersRepository
    {
        private ApplicationDbContext _context;
        public ObservedOffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddObservedStatus(int offerId, string userId)
        {
            var observedOffer = new ObservedOffer { OfferId = offerId, UserId = userId };
            _context.Add(observedOffer);
            _context.SaveChanges();
        }

        public void RemoveObservedStatus(int offerId, string userId)
        {
            var observedOfferToDelete = _context.ObservedOffers
                                .Single(x => x.OfferId == offerId && x.UserId == userId);
            _context.ObservedOffers.Remove(observedOfferToDelete);
            _context.SaveChanges();
        }

        public bool IsObserved(int offerId, string userId)
        {
            return _context.ObservedOffers.Any(x => x.OfferId == offerId && x.UserId == userId);
        }


    }
}
