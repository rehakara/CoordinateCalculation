using Dominos.Services;
using NUnit.Framework;
using System;
using System.IO;

namespace Dominos.Tests.Dominos.Services.Tests
{
    public class FileService_WriteToOutputShould
    {

        [Test]
        public void WriteToOuput_Should_Create_Output()
        {
            var fileService = new FileService();
            fileService.WriteToOutput(DateTime.Now, DateTime.Now, 0);
            string fileName = @"Output.txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            string readText = File.ReadAllText(destPath);
            Assert.IsNotNull(readText);
            Assert.IsNotEmpty(readText);
        }
    }
}
