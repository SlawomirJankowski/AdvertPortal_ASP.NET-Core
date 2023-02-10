using AdvertPortal.Core.Models.Domains;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Mail;

namespace AdvertPortal.Persistence.Extensions
{
    public static class OfferExtensions
    {
        public static string[] GetImagesFilesList(this Offer offer)
        {
            return offer.ImagesNames.Split(new string[] { "||" }, StringSplitOptions.None);
        }

    }
}
