using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Repositories
{
    public interface IFilePathRepository : IRepository<FilePath>
    {
		Task<IEnumerable<FilePath>> GetAllWithItemAndUserAsync();
		Task<FilePath> GetWithItemAndUserByFilePathIdAsync(int filePathId);
		Task<IEnumerable<FilePath>> GetAllWithItemAndUserByUserIdAsync(int userId);
        Task<IEnumerable<FilePath>> GetAllWithItemAndUserByItemIdAsync(int itemId);
    }
}