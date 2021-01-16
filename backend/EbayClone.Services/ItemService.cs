using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;

namespace EbayClone.Services
{
    public class ItemService : IItemService
    {
      private readonly IUnitOfWork _unitOfWork;
      public ItemService(IUnitOfWork unitOfWork)
      {
         this._unitOfWork = unitOfWork;
      }

      public async Task<Item> CreateItem(Item newItem)
      {
			await _unitOfWork.Items.AddAsync(newItem);
			await _unitOfWork.CommitAsync();
			return newItem;
      }

		public async Task DeleteItem(Item item)
		{
			_unitOfWork.Items.Remove(item);
         await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<Item>> GetAllWithUser()
		{
			return await _unitOfWork.Items.GetAllWithUserAsync();
		}

		public async Task<Item> GetItemById(int id)
		{
			return await _unitOfWork.Items.GetWithUserByIdAsync(id);
		}

		public async Task<IEnumerable<Item>> GetItemsByTitle(string title)
		{
			return await _unitOfWork.Items.GetItemsByTitleAsync(title);
		}

		public async Task<IEnumerable<Item>> GetItemsByUserId(int userId)
		{
			return await _unitOfWork.Items.GetAllWithUserbyUserIdAsync(userId);
		}

		public async Task UpdateItem(Item currentItem, Item modifiedItem)
		{
			// to avoid error as modifiedItem.Id = null
			modifiedItem.Id = currentItem.Id;

			_unitOfWork.Items.Update(currentItem, modifiedItem);

         await _unitOfWork.CommitAsync();
		}

		public async Task UpdateQuantity(int itemId, int quantity)
		{
			await _unitOfWork.Items.UpdateQuantityAsync(itemId, quantity);

			await _unitOfWork.CommitAsync();
		}
	}
}