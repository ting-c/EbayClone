namespace EbayClone.API.Resources
{
    public class ItemResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public UserResource Seller { get; set; }
    }
} 