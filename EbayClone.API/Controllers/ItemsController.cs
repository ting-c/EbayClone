using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
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
			
    }
}