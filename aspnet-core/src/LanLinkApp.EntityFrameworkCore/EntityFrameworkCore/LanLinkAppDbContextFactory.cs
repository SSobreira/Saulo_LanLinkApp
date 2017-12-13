using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LanLinkApp.Configuration;
using LanLinkApp.Web;

namespace LanLinkApp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class LanLinkAppDbContextFactory : IDesignTimeDbContextFactory<LanLinkAppDbContext>
    {
        public LanLinkAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LanLinkAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            LanLinkAppDbContextConfigurer.Configure(builder, configuration.GetConnectionString(LanLinkAppConsts.ConnectionStringName));

            return new LanLinkAppDbContext(builder.Options);
        }
    }
}
