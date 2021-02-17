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
				.Where(o => o.userId == userId)
				.Include(o => o.Items)
				.ThenInclude(i => i.Seller)
				.ThenInclude(i => i.ImageUrl)
				.ToListAsync();
		}

		private EbayCloneDbContext EbayCloneDbContext
		{
			get { return Context as EbayCloneDbContext; }
		}
	}
}