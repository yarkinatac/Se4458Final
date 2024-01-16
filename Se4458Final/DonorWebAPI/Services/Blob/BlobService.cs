using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace DonorWebAPI.Services.Blob
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService()
        {
            var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=se4458finalyarkin;AccountKey=zzr8wZhj5dujxoUaC90DJpsqffKk7Wm6KFXmN2BRELe31JR53VbNhKXKdBFXAyxZdziYeuFqQGWh+AStjH+Fyg==;EndpointSuffix=core.windows.net");
            _containerClient = blobServiceClient.GetBlobContainerClient("se4458blobcontainer");
        }

        public async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            // Benzersiz bir dosya adı oluştur BlogStorage İmage kısmında gözükmesi için
            var guid = Guid.NewGuid();
            var fileName = $"{guid + Path.GetExtension(photo.FileName)}";
            var blobClient = _containerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/jpeg" 
                }
            };
            
            using (var stream = photo.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, httpHeaders: new BlobHttpHeaders { ContentType = "image/jpeg" });
            }

            return blobClient.Uri.ToString();
            
        }
    }
}
