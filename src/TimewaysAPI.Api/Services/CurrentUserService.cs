using System.Security.Claims;
using TimewaysAPI.Application.Common;

namespace TimewaysAPI.Api.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public int UserId =>
        int.Parse(
            httpContextAccessor.HttpContext!
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier)!
        );
}