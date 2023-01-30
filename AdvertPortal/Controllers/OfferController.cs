using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.ViewModels;
using AdvertPortal.Persistence;
using AdvertPortal.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdvertPortal.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private OffersRepository _offersRepository;

        public OfferController(ApplicationDbContext context)
        {
            _offersRepository = new OffersRepository(context);
        }

        [AllowAnonymous]
        //GET All Offers
        public IActionResult Offers(string userId = "")
        {
            var vm = new OffersViewModel
            {
                Offers = string.IsNullOrEmpty(userId) ? _offersRepository.GetAll() : _offersRepository.GetAllForUser(userId),
                Categories = _offersRepository.GetCategories(),
                FilterOffers = new FilterOffers()
            };

            return View(vm);
        }

        //GET Edit, Add
        public IActionResult OfferAddEdit(int id = 0)
        {
            var vm = new OfferViewModel
            {
                Categories = new List<Category> {
                    new Category { Id = 1, Name = "Common" },
                    new Category { Id = 2, Name = "Health" }
                },

                Heading = id == 0 ? "New offer" : "Edit Offer ",

                Offer = new Offer
                {
                    Id = 324345,
                    Price = 123.45d,
                    Description = "Opis danego produktu"
                }

            };

            return View(vm);
        }

        //POST Edit, Add
        [HttpPost]
        public IActionResult OfferAddEdit()
        {
            return View();
        }


        //GET View offer PUBLIC
        [AllowAnonymous]
        public IActionResult OfferDetails(int id, int categoryId)
        {
            var vm = new OfferDetailsViewModel
            {
                Category = _offersRepository.GetCategoryById(categoryId),
                Offer = _offersRepository.GetOffer(id),
            };            

            return View(vm);
        }



    }
}
