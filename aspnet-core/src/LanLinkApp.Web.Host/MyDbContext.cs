using Abp.EntityFrameworkCore;
using LanLinkApp.Web.Host.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LanLinkApp.Web.Host
{
    public class MyDbContext : AbpDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
    }

    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    //{
    //    public MyDbContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //        var builder = new DbContextOptionsBuilder<MyDbContext>();

    //        var connectionString = configuration.GetConnectionString("Server=(localdb)\\mssqllocaldb;Database=LanLinkAppDb;Trusted_Connection=True;");

    //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LanLinkAppDb;Trusted_Connection=True;");

    //        return new MyDbContext(builder.Options);
    //    }
    //}
}
