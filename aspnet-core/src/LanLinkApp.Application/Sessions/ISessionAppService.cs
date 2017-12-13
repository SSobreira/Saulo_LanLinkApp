using System.Threading.Tasks;
using Abp.Application.Services;
using LanLinkApp.Sessions.Dto;

namespace LanLinkApp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
