namespace EbayClone.API.Resources
{
    public class SaveItemResource
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Condition { get; set; }
		public bool IsAuction { get; set; }
		public int Quantity { get; set; }
      public int SellerId { get; set; }
    }
}