using System;

namespace Dominos.Core.Services
{
    public interface IFileService
    {
        void WriteToOutput(DateTime processStartTime, DateTime processEndTime, long elapsedMs);
    }
}
