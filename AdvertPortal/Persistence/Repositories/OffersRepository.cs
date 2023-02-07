using AdvertPortal.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdvertPortal.Persistence.Repositories
{
    public class OffersRepository
    {

        private ApplicationDbContext _context;  
        public OffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Offer> GetAll()
        {
            var offers = _context.Offers
                .Include(x => x.Category)
                .ToList();
            return offers;
        }

        public IEnumerable<Offer> GetAllWhereUserIsOwner(string userId)
        {
            var offers = _context.Offers
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .ToList();
            return offers;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Single(x => x.Id == categoryId);
        }

        //For OfferDetails View
        public Offer GetOffer(int id)
        {
            return _context.Offers.Single(x => x.Id == id);
        }

        //For Edit
        public Offer GetOfferForEdit(int id, string userId)
        {
            return _context.Offers.Single(x => x.Id == id && x.UserId == userId);
        }

        public void Add(Offer offer)
        {
            _context.Add(offer);
            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var offerToDelete = _context.Offers
                    .Single(x => x.Id == id && x.UserId == userId);
            _context.Offers.Remove(offerToDelete);
            _context.SaveChanges();
        }

        public void Update(Offer offer)
        {
            var offerToUpdate = _context.Offers
                .Single(x => x.Id == offer.Id && x.UserId == offer.UserId);

            offerToUpdate.Title = offer.Title; 
            offerToUpdate.Description = offer.Description;
            offerToUpdate.CategoryId = offer.CategoryId;
            offerToUpdate.Price = offer.Price;
            // add update for imagesCollection

            _context.SaveChanges();
        }

        public IEnumerable<Offer> GetAllObservedOffers(string loggedUserId)
        {
            var observedOffersByLoggedUser = from offers in _context.Offers
                                             join observedOffer in _context.ObservedOffers
                                             on offers.Id equals observedOffer.OfferId
                                             where observedOffer.UserId == loggedUserId
                                             select offers;

             return observedOffersByLoggedUser;
        }
    }
}
