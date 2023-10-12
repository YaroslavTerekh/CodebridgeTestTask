using CodebridgeTest.BL.Behaviors.Dogs.CreateDog;
using CodebridgeTest.BL.Behaviors.Dogs.GetAllDogs;
using CodebridgeTest.BL.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodebridgeTest.API.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly ApplicationInfo _applicationInfo;
    private readonly IMediator _mediatr;

    public HomeController(ApplicationInfo applicationInfo, IMediator mediatr)
    {
        _applicationInfo = applicationInfo;
        _mediatr = mediatr;
    }

    [HttpGet("ping")]
    public IActionResult GetApplicationVersion()
    {
        return Ok(_applicationInfo.ToString());
    }

    [HttpGet("dogs")]
    public async Task<IActionResult> GetAllDogsAsync
    (
        [FromQuery] string? order,
        [FromQuery] string? attribute,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(new GetAllDogsQuery(attribute, order, pageNumber, pageSize), cancellationToken));

    [HttpPost("dog")]
    public async Task<IActionResult> CreateDogAsync
    (
        [FromBody] CreateDogCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(command, cancellationToken));
}
