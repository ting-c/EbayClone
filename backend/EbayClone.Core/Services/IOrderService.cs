using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Services
{
	public interface IOrderService
	{
		Task<IEnumerable<Order>> GetAllByUserId(int userId);
		Task<Order> GetOrderById(int id);
		Task<Order> CreateOrder(Order newOrder);
	}
}