using System.ComponentModel.DataAnnotations;

namespace EbayClone.Core.Models
{
    public class BasketItem
    {   
        public BasketItem(int itemId, int userId, int quantity=1)
        {
            ItemId = itemId;
            UserId = userId;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId {get; set;}
        public User User {get; set;}

        [Range(1,50)]
        public int Quantity { get; set; }
    }
}