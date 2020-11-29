using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
         Task<IEnumerable<User>> GetAllWithItemsAsync();
         Task<User> GetWithItemByIdAsync(int itemId);
    }
}