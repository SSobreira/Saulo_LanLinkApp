using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LanLinkApp.EntityFrameworkCore
{
    public static class LanLinkAppDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LanLinkAppDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LanLinkAppDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
