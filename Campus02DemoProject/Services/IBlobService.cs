using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Campus02DemoProject.Services
{
    //https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-download
    public interface ICCBlobService
    {
        public Task<BlobDownloadResult> GetBlobByNameAsync(string name);
        public Task<IEnumerable<string>> ListAllBlobsAsync();

        public Task<Azure.Response<BlobContentInfo>> UploadFileBlobWithPathAndNameAsync(string filePathAndName);

        

        public Task DeleteBlobByNameAsync(string blobName);


    }

    public class CCBlobService : ICCBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public CCBlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

       public async Task<BlobDownloadResult> GetBlobByNameAsync(string name)
        {
            var container = _blobServiceClient.GetBlobContainerClient("fhcampus");
            var blobClient = container.GetBlobClient(name);
            var result= await blobClient.DownloadContentAsync();

            return result;

        }

        public Task DeleteBlobByNameAsync(string blobName)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<string>> ListAllBlobsAsync()
        {
            var container = _blobServiceClient.GetBlobContainerClient("fhcampus");
            List<string> myBlobs = new List<string>();

            await foreach(var blob in container.GetBlobsAsync())
            {
                myBlobs.Add(blob.Name);
            }
            return myBlobs;

        }

        

        async Task<Response<BlobContentInfo>> ICCBlobService.UploadFileBlobWithPathAndNameAsync(string filePathAndName)
        {
            
            var container = _blobServiceClient.GetBlobContainerClient("fhcampus");
            var blobClient = container.GetBlobClient("B");

            return await blobClient.UploadAsync(filePathAndName, true);
        }
    }
}
