using static OnlineStore.App.Constants;

namespace OnlineStore.App.Helpers;

public static class HttpContextAccessor
{
    public static long GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return long.TryParse(httpContextAccessor.HttpContext.Items[HttpContextItemKeyConstants.UserId]?.ToString(), out var userId)
            ? userId
            : 0;
    }
}
