using Application.Profile;
using Application.Profile.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class ProfileController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileDTO>> Profile(string id)
    {
        var result = await Mediator.Send(new GetProfile.Query { Id = id });

        return GetResult(result);
    }
}
