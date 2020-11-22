using CalculateAPI.Service.Models;
using System;

namespace CalculateAPI.Service.Services
{
    public class CoordinateService : ICoordinateService
    {

        public double CalculateDistance(Coordinate coordinate)
        {
            var distance = GetDistance(
                    Convert.ToDouble(coordinate.Source_Latitude),
                    Convert.ToDouble(coordinate.Source_Longitude),
                    Convert.ToDouble(coordinate.Destination_Latitude),
                    Convert.ToDouble(coordinate.Destination_Longitude));
            return distance;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.device.location.geocoordinate.getdistanceto?view=netframework-4.8
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        private static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var d1 = lat1 * (Math.PI / 180.0);
            var num1 = lon1 * (Math.PI / 180.0);
            var d2 = lat2 * (Math.PI / 180.0);
            var num2 = lon2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6371 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }


    }
}
