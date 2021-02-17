using System.Collections.Generic;
using EbayClone.Core.Models;

namespace EbayClone.API.Resources
{
	public class CreateOrderResource
	{
		public IEnumerable<BasketItem> BasketItems { get; set; }
	}
}