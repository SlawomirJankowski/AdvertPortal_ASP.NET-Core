using AdvertPortal.Core;
using AdvertPortal.Core.Services;

namespace AdvertPortal.Persistence.Services
{
    public class ObservedOfferService : IObservedOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObservedOfferService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddObservedStatus(int offerId, string userId)
        {
            _unitOfWork.ObservedOffersRepository.AddObservedStatus(offerId, userId);
            _unitOfWork.Complete();
        }

        public void RemoveObservedStatus(int offerId, string userId)
        {
            _unitOfWork.ObservedOffersRepository.RemoveObservedStatus(offerId, userId);
            _unitOfWork.Complete();
        }

        public bool IsObserved(int offerId, string userId)
        {
            return _unitOfWork.ObservedOffersRepository.IsObserved(offerId, userId);  
        }

        public void DeleteAllObserved(int id)
        {
            _unitOfWork.ObservedOffersRepository.DeleteAllObserved(id);
            _unitOfWork.Complete();
        }

    }
}
