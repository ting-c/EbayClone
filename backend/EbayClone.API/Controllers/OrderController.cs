using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IItemService _itemService;
		private readonly IBasketItemService _basketItemService;
		private readonly IOrderService _orderService;

		public OrderController(
			IItemService itemService, 
			IBasketItemService basketItemService, 
			IOrderService orderService
		)
		{
			this._itemService = itemService;
			this._basketItemService = basketItemService;
			this._orderService = orderService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			var isAuthorized = await CheckIfUserIsAuthorized(id);
			if (!isAuthorized)
				return Unauthorized();
			var order = await _orderService.GetOrderById(id);
			
			return Ok(order);
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetAllByUserId(int userId)
		{
			var orders = await _orderService.GetAllByUserId(userId);
			if (orders == null)
				return NotFound();
			
			return Ok(orders);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderResource createOrderResource)
		{
			var userId = getUserId();
			var order = new Order();
			order.userId = userId;
			var basketItems = createOrderResource.BasketItems;
			foreach (BasketItem basketItem in basketItems)
			{
				var item = await _itemService.GetItemById(basketItem.ItemId);
				if (item == null) {
					return NotFound();
				}
				order.Items.Append(item);
				
				// update item quantity
				var quantity = item.Quantity;
				var updatedQuantity = quantity - basketItem.Quantity;
				await _itemService.UpdateQuantity(basketItem.ItemId, updatedQuantity);
			}

			var newOrder = await _orderService.CreateOrder(order);

			return Ok(newOrder);
		}

		private int getUserId()
		{
			// Get userId 
			int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			return userId;
		}

		private async Task<bool> CheckIfUserIsAuthorized(int orderId)
		{
			var userId = getUserId();
			var order = await _orderService.GetOrderById(orderId);
			if (order.userId == userId)
				return true;

			return false;
		}
	}
}