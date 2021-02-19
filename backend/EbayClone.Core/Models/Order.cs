using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EbayClone.Core.Models
{
	public class Order
	{
		public Order(int userId)
		{
			UserId = userId;
			Items = new Collection<OrderItem>();
			Date = DateTime.Now;
		}
		public int Id { get; set; }
		public ICollection<OrderItem> Items { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
	}
}