using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Services
{
    public interface IBasketItemService
    {
		Task<IEnumerable<BasketItem>> GetAllBasketItems(int userId);
		Task<BasketItem> GetBasketItemById(int basketItemId);
		Task<BasketItem> AddBasketItem(BasketItem newBasketItem);
		Task RemoveBasketItem(BasketItem basketItem);
		Task UpdateQuantity(int basketItemId, int quantity);
    }
}