using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v2/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult GetResult<T>(ResultValidator<T> result)
    {
        if (result.IsSuccess && result.Value != null && result.StatusCode == 200) return Ok(result.Value);
        if (!result.IsSuccess && result.Value == null && result.StatusCode == 404) return NotFound(CreateResponse(Response.StatusCode, result.Message));

        return BadRequest(CreateResponse(Response.StatusCode, result.Message));
    }

    protected object CreateResponse(int code, string message)
    {
        return new
        {
            StatusCode = code,
            Message = message
        };
    }
}
