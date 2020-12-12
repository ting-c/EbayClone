using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public ItemsController(IItemService itemService, IMapper mapper)
        {
            this._itemService = itemService;
        }
        
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _itemService.GetAllWithUser();
            return Ok(items);
        }
			
    }
}