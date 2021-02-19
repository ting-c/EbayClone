using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Services
{
	public interface IOrderItemService
	{
		Task<OrderItem> GetOrderItemById(int id);
		Task<IEnumerable<OrderItem>> GetAllByOrderId(int orderId);
		Task<OrderItem> CreateOrderItem(OrderItem newOrderItem);
	}
}