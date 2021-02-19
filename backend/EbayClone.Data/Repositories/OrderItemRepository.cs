using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
	public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(EbayCloneDbContext context) : base(context)
		{ }

		public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId)
		{
			return await EbayCloneDbContext.OrderItems
				.Where(oi => oi.OrderId == orderId)
				.Include(oi => oi.Item)
				.ThenInclude(i => i.Seller)
				
				.Include(oi => oi.Item)
				.ThenInclude(i => i.ImageUrl)
				
				.ToListAsync();
		}

		public async Task<OrderItem> GetByIdWithSellerAndImageUrlAsync(int id)
		{
			return await EbayCloneDbContext.OrderItems
				.Include(oi => oi.Item)
				.ThenInclude(i => i.Seller)

				.Include(oi => oi.Item)
				.ThenInclude(i => i.ImageUrl)
				.FirstOrDefaultAsync(oi => oi.Id == id);
		}

		private EbayCloneDbContext EbayCloneDbContext
		{
			get { return Context as EbayCloneDbContext; }
		}
	}
}