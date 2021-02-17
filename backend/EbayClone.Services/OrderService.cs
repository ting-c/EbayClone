using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;

namespace EbayClone.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Order>> GetAllByUserId(int userId)
		{
			var orders = await _unitOfWork.Orders.GetAllByUserIdAsync(userId);
			return orders;
		}

		public async Task<Order> GetOrderById(int id)
		{
			var order = await _unitOfWork.Orders.GetByIdAsync(id);
			return order;
		}

		public async Task<Order> CreateOrder(Order newOrder)
		{
			await _unitOfWork.Orders.AddAsync(newOrder);
			return newOrder;
		}
	}
}