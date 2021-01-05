using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;

namespace EbayClone.Core.Services
{
    public interface IFilePathService
    {
		Task<FilePath> CreateFilePath(FilePath newFilePath);

		Task DeleteFilePath(FilePath filePath);

		Task<IEnumerable<FilePath>> GetAllWithItemAndUser();

		Task<FilePath> GetFilePathById(int filePathId);

		Task<IEnumerable<FilePath>> GetFilePathsByItemId(int itemId);

		Task<IEnumerable<FilePath>> GetFilePathsByUserId(int userId);

		Task UpdateFilePath(FilePath currentFilePath, FilePath modifiedFilePath);
    }
}