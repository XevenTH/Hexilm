using Application.Movies;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

public class MovieController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        var result = await Mediator.Send(new List.Query());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(Guid id)
    {
        var result = await Mediator.Send(new Application.Movies.Single.Query { Id = id });

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
    {
        var result = await Mediator.Send(new Create.Command { Movie = movie });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id ,[FromBody] Movie movie)
    {
        movie.Id = id;
        var result = await Mediator.Send(new Update.Command { Movie = movie });

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        var result = await Mediator.Send(new Delete.Command { Id = id });

        return Ok(result);
    }
}
