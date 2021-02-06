using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class BasketItemController : ControllerBase
	{
		private readonly IBasketItemService _basketItemService;

		public BasketItemController(IBasketItemService basketItemService)
		{
			this._basketItemService = basketItemService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllBasketItems()
		{
			int userId = GetUserId();

			var basketItems = await _basketItemService.GetAllBasketItems(userId);
			if (basketItems == null)
				return NotFound(); 

			return Ok(basketItems);
		}

		[HttpPost("{itemId}/{quantity}")]
		public async Task<IActionResult> AddBasketItem(int itemId, int quantity=1)
		{
			if (quantity < 1 || quantity > 50)
				return BadRequest("Invalid quantity");

			int userId = GetUserId();
			var newBasketItem = new BasketItem(itemId, userId, quantity);

			var basketItem = await _basketItemService.AddBasketItem(newBasketItem);
			if (basketItem == null)
				return NotFound();

			return Ok(basketItem);
		}

		[HttpDelete("{basketItemId}")]
		public async Task<IActionResult> RemoveBasketItem(int basketItemId)
		{
			var basketItem = await _basketItemService.GetBasketItemById(basketItemId);
			if (basketItem == null)
				return NotFound();

			int userId = GetUserId();
			bool isAuthorized = await IsAuthorized(userId, basketItemId);
			if (!isAuthorized)
				return Unauthorized();

			await _basketItemService.RemoveBasketItem(basketItem);
			return Ok();
		}

		[HttpPut("{basketItemId}/{quantity}")]
		public async Task<IActionResult> UpdateQuantity(int basketItemId, int quantity)
		{
			var basketItem = await _basketItemService.GetBasketItemById(basketItemId);
			if (basketItem == null)
				return NotFound();

			int userId = GetUserId();
			bool isAuthorized = await IsAuthorized(userId, basketItemId);
			if (!isAuthorized)
				return Unauthorized();

			await _basketItemService.UpdateQuantity(basketItemId, quantity);
			return Ok();
		}

		private int GetUserId()
		{	
			// Get userId from Claims
			var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			return userId;
		}

		private async Task<bool> IsAuthorized(int userId, int basketItemId)
		{
			var basketItem = await _basketItemService.GetBasketItemById(basketItemId);

			bool isAuthorized = basketItem.UserId == userId;
			return isAuthorized;
		}
	}
}