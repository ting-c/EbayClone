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
         IBasketItemRepository BasketItems { get; }
         IOrderRepository Orders { get; }
         IOrderItemRepository OrderItems { get; }
         Task<int> CommitAsync();
    }
}