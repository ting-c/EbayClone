using System.Collections.Generic;
using EbayClone.Core.Models;

namespace EbayClone.API.Resources
{
    public class ItemResource : SaveItemResource
    {
        public int Id { get; set; }
        public User Seller {get; set; }
        public ICollection<FilePath> ImageUrl{ get; set; }
    }
} 