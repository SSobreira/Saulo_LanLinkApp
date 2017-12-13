using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using LanLinkApp.Authorization;

namespace LanLinkApp
{
    [DependsOn(
        typeof(LanLinkAppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class LanLinkAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<LanLinkAppAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(LanLinkAppApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}
