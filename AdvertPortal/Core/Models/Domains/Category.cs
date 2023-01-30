using System.Collections.ObjectModel;

namespace AdvertPortal.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Offers = new Collection<Offer>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
