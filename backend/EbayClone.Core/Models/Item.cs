namespace EbayClone.Core.Models
{
    public class Item
    {   
        public int Id {get; set; }
        public string Title {get; set; }
        public string Description {get; set; }
        public decimal Price {get; set; }
        public string Condition {get; set; }
        public bool IsAuction {get; set; }
        public int SellerId {get; set; }
        public User Seller {get; set; }
    }    
}