using Application.MovieRoom;
using Application.MovieRoom.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RoomController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetAllRoom()
    {
        var result = await Mediator.Send(new List.Query());

        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoom(Guid id)
    {
        var result = await Mediator.Send(new SingleMovie.Query { Id = id });

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDTO>> CreateRoom([FromBody] RequestRoomDTO room)
    {
        var result = await Mediator.Send(new Create.Query { Room = room });

        return result;
    }
}
