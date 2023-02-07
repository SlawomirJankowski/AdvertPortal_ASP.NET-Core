using System.ComponentModel.DataAnnotations;

namespace AdvertPortal.Core.Models.Domains
{
    public class ObservedOffer
    {
        public int Id { get; set; }
        public int OfferId { get; set; }

        [Required]
        public string UserId { get; set; }



    }
}
