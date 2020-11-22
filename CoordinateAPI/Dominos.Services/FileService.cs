using Dominos.Core.Services;
using System;
using System.IO;

namespace Dominos.Services
{
    public class FileService : IFileService
    {
        public void WriteToOutput(DateTime processStartTime, DateTime processEndTime, long elapsedMs)
        {
            string fileName = @"Output.txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            using (StreamWriter sw = File.AppendText(destPath))
            {
                sw.WriteLineAsync($"Process Start Time: {processStartTime:dd.MM.yyyy hh:mm:ss}");
                sw.WriteLineAsync($"Process End Time: {processEndTime:dd.MM.yyyy hh:mm:ss}");
                sw.WriteLineAsync($"Elapsed ms: {elapsedMs}");
            }
        }
    }
}
