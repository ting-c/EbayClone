using System.Collections.Generic;
using System.Collections.ObjectModel;
using EbayClone.Core.Models;

namespace EbayClone.API.Resources
{
    public class UserResource
    {
		public UserResource()
		{
			SellingItems = new Collection<Item>();
		}
        public int Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public ICollection<Item> SellingItems { get; set; }
    }
}