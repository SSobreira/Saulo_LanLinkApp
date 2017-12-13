using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LanLinkApp.Controllers
{
    public abstract class LanLinkAppControllerBase: AbpController
    {
        protected LanLinkAppControllerBase()
        {
            LocalizationSourceName = LanLinkAppConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
