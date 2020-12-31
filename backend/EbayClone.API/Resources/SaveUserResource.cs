using System.Collections.Generic;
using EbayClone.Core.Models;

namespace EbayClone.API.Resources
{
    public class SaveUserResource : UserSignUpResource
    {
		public string PhoneNumber { get; set; }
		public ICollection<Item> SellingItems { get; set; }
    }
}