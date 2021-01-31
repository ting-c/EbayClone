using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
	public class BasketItemRepository : Repository<BasketItem>, IBasketItemRepository
	{
		public BasketItemRepository(EbayCloneDbContext context) : base(context)
		{ }

		public async Task<IEnumerable<BasketItem>> GetAllBasketItemsAsync(int userId)
		{
			return await EbayCloneDbContext.BasketItems
				.Where(b => b.UserId == userId)
				.Include(b => b.Item)
				.ThenInclude(i => i.Seller)
				.ThenInclude(i => i.ImageUrl)
				.ToListAsync();
		}

		public async Task UpdateQuantityAsync(int basketItemId, int quantity)
		{
			BasketItem basketItem = EbayCloneDbContext.BasketItems.Find(basketItemId);

			basketItem.Quantity = quantity;

			EbayCloneDbContext.BasketItems.Attach(basketItem);

			EbayCloneDbContext.Entry(basketItem).Property(x => x.Quantity).IsModified = true;

			await EbayCloneDbContext.SaveChangesAsync();
		}

		private EbayCloneDbContext EbayCloneDbContext
		{
			get { return Context as EbayCloneDbContext; }
		}
	}
}