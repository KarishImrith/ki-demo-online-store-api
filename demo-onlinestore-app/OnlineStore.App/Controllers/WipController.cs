using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.App.Helpers;

namespace OnlineStore.App.Controllers;

[ApiController]
[Route("[controller]")]
public class WipController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WipController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult Get()
        => this.Ok();

    [HttpGet("secure")]
    [Authorize]
    public IActionResult GetSecure()
        => this.Ok(_httpContextAccessor.GetUserId());
}
