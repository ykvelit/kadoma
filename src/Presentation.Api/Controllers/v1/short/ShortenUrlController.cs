using Application.Commands.ShortenUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ykvelit.Extensions.AspNetCore.Filters;

namespace Presentation.Api.Controllers.v1.@short;

[ApiController]
[Route("api/v1/short")]
public class ShortenUrlController(ISender sender) : ControllerBase
{
    [HttpPost]
    [UnitOfWork]
    public async Task<IActionResult> PostAsync([FromBody] string url, CancellationToken cancellationToken)
    {
        var command = new ShortenUrlCommand(url);
        var result = await sender.Send(command, cancellationToken);
        var response = $"{Request.Scheme}:{Request.Host}/api/v1/short/{result}";
        return Ok(response);
    }
}