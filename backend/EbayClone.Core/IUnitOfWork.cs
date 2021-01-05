using System;
using System.Threading.Tasks;
using EbayClone.Core.Repositories;

namespace EbayClone.Core
{
    public interface IUnitOfWork : IDisposable
    {
         IItemRepository Items { get; }
         IUserRepository Users { get; }
         IFilePathRepository FilePaths { get; }
         Task<int> CommitAsync();
    }
}