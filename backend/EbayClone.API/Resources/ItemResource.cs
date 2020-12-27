namespace EbayClone.API.Resources
{
    public class ItemResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public string Condition { get; set; }
		public bool IsAuction { get; set; }
		public UserResource Seller { get; set; }
    }
} 