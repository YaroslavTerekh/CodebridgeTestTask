using CodebridgeTest.BL.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodebridgeTest.API.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly ApplicationInfo _applicationInfo;

    public HomeController(ApplicationInfo applicationInfo)
    {
        _applicationInfo = applicationInfo;
    }

    [HttpGet("ping")]
    public IActionResult GetApplicationVersion()
    {
        return Ok(_applicationInfo.ToString());
    }
}
