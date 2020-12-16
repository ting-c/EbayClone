using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System;

namespace EbayClone.Core.Models
{
    public class User
    {
        public User()
        {
            SellingItems = new Collection<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> SellingItems { get; set; }
    }
}