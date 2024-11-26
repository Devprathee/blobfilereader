using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobDataRead.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlobDataRead.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly ILogger<BlobController> _logger;
        private readonly BlobSettings _mySettings;
        public BlobController(ILogger<BlobController> logger,IOptions<BlobSettings> mySettings)
        {
            _logger = logger;
            _mySettings = mySettings.Value;
        }

        [HttpGet("read-blob")]
        public async Task<IActionResult> ReadBlob()
        {
            try
            {
                string blobUri = _mySettings.blobUri!;
                string sasToken = _mySettings.sasToken!;
                var content = await ReadBlobAsync(blobUri, sasToken);
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading blob");
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<List<Property>> ReadBlobAsync(string blobUri, string sasToken)
        {
            string json = String.Empty;
            var blobUrlWithSas = $"{blobUri}?{sasToken}";
            BlobClient blobClient = new BlobClient(new Uri(blobUrlWithSas));
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (StreamReader reader = new StreamReader(download.Content))
            {
                json = await reader.ReadToEndAsync();
            }
           return JsonConvert.DeserializeObject<List<Property>>(json);
        }
    }
}
