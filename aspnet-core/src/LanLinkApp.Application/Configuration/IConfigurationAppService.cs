using System.Threading.Tasks;
using LanLinkApp.Configuration.Dto;

namespace LanLinkApp.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
