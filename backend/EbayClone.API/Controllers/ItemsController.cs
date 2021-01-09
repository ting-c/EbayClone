using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.API.Validators;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;

namespace EbayClone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper)
        {
            this._mapper = mapper;
            this._itemService = itemService;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllWithUser();
            if (items == null)
                return NotFound();

            var itemResources = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemResource>>(items);

            return Ok(itemResources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _itemService.GetItemById(id);
			if (item == null)
				return NotFound();

            var itemResource = _mapper.Map<Item, ItemResource>(item);

            return Ok(itemResource);
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> GetItemsByTitle(string title)
        {
            var items = await _itemService.GetItemsByTitle(title);
			if (items == null)
				return NotFound();

            var itemResource = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemResource>>(items);

            return Ok(itemResource);
        }

		[Authorize]
        [HttpPost("")]
        public async Task<IActionResult> CreateItem([FromBody] SaveItemResource saveItemResource)
        {
            var validator = new SaveItemResourceValidator();
            ValidationResult results = validator.Validate(saveItemResource);

            if (!results.IsValid)
                return BadRequest(results.Errors);

            Item itemToCreate = _mapper.Map<SaveItemResource, Item>(saveItemResource);

            var newItem = await _itemService.CreateItem(itemToCreate);

			if (newItem == null)
				return NotFound();

            var item = await _itemService.GetItemById(newItem.Id);

            // map item to itemResource before returning in an OkResult object
            ItemResource itemResource = _mapper.Map<Item, ItemResource>(item);

            return Ok(itemResource);
        }

		[Authorize]
        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(int userId, int itemId, [FromBody] SaveItemResource saveItemResource)
        {
			bool IsValid = await CheckIfUserIsItemSeller(userId, itemId);
			if (!IsValid)
				return Unauthorized();

            var validator = new SaveItemResourceValidator();
            ValidationResult results = await validator.ValidateAsync(saveItemResource);

            if (!results.IsValid)
                return BadRequest(results.Errors);

            var currentItem = await _itemService.GetItemById(itemId);

            if (currentItem == null)
                return NotFound();

            Item modifiedItem = _mapper.Map<SaveItemResource, Item>(saveItemResource);

            await _itemService.UpdateItem(currentItem, modifiedItem);

            Item updatedItem = await _itemService.GetItemById(itemId);

            ItemResource updatedItemResource = _mapper.Map<Item, ItemResource>(updatedItem);

            return Ok(updatedItemResource);
        }

		[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int userId, int itemId)
        {
            bool IsValid = await CheckIfUserIsItemSeller(userId, itemId);
            if (!IsValid)
                return Unauthorized();

            var item = await _itemService.GetItemById(itemId);
            if (item == null)
                return NotFound();

            await _itemService.DeleteItem(item);

            return NoContent();
        }

        private async Task<bool> CheckIfUserIsItemSeller(int userId, int itemId)
        {
            var item = await _itemService.GetItemById(itemId);
            if (item.SellerId == userId)
                return true;
            
            return false;
        }
    }
}