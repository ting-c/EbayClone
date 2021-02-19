using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;

namespace EbayClone.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderItemService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<OrderItem>> GetAllByOrderId(int orderId)
		{
			var orderItems = await _unitOfWork.OrderItems.GetAllByOrderIdAsync(orderId);
			return orderItems;
		}

		public async Task<OrderItem> GetOrderItemById(int id)
		{
			var orderItem = await _unitOfWork.OrderItems.GetByIdAsync(id);
			return orderItem;
		}

		public async Task<OrderItem> CreateOrderItem(OrderItem newOrderItem)
		{
			await _unitOfWork.OrderItems.AddAsync(newOrderItem);
			await _unitOfWork.CommitAsync();
			return newOrderItem;
		}
	}
}