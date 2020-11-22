using Dominos.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Dominos.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICoordinateRepository Coordinates { get; }

        Task<int> CommitAsync();
    }
}
