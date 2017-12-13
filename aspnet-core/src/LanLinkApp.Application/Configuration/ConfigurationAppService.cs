using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LanLinkApp.Configuration.Dto;

namespace LanLinkApp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LanLinkAppAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
