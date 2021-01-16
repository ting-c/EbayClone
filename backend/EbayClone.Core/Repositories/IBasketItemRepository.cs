using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
	public interface IBasketItemRepository : IRepository<BasketItem>
	{
		Task<IEnumerable<BasketItem>> GetAllBasketItemsAsync(int userId);
		Task UpdateQuantityAsync(int basketItemId, int quantity);
	}
}