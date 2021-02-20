using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(EbayCloneDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Item>> GetAllWithUserAsync()
        {
            return await EbayCloneDbContext.Items
                .Include(i => i.Seller)
                .Include(i => i.ImageUrl)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsByTitleAsync(string title)
        {
            return await EbayCloneDbContext.Items
                .Where(i => i.Title.Contains(title))
                .Include(i => i.Seller)
                .Include(i => i.ImageUrl)
                .ToListAsync();
        }

        public async Task<Item> GetWithUserByIdAsync(int itemId)
        {
            return await EbayCloneDbContext.Items
				.Include(i => i.Seller)
                .Include(i => i.ImageUrl)
                .SingleOrDefaultAsync(i => i.Id == itemId);
        }

		public async Task<IEnumerable<Item>> GetAllWithUserbyUserIdAsync(int userId)
		{
			return await EbayCloneDbContext.Items
                .Include(i => i.Seller)
                .Include(i => i.ImageUrl)
                .Where(i => i.SellerId == userId)
                .ToListAsync();
		}

		public async Task UpdateQuantityAsync(int itemId, int quantity)
		{
			Item item = EbayCloneDbContext.Items.Find(itemId);

			item.Quantity = quantity;

			EbayCloneDbContext.Items.Attach(item);

			EbayCloneDbContext.Entry(item).Property(x => x.Quantity).IsModified = true;

			await EbayCloneDbContext.SaveChangesAsync();
		}

        private EbayCloneDbContext EbayCloneDbContext
        {
            get { return Context as EbayCloneDbContext; }
        }
    }
}