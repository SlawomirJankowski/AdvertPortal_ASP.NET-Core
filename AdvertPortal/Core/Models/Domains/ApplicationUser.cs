using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace AdvertPortal.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Offers = new Collection<Offer>();
        }

        public ICollection<Offer> Offers { get; set; }
    }
}