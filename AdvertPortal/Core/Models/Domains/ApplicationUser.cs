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
        //how to GET email, username HERE
        public ICollection<Offer> Offers { get; set; }
    }
}