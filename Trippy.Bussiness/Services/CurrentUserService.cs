using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Trippy.Business.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? UserId =>
            _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? UserName =>
            _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Email =>
            _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.MobilePhone)?.Value;
    }
}
