using AdvertPortal.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace AdvertPortal.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Offer> Offers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ObservedOffer> ObservedOffers { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        int SaveChanges();
    }
}
