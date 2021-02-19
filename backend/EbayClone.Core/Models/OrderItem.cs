using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbayClone.Core.Models
{
	public class OrderItem
	{
		public OrderItem(int orderId, int itemId, int quantity, decimal price)
		{
			OrderId = orderId;
			ItemId = itemId;
			Quantity = quantity;
			Price = price;
		}

		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int ItemId { get; set; }
		public Item Item { get; set; }

		[Range(1, 50)]
		public int Quantity { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; }
	}
}