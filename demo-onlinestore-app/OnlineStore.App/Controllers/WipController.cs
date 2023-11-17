using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.App.Controllers;

[ApiController]
[Route("[controller]")]
public class WipController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => this.Ok();

    [HttpGet("secure")]
    [Authorize]
    public IActionResult GetSecure()
        => this.Ok();
}
