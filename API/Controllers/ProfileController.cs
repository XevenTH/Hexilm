using Application.Movies.DTO;
using Application.Profile;
using Application.Profile.DTO;
using AutoMapper;
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

    [HttpPut("updateProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO requestProfileDto)
    {
        var result = await Mediator.Send(new Update.Command { RequestProfile = requestProfileDto });

        return GetResult(result);
    }

    [HttpPost("favoriteMovieAction")]
    public async Task<IActionResult> FavoriteMovieAction([FromBody] FavoriteMovieDTO requestMovie)
    {
        var result = await Mediator.Send(new FavoriteMovieAction.Command { RequestFavoriteMovie = requestMovie });

        return GetResult(result);
    }
}