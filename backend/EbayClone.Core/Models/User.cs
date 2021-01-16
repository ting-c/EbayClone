using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace EbayClone.Core.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            SellingItems = new Collection<Item>();
            Basket = new Collection<BasketItem>();
            ImageUrl = new Collection<FilePath>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ICollection<Item> SellingItems { get; set; }
        public ICollection<BasketItem> Basket { get; set; }
        public ICollection<FilePath> ImageUrl { get; set; }
    }
}