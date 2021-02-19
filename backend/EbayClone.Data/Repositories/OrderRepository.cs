using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(EbayCloneDbContext context) : base(context)
		{ }

		public async Task<IEnumerable<Order>> GetAllByUserIdAsync(int userId)
		{
			return await EbayCloneDbContext.Orders
				.Where(o => o.UserId == userId)
				.ToListAsync();
		}
		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			return await EbayCloneDbContext.Orders
				.Include(o => o.Items)
				.ThenInclude(oi => oi.Item)
				.ThenInclude(i => i.ImageUrl)

				.Include(o => o.Items)
				.ThenInclude(oi => oi.Item)
				.ThenInclude(i => i.Seller)

				.SingleOrDefaultAsync(o => o.Id == orderId);
		}

		private EbayCloneDbContext EbayCloneDbContext
		{
			get { return Context as EbayCloneDbContext; }
		}
	}
}