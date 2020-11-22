using Dominos.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominos.Core.Repositories
{
    public interface ICoordinateRepository : IRepository<Coordinate>
    {
        Task<IEnumerable<Coordinate>> GetAllWithCoordinatesAsync();

        Task<Coordinate> GetCoordinateWithIdAsync(long id);
    }
}
