using Dominos.Core;
using Dominos.Core.Repositories;
using Dominos.Data.Repositories;
using System.Threading.Tasks;

namespace Dominos.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DominosDbContext _context;
        private CoordinateRepository _coordinateRepository;

        public UnitOfWork(DominosDbContext context)
        {
            this._context = context;
        }

        public ICoordinateRepository Coordinates => _coordinateRepository = _coordinateRepository ?? new CoordinateRepository(_context);

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
