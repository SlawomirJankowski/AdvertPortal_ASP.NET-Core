using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;


namespace AdvertPortal.Core.Models
{
    public class ImagesUploader
    {
        private readonly string _imagesDirectoryPath = Guid.NewGuid().ToString();


        public string ImagesDirectoryPath { get; set; }

        [DisplayName("Dodaj miniaturkę:")]
        [AllowNull]
        public IFormFile? PostedThumbnail { get; set; }

        [DisplayName("Dodaj zdjęcia:")]
        [AllowNull]
        public List<IFormFile>? PostedImages { get; set; }


        public ImagesUploader()
        {
            ImagesDirectoryPath = _imagesDirectoryPath;
        }

        private void UploadFileToServer(string path, IFormFile formFile)
        {
            using (var fileStream = new FileStream(Path.Combine(path, Path.GetFileName(formFile.FileName)), FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
        }

        public string? GetListOfImagesFileNamesAsStringAndUpload(string path, List<IFormFile> formFiles)
        {
            try
            {
                var imagesFileNamesList = new List<string>();
                foreach (var formFile in formFiles)
                {
                    UploadFileToServer(path, formFile);

                    imagesFileNamesList.Add(Path.GetFileName(formFile.FileName));
                }
                return string.Join("||", imagesFileNamesList);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public string? GetNameAndUploadImageToServer(string path, IFormFile formFile)
        {
            try
            {
                UploadFileToServer(path, formFile);
                return Path.GetFileName(formFile.FileName);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}