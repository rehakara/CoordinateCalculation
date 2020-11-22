using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Globalization;
using System.Text;

namespace Dominos.Data.Migrations
{
    public partial class SeedData : Migration
    {
        private static Random _random = new Random();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = new StringBuilder();
            for (int i = 0; i < 30000000; i++)
            {
                var source_latitude = RandomDouble(36.000000, 42.000000).ToGBString();
                var source_longitude = RandomDouble(26.000000, 45.000000).ToGBString();
                var destination_latitude = RandomDouble(36.000000, 42.000000).ToGBString();
                var destination_longitude = RandomDouble(26.000000, 45.000000).ToGBString();
                sql.AppendLine($"INSERT INTO dbo.Coordinate (Source_Latitude, Source_Longitude, Destination_Latitude, Destination_Longitude) VALUES ('{source_latitude}', '{source_longitude}', '{destination_latitude}', '{destination_longitude}')");
            }
            migrationBuilder
                .Sql(sql.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("TRUNCATE TABLE dbo.Coordinate");
        }

        public static double RandomDouble(double min, double max)
        {
            return (_random.NextDouble() * (max - min)) + min;
        }
    }

    public static class DoubleExtensions
    {
        public static string ToGBString(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}
