using Dominos.Data;
using Dominos.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dominos.Tests.Dominos.Data.Tests
{
    public class RepositoryTests
    {
        [Test]
        public async Task GetCoordinateById_Gets_The_Given_Coordinate()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            var options = new DbContextOptionsBuilder<DominosDbContext>().UseSqlServer(config.GetConnectionString("DevConnection"), x => x.MigrationsAssembly("Dominos.Data")).Options;
            var context = new DominosDbContext(options);
            var repository = new CoordinateRepository(context);

            var coordinate = await repository.GetCoordinateWithIdAsync(1);
            Assert.AreEqual(1, coordinate.Id);
        }
    }
}
