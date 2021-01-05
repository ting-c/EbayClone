using System;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly IFilePathService _filePathService;
        
        public UploadController(Cloudinary cloudinary, IFilePathService filePathService)
        {
            this._cloudinary = cloudinary;
            this._filePathService = filePathService;
        }

        [HttpPost("")]
        public async Task<IActionResult> UploadImage(int userId, int itemId, ImageUploadParams parameters)
        {
            var result = _cloudinary.Upload(parameters);
            int statusCode = (int)result.StatusCode;

            // image upload is successful
            if (statusCode >= 200 && statusCode <= 299)
            {
                var filePath = new FilePath()
                {
                    UrlPath = result.Url.ToString(),
                    ItemId = itemId,
                    UserId = userId                    
                };
                try {
                    await _filePathService.CreateFilePath(filePath);
                } catch (Exception e) {
                    throw new Exception(e.Message);
                }

                return Ok();
            }

            return Problem(result.Error.Message);
        }
    }
}