using Campus02DemoProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus02DemoProject.Controllers
{

    //Nuget -- Azure.Storage.Blobs
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly ICCBlobService _ccBlobService;

        public BlobController(ICCBlobService ccBlobService)
        {
            _ccBlobService = ccBlobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var result = await _ccBlobService.GetBlobByNameAsync(blobName);
            return Ok(result.Content.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> PostBlob(string fileName)
        {
            var result = await _ccBlobService.UploadFileBlobWithPathAndNameAsync(fileName);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBlobs")]
        public async Task<IActionResult> GetAllBlobs()
        {
            var result = await _ccBlobService.ListAllBlobsAsync();
            return Ok(result);
        }


    }
}
