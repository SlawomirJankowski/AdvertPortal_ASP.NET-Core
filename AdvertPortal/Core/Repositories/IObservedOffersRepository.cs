namespace AdvertPortal.Core.Repositories
{
    public interface IObservedOffersRepository
    {
        void AddObservedStatus(int offerId, string userId);
        void RemoveObservedStatus(int offerId, string userId);
        bool IsObserved(int offerId, string userId);
        void DeleteAllObserved(int id);
    }
}
