using System.Collections.Generic;
using EbayClone.Core.Models;

namespace EbayClone.API.Resources
{
    public class SaveUserResource : UserSignUpResource
    {
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public ICollection<Item> SellingItems { get; set; }
    }
}