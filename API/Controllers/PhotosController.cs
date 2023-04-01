using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PhotosController : BaseApiController
{
    [HttpPost("profile-upload")]
    public async Task<IActionResult> PostPhoto([FromForm] ProfileUpload.Command command)
    {
        var result = await Mediator.Send(command);

        return GetResult(result);
    }

    [HttpDelete("{publicId}/profile-delete")]
    public async Task<IActionResult> DeletePhoto([FromRoute] string publicId)
    {
        var result = await Mediator.Send(new ProfileDelete.Command { PublicId = publicId });

        return GetResult(result);
    }

    [HttpPut("{publicId}/profile-manage-main")]
    public async Task<IActionResult> SetMainProfile(string publicId)
    {
        var result = await Mediator.Send(new ProfileMain.Command { PublicId = publicId });

        return GetResult(result);
    }
}