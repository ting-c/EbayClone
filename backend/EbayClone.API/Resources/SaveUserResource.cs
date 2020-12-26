namespace EbayClone.API.Resources
{
    public class SaveUserResource : UserSignUpResource
    {
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
    }
}