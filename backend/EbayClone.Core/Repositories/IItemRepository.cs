using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
         Task<IEnumerable<Item>> GetAllWithUserAsync();
         Task<IEnumerable<Item>> GetItemsByTitleAsync(string title);
         Task<Item> GetWithUserByIdAsync(int itemId);
         Task<IEnumerable<Item>> GetAllWithUserbyUserIdAsync(int userId);
    }
}