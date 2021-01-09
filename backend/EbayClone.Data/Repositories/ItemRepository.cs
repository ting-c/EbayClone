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
                .SingleOrDefaultAsync(i => i.Id == itemId);
        }

		public async Task<IEnumerable<Item>> GetAllWithUserbyUserIdAsync(int userId)
		{
			return await EbayCloneDbContext.Items
                .Include(i => i.Seller)
                .Where(i => i.SellerId == userId)
                .ToListAsync();
		}

        private EbayCloneDbContext EbayCloneDbContext
        {
            get { return Context as EbayCloneDbContext; }
        }
    }
}