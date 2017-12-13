using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LanLinkApp.MultiTenancy.Dto;

namespace LanLinkApp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
