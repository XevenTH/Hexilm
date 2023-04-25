using API.Controllers.Attributes;
using API.Controllers.DTO;
using Application.Core.Parameters;
using Application.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;

[Authorize]
public class PhotosController : BaseApiController
{
    [HttpPost("upload")]
    public async Task<ActionResult<PhotoResponder>> PostPhotoProfile([FromForm] PhotoUploadModel file, [FromQuery][BindRequired] string mode)
    {
        if (file == null || file.File == null || file.File.Length == 0) return BadRequest(CreateResponse(400, "Please Proveide Photo Image"));

        using Stream fileStream = file.File.OpenReadStream();

        var result = await Mediator.Send(new Upload.Command { File = fileStream, FileName = file.File.Name, Param = mode });

        return GetResult(result);
    }
    
    [HttpPut("{publicId}/manage-main")]
    public async Task<IActionResult> SetMainProfile(string publicId)
    {
        var result = await Mediator.Send(new ManageMain.Command { PublicId = publicId });

        return GetResult(result);
    }

    [AuthorizeWithQuery(Role = "admin", RestrictedQueryParams = new[] { "movie", "actor", "director" })]
    [HttpPut("save")]
    public async Task<IActionResult> SaveInt([FromBody] PhotoResponder photoResponder, [FromQuery] PhotoQuery query)
    {
        var result = await Mediator.Send(new Save.Command { photoResponder = photoResponder, Param = query });

        return GetResult(result);
    }

    [AuthorizeWithQuery(Role = "admin", RestrictedQueryParams = new[] { "movie", "actor", "director" })]
    [HttpDelete("{publicId}")]
    public async Task<IActionResult> DeletePhoto([FromRoute] string publicId, [FromQuery] PhotoQuery query)
    {
        var result = await Mediator.Send(new Delete.Command { PublicId = publicId, Param = query });

        return GetResult(result);
    }
}