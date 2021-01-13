using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EbayClone.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IFilePathService _filePathService;
        private readonly IHttpClientFactory _clientFactory;
        
        public UploadController(IFilePathService filePathService, IItemService itemService, IHttpClientFactory clientFactory)
        {
            this._filePathService = filePathService;
            this._itemService = itemService;
			this._clientFactory = clientFactory;
        }

        [HttpPost("{itemId}")]
        public async Task<IActionResult> UploadImage(int itemId, List<IFormFile> files)
        {
            // Get userId 
			int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            // check if item exists
            var item = await _itemService.GetItemById(itemId);
            if (item == null)
                return NotFound("Item not found");

			// // check if userId matches item's sellerId
			// if (userId != item.SellerId)
			//     return Forbid("Unauthorized Request");

			var client = _clientFactory.CreateClient("imgbb");
			// Loop through files and upload images to imgbb
            foreach (var file in files){
				if (file.Length > 0)
				{
					using (var ms = new MemoryStream())
					{
						file.CopyTo(ms);
						var fileBytes = ms.ToArray();
						string base64 = Convert.ToBase64String(fileBytes);
                        // content with base64 string for the post request
						var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("image", base64)
                        });
                        var response = await client.PostAsync(client.BaseAddress.ToString(), content);

                        if ( !response.IsSuccessStatusCode)
                            throw new Exception($"Failed to upload image {file.Name}");

                        var imageUrl = await GetImageUrlFromResponse(response);
                        
                        // add url to file path repository
                        await CreateFilePath(itemId, userId, imageUrl);
					}
				} 
            }
            return Ok();
        }

        private async Task<dynamic> GetImageUrlFromResponse(HttpResponseMessage response)
        {
			var jsonString = await response.Content.ReadAsStringAsync();
			dynamic jsonObject = JObject.Parse(jsonString);
			var imageUrl = jsonObject.data.url;
            return imageUrl;
        }

        private async void CreateFilePath(int itemId, int userId, dynamic imageUrl)
        {
			var filePath = new FilePath()
			{
				UrlPath = imageUrl,
				ItemId = itemId,
				UserId = userId
			};
			await _filePathService.CreateFilePath(filePath);
        }
    }
}