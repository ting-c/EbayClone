using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data.Repositories
{
    public class FilePathRepository : Repository<FilePath>, IFilePathRepository
    {
        public FilePathRepository(EbayCloneDbContext context) : base(context)
        {}

        public async Task<IEnumerable<FilePath>> GetAllWithItemAndUserAsync()
        {
            return await EbayCloneDbContext.FilePaths
                .Include(f => f.Item)
                .Include(f => f.User)
                .ToListAsync();
        }

        public async Task<FilePath> GetWithItemAndUserByFilePathIdAsync(int filePathId)
        {
            return await EbayCloneDbContext.FilePaths
                .Include(f => f.Item)
                .Include(f => f.User)
                .SingleOrDefaultAsync(f => f.Id == filePathId);
        }

        public async Task<IEnumerable<FilePath>> GetAllWithItemAndUserByUserIdAsync(int userId)
        {
            return await EbayCloneDbContext.FilePaths
                .Include(f => f.Item)
                .Include(f => f.User)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FilePath>> GetAllWithItemAndUserByItemIdAsync(int itemId)
        {
            return await EbayCloneDbContext.FilePaths
                .Include(f => f.Item)
                .Include(f => f.User)
                .Where(f => f.ItemId == itemId)
                .ToListAsync();
        }

        private EbayCloneDbContext EbayCloneDbContext
        {
            get { return Context as EbayCloneDbContext; }
        }
    }
}