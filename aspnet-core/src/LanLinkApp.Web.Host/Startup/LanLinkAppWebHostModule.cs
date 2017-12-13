using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LanLinkApp.Configuration;

namespace LanLinkApp.Web.Host.Startup
{
    [DependsOn(
       typeof(LanLinkAppWebCoreModule))]
    public class LanLinkAppWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public LanLinkAppWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LanLinkAppWebHostModule).GetAssembly());
        }
    }
}
