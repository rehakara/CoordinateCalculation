using Dominos.Core;
using Dominos.Core.Models;
using Dominos.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominos.Services
{
    public class CoordinateService : ICoordinateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoordinateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Coordinate> CreateCoordinate(Coordinate newCoordinate)
        {
            await _unitOfWork.Coordinates
                .AddAsync(newCoordinate);

            return newCoordinate;
        }

        public async Task DeleteCoordinate(Coordinate coordinate)
        {
            _unitOfWork.Coordinates.Remove(coordinate);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Coordinate>> GetAllCoordinates()
        {
            return await _unitOfWork.Coordinates.GetAllAsync();
        }

        public async Task<Coordinate> GetCoordinateById(long id)
        {
            return await _unitOfWork.Coordinates.GetByIdAsync(id);
        }

        public async Task UpdateCoordinate(Coordinate coordinateToBeUpdated, Coordinate coordinate)
        {
            coordinateToBeUpdated.Source_Latitude = coordinate.Source_Latitude;
            coordinateToBeUpdated.Source_Longitude = coordinate.Source_Longitude;
            coordinateToBeUpdated.Destination_Latitude = coordinate.Destination_Latitude;
            coordinateToBeUpdated.Destination_Longitude = coordinate.Destination_Longitude;

            await _unitOfWork.CommitAsync();
        }
    }
}
