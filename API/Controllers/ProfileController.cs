using Application.Movies.DTO;
using Application.Profile;
using Application.Profile.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseApiController
{
    [HttpGet("{username}")]
    public async Task<ActionResult<ProfileDTO>> Profile(string username)
    {
        var result = await Mediator.Send(new GetProfile.Query { Username = username });

        return GetResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO requestProfileDto)
    {
        var result = await Mediator.Send(new Update.Command { RequestProfile = requestProfileDto });

        return GetResult(result);
    }
    
    [HttpPost("manage-favorite-movie")]
    public async Task<IActionResult> FavoriteMovieAction([FromBody] FavoriteMovieDTO requestMovie)
    {
        var result = await Mediator.Send(new FavoriteMovieAction.Command { RequestFavoriteMovie = requestMovie });

        return GetResult(result);
    }
}