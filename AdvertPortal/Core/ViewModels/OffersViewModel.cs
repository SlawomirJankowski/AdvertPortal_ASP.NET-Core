using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;

namespace AdvertPortal.Core.ViewModels
{
    public class OffersViewModel
    {
        public FilterOffers FilterOffers { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
