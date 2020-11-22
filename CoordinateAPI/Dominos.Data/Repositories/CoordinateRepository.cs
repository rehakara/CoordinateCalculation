using Dominos.Core.Models;
using Dominos.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominos.Data.Repositories
{
    public class CoordinateRepository : Repository<Coordinate>, ICoordinateRepository
    {
        public CoordinateRepository(DominosDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Coordinate>> GetAllWithCoordinatesAsync()
        {
            return await DominosDbContext.Coordinates
                .ToListAsync();
        }

        public async Task<Coordinate> GetCoordinateWithIdAsync(long id)
        {
            return await DominosDbContext.Coordinates
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        private DominosDbContext DominosDbContext
        {
            get { return Context as DominosDbContext; }
        }
    }
}
