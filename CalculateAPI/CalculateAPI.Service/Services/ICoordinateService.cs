using CalculateAPI.Service.Models;

namespace CalculateAPI.Service.Services
{
    public interface ICoordinateService
    {
        double CalculateDistance(Coordinate coordinate);
    }
}
