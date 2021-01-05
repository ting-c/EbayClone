using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;

namespace EbayClone.Services
{
    public class FilePathService : IFilePathService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FilePathService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<FilePath> CreateFilePath(FilePath newFilePath)
        {
            await _unitOfWork.FilePaths.AddAsync(newFilePath);
            await _unitOfWork.CommitAsync();
            return newFilePath;
        }

        public async Task DeleteFilePath(FilePath filePath)
        {
            _unitOfWork.FilePaths.Remove(filePath);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<FilePath>> GetAllWithItemAndUser()
        {
            return await _unitOfWork.FilePaths.GetAllWithItemAndUserAsync();
        }

        public async Task<FilePath> GetFilePathById(int filePathId)
        {
            return await _unitOfWork.FilePaths.GetWithItemAndUserByFilePathIdAsync(filePathId);
        }

        public async Task<IEnumerable<FilePath>> GetFilePathsByItemId(int itemId)
        {
            return await _unitOfWork.FilePaths.GetAllWithItemAndUserByItemIdAsync(itemId);
        }

        public async Task<IEnumerable<FilePath>> GetFilePathsByUserId(int userId)
        {
            return await _unitOfWork.FilePaths.GetAllWithItemAndUserByUserIdAsync(userId);
        }

        public async Task UpdateFilePath(FilePath currentFilePath, FilePath modifiedFilePath)
        {
            modifiedFilePath.Id = currentFilePath.Id;

            _unitOfWork.FilePaths.Update(currentFilePath, modifiedFilePath);

            await _unitOfWork.CommitAsync();
        }
    }
}