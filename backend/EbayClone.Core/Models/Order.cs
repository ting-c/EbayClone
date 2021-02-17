using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EbayClone.Core.Models
{
	public class Order
	{
		public Order()
		{
			Items = new Collection<Item>();
			Date = new DateTime();
		}
		public int Id { get; set; }
		public ICollection<Item> Items { get; set; }
		public DateTime Date { get; set; }
		public int userId { get; set; }
	}
}