using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EbayCloneDbContext context) : base(context)
        {}

        public async Task<IEnumerable<User>> GetAllWithItemsAsync()
        {
            return await EbayCloneDbContext.Users
                .Include(u => u.SellingItems)
                .ToListAsync();
        }

        public async Task<User> GetWithItemByIdAsync(int userId)
        {
            return await EbayCloneDbContext.Users
                .Include(u => u.SellingItems)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }

        private EbayCloneDbContext EbayCloneDbContext
        {
            get { return Context as EbayCloneDbContext; }
        }
    }
}