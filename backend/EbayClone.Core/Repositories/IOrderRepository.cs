using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<IEnumerable<Order>> GetAllByUserIdAsync(int userId);
	}
}