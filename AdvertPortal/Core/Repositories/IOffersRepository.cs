using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Core.Repositories
{
    public interface IOffersRepository
    {
        IEnumerable<Offer> GetAll();
        IEnumerable<Offer> GetAllWhereUserIsOwner(string userId);
        IEnumerable<Offer> GetFilteredOffers(int categoryId = 0, string? title = null, ICollection<string>? sortingOrderParams = null);
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        Offer GetOffer(int id);
        Offer GetOfferForEdit(int id, string userId);
        void Add(Offer offer);
        void Delete(int id, string userId, string wwwRootPath);
        void Update(Offer offer);
        IEnumerable<Offer> GetAllObservedOffersByLoggedUser(string loggedUserId);
    }
}
