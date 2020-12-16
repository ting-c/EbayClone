using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Repositories;
using EbayClone.Data.Repositories;

namespace EbayClone.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly EbayCloneDbContext _context;
        private ItemRepository _itemRepository;
        private UserRepository _userRepository;

        public UnitOfWork(EbayCloneDbContext context)
        {
            this._context = context;
        }
        
		public IItemRepository Items => _itemRepository = _itemRepository ?? new ItemRepository(_context);

		public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
		    _context.Dispose();
		}
	}
}