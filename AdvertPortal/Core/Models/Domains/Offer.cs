using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdvertPortal.Core.Models.Domains
{
    public class Offer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole TYTUŁ jest wymagane")]
        [MaxLength(100)]
        [Display(Name = "Tytuł:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Data publikacji:")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Pole DANE SZCZEGÓŁOWE jest wymagane")]
        [MaxLength(300)]
        [Display(Name = "Dane Szczegółowe:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole CENA jest wymagane")]
        [Display(Name = "Cena:")]
        [RegularExpression(@"^\$?\d+(\,(\d{2}))?$")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Pole KATEGORIA jest wymagane")]
        [Display(Name = "Kategoria:")]
        public int CategoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string? ImagesPath { get; set; }

        public string? ThumbnailName { get; set; }

        public string? ImagesNames { get; set; }


        public Category? Category { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
