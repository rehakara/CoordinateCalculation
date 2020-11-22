using Dominos.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominos.Core.Services
{
    public interface ICoordinateService
    {
        Task<IEnumerable<Coordinate>> GetAllCoordinates();
        Task<Coordinate> GetCoordinateById(long id);
        Task<Coordinate> CreateCoordinate(Coordinate newCoordinate);
        Task UpdateCoordinate(Coordinate coordinateToBeUpdated, Coordinate coordinate);
        Task DeleteCoordinate(Coordinate coordinate);
    }
}
