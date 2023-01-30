using System.Collections.ObjectModel;

namespace AdvertPortal.Core.Models
{
    public class FilterOffers
    {
        private ICollection<string> _sortingOrderParams = new Collection<string>()
            {
                { "Dacie: rosnąco" },
                { "Dacie: malejąco" },
                { "Cenie: rosnąco" },
                { "Cenie: malejąco" }
            };

        public FilterOffers()
        {
            SortingOrderParams = _sortingOrderParams;
        }
                
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public ICollection<string> SortingOrderParams { get; set; }

    }
}
