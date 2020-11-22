using Dominos.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dominos.Data.Configuration
{
    public class CoordinateConfiguration : IEntityTypeConfiguration<Coordinate>
    {
        public void Configure(EntityTypeBuilder<Coordinate> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Source_Latitude)
                .IsRequired();

            builder
                .Property(m => m.Source_Longitude)
                .IsRequired();


            builder
                .Property(m => m.Destination_Latitude)
                .IsRequired();


            builder
                .Property(m => m.Destination_Longitude)
                .IsRequired();

            builder.ToTable("Coordinate");
        }
    }
}
