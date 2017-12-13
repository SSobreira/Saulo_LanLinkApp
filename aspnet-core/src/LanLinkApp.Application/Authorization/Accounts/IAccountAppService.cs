using System.Threading.Tasks;
using Abp.Application.Services;
using LanLinkApp.Authorization.Accounts.Dto;

namespace LanLinkApp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
