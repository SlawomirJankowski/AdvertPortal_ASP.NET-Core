using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.Services;
using AdvertPortal.Core.ViewModels;
using AdvertPortal.Persistence.Extensions;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AdvertPortal.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {

        private IOfferService _offerService;
        private IUserService _userService;
        private IObservedOfferService _observedOfferService;
        private readonly IWebHostEnvironment _env;

        public OfferController(IOfferService offerService, IUserService userService, IObservedOfferService observedOfferService, IWebHostEnvironment env)
        {
            _offerService = offerService;
            _userService = userService;
            _observedOfferService = observedOfferService;
            _env = env;
        }

        [AllowAnonymous]
        //GET All Offers
        public IActionResult Offers(string userId, bool observedOffers)
        {
            IEnumerable<Offer> offers;
            if (observedOffers)
                offers = _offerService.GetAllObservedOffersByLoggedUser(userId);
            else
                offers = string.IsNullOrEmpty(userId) ? _offerService.GetAll() : _offerService.GetAllWhereUserIsOwner(userId);

            var vm = new OffersViewModel
            {   
                Offers = offers,
                Categories = _offerService.GetCategories(),
                FilterOffers = new FilterOffers(),
                UserName = !string.IsNullOrEmpty(userId) ? _userService.GetUserById(userId) : null,
            };

            return View(vm);
        }

        //POST filter Offers
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Offers(OffersViewModel offersViewModel)
        {
            var offers = _offerService.GetFilteredOffers(offersViewModel.FilterOffers.CategoryId,
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
                Category = _offerService.GetCategoryById(categoryId),
                Offer = _offerService.GetOffer(id),
                User = _userService.GetUserById(userId),
                LoggedUserId = loggedUserId,
                IsObserved = _observedOfferService.IsObserved(id, loggedUserId),
            };

            return View(vm);
        }

        //GET Edit, Add
        public IActionResult OfferAddEdit(int id = 0)
        {
            var userId = User.GetLoggedUserId();
            var offer = id == 0 ? new Offer { Id = 0, UserId = userId } : _offerService.GetOfferForEdit(id, userId);

            var vm = PrepareOfferViewModel(id, offer);

            return View(vm);
        }

        //POST Edit, Add
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult OfferAddEdit(Offer offer, ImagesUploader imagesUploader)
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

            //upload images
            if (imagesUploader.PostedThumbnail != null || imagesUploader.PostedImages != null)
            {   if(offer.Id == 0)
                    offer.ImagesPath = imagesUploader.ImagesDirectoryPath;
                
                string path = Path.Combine(this._env.WebRootPath, "Uploads", offer.ImagesPath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (imagesUploader.PostedThumbnail != null)
                    offer.ThumbnailName = imagesUploader.GetNameAndUploadImageToServer(path, imagesUploader.PostedThumbnail);
                if (imagesUploader.PostedImages != null)
                    offer.ImagesNames = imagesUploader.GetListOfImagesFileNamesAsStringAndUpload(path, imagesUploader.PostedImages);
            }
          
            if (offer.Id == 0)
                _offerService.Add(offer);
            else
                _offerService.Update(offer);

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
                Categories = _offerService.GetCategories(),
                Heading = id == 0 ? "Dodawanie nowego ogłoszenia" : $"Edycja ogłoszenia nr: {offer.Id}"
            };
            return vm;
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        {
            try
            {
                var userId = User.GetLoggedUserId();
                var wwwRootPath = _env.WebRootPath;

                _observedOfferService.DeleteAllObserved(id);
                _offerService.Delete(id, userId, wwwRootPath);
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
                _observedOfferService.AddObservedStatus(offerId, userId);
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
                _observedOfferService.RemoveObservedStatus(offerId, userId);
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
