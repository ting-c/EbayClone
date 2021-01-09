using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllWithUser();
        Task<Item> GetItemById(int id);
        Task<IEnumerable<Item>> GetItemsByTitle(string title);
        Task<IEnumerable<Item>> GetItemsByUserId(int userId);
        Task<Item> CreateItem(Item newItem);
        Task UpdateItem(Item itemToBeUpdated, Item item);
        Task DeleteItem(Item item);
    }
}