using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
	public interface IOrderItemRepository : IRepository<OrderItem>
	{
		Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId);
		Task<OrderItem> GetByIdWithSellerAndImageUrlAsync(int id);
	}
}