using Dominos.Core.Models;
using Dominos.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Dominos.Data
{
    public class DominosDbContext : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }

        public DominosDbContext(DbContextOptions<DominosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CoordinateConfiguration());
        }
    }
}
