using AdvertPortal.Core;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.Services;

namespace AdvertPortal.Persistence.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Offer> GetAll()
        {
            return _unitOfWork.OffersRepository.GetAll();
        }
  

        public IEnumerable<Offer> GetAllWhereUserIsOwner(string userId)
        {
            return _unitOfWork.OffersRepository.GetAllWhereUserIsOwner(userId);
        }

        public IEnumerable<Offer> GetFilteredOffers(int categoryId = 0, string? title = null, ICollection<string>? sortingOrderParams = null)
        {
            return _unitOfWork.OffersRepository.GetFilteredOffers(categoryId, title, sortingOrderParams);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.OffersRepository.GetCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _unitOfWork.OffersRepository.GetCategoryById(categoryId);
        }

        public Offer GetOffer(int id)
        {
            return _unitOfWork.OffersRepository.GetOffer(id);          
        }

        public Offer GetOfferForEdit(int id, string userId)
        {
            return _unitOfWork.OffersRepository.GetOfferForEdit(id, userId);
        }

        public void Add(Offer offer)
        {
            _unitOfWork.OffersRepository.Add(offer);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId, string wwwRootPath)
        {
            _unitOfWork.OffersRepository.Delete(id, userId, wwwRootPath);
            _unitOfWork.Complete();
        }

        public void Update(Offer offer)
        {
            _unitOfWork.OffersRepository.Update(offer);
            _unitOfWork.Complete();
        }

        public IEnumerable<Offer> GetAllObservedOffersByLoggedUser(string loggedUserId)
        {
            return _unitOfWork.OffersRepository.GetAllObservedOffersByLoggedUser(loggedUserId);
        }

    }
}
