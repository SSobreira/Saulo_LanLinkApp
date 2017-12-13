using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LanLinkApp.Roles.Dto;
using LanLinkApp.Users.Dto;

namespace LanLinkApp.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
