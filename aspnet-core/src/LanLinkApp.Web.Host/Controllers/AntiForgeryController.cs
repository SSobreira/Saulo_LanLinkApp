using Microsoft.AspNetCore.Antiforgery;
using LanLinkApp.Controllers;

namespace LanLinkApp.Web.Host.Controllers
{
    public class AntiForgeryController : LanLinkAppControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
