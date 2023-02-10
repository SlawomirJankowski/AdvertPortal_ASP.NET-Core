using AdvertPortal.Core.Models.Domains;
using System.Diagnostics.CodeAnalysis;

namespace AdvertPortal.Core.ViewModels
{
    public class OfferDetailsViewModel
    {
        public Offer Offer { get; set; }
        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
        public string LoggedUserId { get; set; }
        public bool IsObserved { get; set; }

    }
}
