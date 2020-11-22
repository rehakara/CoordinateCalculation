using Dominos.Core.Models;

namespace Dominos.Messaging.Send.Sender
{
    public interface ICoordinateSender
    {
        void SendCoordinate(Coordinate coordinate);
    }
}
