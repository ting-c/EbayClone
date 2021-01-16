using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;

namespace EbayClone.Services
{
    public class BasketItemService : IBasketItemService
    {
		private readonly IUnitOfWork _unitOfWork;
		public BasketItemService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<BasketItem>> GetAllBasketItems(int userId)
		{
			var basketItems = await _unitOfWork.BasketItems.GetAllBasketItemsAsync(userId);
			return basketItems;
		}

		public async Task<BasketItem> GetBasketItemById(int basketItemId)
		{
			return await _unitOfWork.BasketItems.GetByIdAsync(basketItemId);
		}

		public async Task<BasketItem> AddBasketItem(BasketItem newBasketItem)
		{
			await _unitOfWork.BasketItems.AddAsync(newBasketItem);
			await _unitOfWork.CommitAsync();
			return newBasketItem;
		}

		public async Task RemoveBasketItem(BasketItem basketItem)
		{
			_unitOfWork.BasketItems.Remove(basketItem);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateQuantity(int basketItemId, int quantity)
		{
			await _unitOfWork.BasketItems.UpdateQuantityAsync(basketItemId, quantity);
			await _unitOfWork.CommitAsync();
		}
    }
}