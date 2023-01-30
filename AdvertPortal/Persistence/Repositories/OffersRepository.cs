using AdvertPortal.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Offer> GetAllForUser(string userId)
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

        public Offer GetOffer(int id)
        {
            return _context.Offers.Single(x => x.Id == id);
        }
    }
}
