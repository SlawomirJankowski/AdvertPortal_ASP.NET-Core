using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.ViewModels;
using AdvertPortal.Persistence;
using AdvertPortal.Persistence.Extensions;
using AdvertPortal.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ganss.Xss;
using System.Net.Mail;
using System.Net;

namespace AdvertPortal.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private OffersRepository _offersRepository;
        private UsersRepository _usersRepository;
        private ObservedOffersRepository _observedOffersRepository;

        public OfferController(ApplicationDbContext context)
        {
            _offersRepository = new OffersRepository(context);
            _usersRepository = new UsersRepository(context);
            _observedOffersRepository = new ObservedOffersRepository(context);
        }

        [AllowAnonymous]
        //GET All Offers
        public IActionResult Offers(string userId, bool observedOffers)
        {
            IEnumerable<Offer> offers;
            if (observedOffers)
                offers = _offersRepository.GetAllObservedOffersByLoggedUser(userId);
            else
                offers = string.IsNullOrEmpty(userId) ? _offersRepository.GetAll() : _offersRepository.GetAllWhereUserIsOwner(userId);

            var vm = new OffersViewModel
            {   
                Offers = offers,
                Categories = _offersRepository.GetCategories(),
                FilterOffers = new FilterOffers(),
                UserName = !string.IsNullOrEmpty(userId) ? _usersRepository.GetUserById(userId) : null
            };

            return View(vm);
        }

        //POST filter Offers
        [HttpPost]
        public IActionResult Offers(OffersViewModel offersViewModel)
        {
            var offers = _offersRepository.GetFilteredOffers(offersViewModel.FilterOffers.CategoryId,
                offersViewModel.FilterOffers.Title,
                offersViewModel.FilterOffers.SortingOrderParams);

            return PartialView("_OffersTable", offers);
        }

        //GET View offer PUBLIC
        [AllowAnonymous]
        public IActionResult OfferDetails(int id, int categoryId, string userId)
        {
            var loggedUserId = User.GetLoggedUserId();
            var vm = new OfferDetailsViewModel
            {
                Category = _offersRepository.GetCategoryById(categoryId),
                Offer = _offersRepository.GetOffer(id),
                User = _usersRepository.GetUserById(userId),
                LoggedUserId = loggedUserId,
                IsObserved = _observedOffersRepository.IsObserved(id, loggedUserId)
            };

            return View(vm);
        }

        //GET Edit, Add
        public IActionResult OfferAddEdit(int id = 0)
        {
            var userId = User.GetLoggedUserId();
            var offer = id == 0 ? new Offer { Id = 0, UserId = userId } : _offersRepository.GetOfferForEdit(id, userId);

            var vm = PrepareOfferViewModel(id, offer);

            return View(vm);
        }

        //POST Edit, Add
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult OfferAddEdit(Offer offer)
        {
            var userId = User.GetLoggedUserId();
            offer.UserId = userId;

            if (offer.Id == 0)
                offer.Date = DateTime.Now;

            //to prevent XSS attacks
            offer.Description = HtmlSanitizer(WebUtility.HtmlDecode(offer.Description));

            if (!ModelState.IsValid)
            {
                var vm = PrepareOfferViewModel(offer.Id, offer);
                return View("OfferAddEdit", vm);
            }

            if (offer.Id == 0)
                _offersRepository.Add(offer);
            else
                _offersRepository.Update(offer);

            return RedirectToAction("Offers");
        }

        private string HtmlSanitizer(string dirtyHtml)
        {
            var sanitizer = new HtmlSanitizer();
            var cleanHtml = sanitizer.Sanitize(dirtyHtml);
            return cleanHtml;
        }

        public OfferViewModel PrepareOfferViewModel(int id, Offer offer)
        {
            var vm = new OfferViewModel
            {
                Offer = offer,
                Categories = _offersRepository.GetCategories(),
                Heading = id == 0 ? "Dodawanie nowego ogłoszenia" : $"Edycja ogłoszenia nr: {offer.Id}"
            };
            return vm;
        }

        [HttpPost]
        public IActionResult Delete(int id) //Offer offer
        {
            try
            {
                var userId = User.GetLoggedUserId();
                _offersRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new {redirectToUrl = Url.Action("Offers", "Offer"),
                        message = $"Pomyślnie usunięto ofertę nr {id}"});
        }

        [HttpPost]
        public IActionResult AddObservedOffer(int offerId)
        {
            try
            {
                var userId = User.GetLoggedUserId();
                _observedOffersRepository.AddObservedStatus(offerId, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { message = ex.Message });
            }

            return Json(new
            {
                message = "Dodano do OBSERWOWANYCH"
            });
        }

        [HttpPost]
        public IActionResult RemoveObservedOffer(int offerId)
        {
            try
            {
                var userId = User.GetLoggedUserId();
                _observedOffersRepository.RemoveObservedStatus(offerId, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { message = ex.Message });
            }

            return Json(new
            {
                message = "Usunięto z OBSERWOWANYCH"
            });
        }

    }
}
