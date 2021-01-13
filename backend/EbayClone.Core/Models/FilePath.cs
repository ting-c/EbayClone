using System.Text.Json.Serialization;

namespace EbayClone.Core.Models
{
    public class FilePath
    {
        public int Id { get; set; }
        public string UrlPath { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}