namespace EbayClone.API.Resources
{
    public class AuthResource
    {
        public string JwtString { get; set; }
        public UserResource User { get; set; }
    }
}