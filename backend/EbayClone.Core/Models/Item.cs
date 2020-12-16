namespace EbayClone.Core.Models
{
    public class Item
    {   
        public int Id {get; set; }
        public string Title {get; set; }
        public int SellerId {get; set; }
        public User Seller {get; set; }
    }    
}