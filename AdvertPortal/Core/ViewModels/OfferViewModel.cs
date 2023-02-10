using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;


namespace AdvertPortal.Core.ViewModels
{
    public class OfferViewModel
    {
        public Offer Offer { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string Heading { get; set; }
        public ImagesUploader ImagesUploader { get; set; }
    }
}
