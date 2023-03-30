using Application.MovieRoom;
using Application.MovieRoom.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RoomsController : BaseApiController
{
    private readonly IValidator<RequestRoomDTO> _validator;

    public RoomsController(IValidator<RequestRoomDTO> validator)
    {
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetAllRoom()
    {
        var result = await Mediator.Send(new List.Query());

        return GetResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoom([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new SingleMovie.Query { Id = id });

        return GetResult(result);
    }

    [HttpPut("{id}/manage-attende")]
    public async Task<IActionResult> UpdateAttendeesAction([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new UpdateAttendeesAction.Command { Id = id });

        return GetResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDTO>> CreateRoom([FromBody] RequestRoomDTO room)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(room);
        if (!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new Create.Query { Room = room });

        return GetResult(result);
    }
}