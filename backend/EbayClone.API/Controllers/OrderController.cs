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
		private readonly IOrderItemService _orderItemService;
		private readonly IOrderService _orderService;

		public OrderController(
			IItemService itemService, 
			IOrderItemService orderItemService, 
			IOrderService orderService
		)
		{
			this._itemService = itemService;
			this._orderItemService = orderItemService;
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
			var order = new Order(userId);
			var newOrder = await _orderService.CreateOrder(order);
			
			var basketItems = createOrderResource.BasketItems;
			foreach (BasketItem basketItem in basketItems)
			{
				var item = await _itemService.GetItemById(basketItem.ItemId);
				if (item == null) {
					return NotFound();
				}

				await _orderItemService.CreateOrderItem(new OrderItem(order.Id, item.Id, basketItem.Quantity, item.Price));
				
				// update item quantity
				var updatedQuantity = item.Quantity - basketItem.Quantity;
				await _itemService.UpdateQuantity(basketItem.ItemId, updatedQuantity);
			}

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
			if (order.UserId == userId)
				return true;

			return false;
		}
	}
}