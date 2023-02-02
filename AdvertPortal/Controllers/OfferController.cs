﻿using AdvertPortal.Core.Models;
using AdvertPortal.Core.Models.Domains;
using AdvertPortal.Core.ViewModels;
using AdvertPortal.Persistence;
using AdvertPortal.Persistence.Extensions;
using AdvertPortal.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace AdvertPortal.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private OffersRepository _offersRepository;
        private UsersRepository _usersRepository;

        public OfferController(ApplicationDbContext context)
        {
            _offersRepository = new OffersRepository(context);
            _usersRepository = new UsersRepository(context);
        }

        [AllowAnonymous]
        //GET All Offers
        public IActionResult Offers(string userId)
        {
            var vm = new OffersViewModel
            {
                Offers = string.IsNullOrEmpty(userId) ? _offersRepository.GetAll() : _offersRepository.GetAllForUser(userId),
                Categories = _offersRepository.GetCategories(),
                FilterOffers = new FilterOffers(),
                UserName = !string.IsNullOrEmpty(userId) ? _usersRepository.GetUserById(userId) : null
            };

            return View(vm);
        }

        //GET View offer PUBLIC
        [AllowAnonymous]
        public IActionResult OfferDetails(int id, int categoryId, string userId)
        {
            var vm = new OfferDetailsViewModel
            {
                Category = _offersRepository.GetCategoryById(categoryId),
                Offer = _offersRepository.GetOffer(id),
                User = _usersRepository.GetUserById(userId),
                LoggedUserId = User.GetLoggedUserId(),
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

        



    }
}
