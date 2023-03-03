using Application.MovieRoom;
using Application.MovieRoom.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RoomController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoom(Guid id)
    {
        var result = await Mediator.Send(new SingleMovie.Query { Id = id });

        return result;
    }
}
