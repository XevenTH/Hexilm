using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult GetResult<T>(ResultValidator<T> result)
    {
        if (result.IsSuccess && result.Value != null) return Ok(result.Value);
        if (!result.IsSuccess && result.Value == null) return NotFound(CreateResponseAuth(StatusCodes.Status404NotFound, result.Message));

        return BadRequest();
    }

    protected object CreateResponseAuth(int code, string message)
    {
        return new
        {
            StatusCode = code,
            Message = message
        };
    }
}
