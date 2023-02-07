using AdvertPortal.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
        
        public IEnumerable<Offer> GetFilteredOffers(int categoryId = 0, string? title = null, ICollection<string>? sortingOrderParams = null)
        {
            IQueryable<Offer> offers = _context.Offers.
                Include(x => x.Category);

            if (categoryId != 0)
                offers = offers.Where(x=> x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                offers = offers.Where(x => x.Title.Contains(title));

            if (sortingOrderParams != null && sortingOrderParams.Any())
            {
                if (sortingOrderParams.Contains("Dacie: rosnąco"))
                        offers = offers.OrderBy(x => x.Date);
                else if (sortingOrderParams.Contains("Dacie: malejąco"))
                        offers = offers.OrderByDescending(x => x.Date);
                else if (sortingOrderParams.Contains("Cenie: rosnąco"))
                        offers = offers.OrderBy(x => x.Price);
                else if (sortingOrderParams.Contains("Cenie: malejąco"))
                        offers = offers.OrderByDescending(x => x.Price);
            }
            return offers.ToList();
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

        public IEnumerable<Offer> GetAllObservedOffersByLoggedUser(string loggedUserId)
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
