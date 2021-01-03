using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbayClone.Core.Models
{
    public class Item
    {   
        public Item()
        {
            ImageUrl = new Collection<FilePath>();
        }
        public int Id {get; set; }
        public string Title {get; set; }
        public string Description {get; set; }

		[Column(TypeName = "decimal(10,2)")]
        public decimal Price {get; set; }
        public string Condition {get; set; }
        public bool IsAuction {get; set; }
        public int SellerId {get; set; }
        public User Seller {get; set; }
        public ICollection<FilePath> ImageUrl {get; set;}
    }    
}