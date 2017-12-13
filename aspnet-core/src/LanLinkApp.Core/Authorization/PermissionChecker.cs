using Abp.Authorization;
using LanLinkApp.Authorization.Roles;
using LanLinkApp.Authorization.Users;

namespace LanLinkApp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
