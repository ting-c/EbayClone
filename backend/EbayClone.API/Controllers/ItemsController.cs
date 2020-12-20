using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.API.Validators;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<ItemResource>>> GetAllItems()
        {
            var items = await _itemService.GetAllWithUser();
            var itemResources = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemResource>>(items);

            return Ok(itemResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemResource>> GetItemById(int id)
        {
            var item = await _itemService.GetItemById(id);
            var itemResource = _mapper.Map<Item, ItemResource>(item);

            return Ok(itemResource);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateItem([FromBody] SaveItemResource saveItemResource)
        {
            var validator = new SaveItemResourceValidator();
            ValidationResult results = await validator.ValidateAsync(saveItemResource);

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

        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(int id, [FromBody] SaveItemResource saveItemResource)
        {
            var validator = new SaveItemResourceValidator();
            ValidationResult results = await validator.ValidateAsync(saveItemResource);

            if (!results.IsValid)
                return BadRequest(results.Errors);

            var itemToBeUpdated = await _itemService.GetItemById(id);

            if (itemToBeUpdated != null)
                return NotFound();

            Item item = _mapper.Map<SaveItemResource, Item>(saveItemResource);

            await _itemService.UpdateItem(itemToBeUpdated, item);

            Item updatedItem = await _itemService.GetItemById(id);

            ItemResource updatedItemResource = _mapper.Map<Item, ItemResource>(updatedItem);

            return Ok(updatedItemResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _itemService.GetItemById(id);

            if (item == null)
                return NotFound();

            await _itemService.DeleteItem(item);

            return NoContent();
        }
    }
}