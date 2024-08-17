using Application.Queries.GetShortenedUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers.v1.@short;

[ApiController]
[Route("api/v1/short/{code}")]
public class GetShortenedUrlController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromRoute] string code, CancellationToken cancellationToken)
    {
        var query = new GetShortenedUrlQuery(code);
        var result = await sender.Send(query, cancellationToken);
        return Redirect(result);
    }
}