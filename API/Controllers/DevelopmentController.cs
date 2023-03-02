using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

[AllowAnonymous]
public class DevelopmentController : BaseApiController
{
    private readonly UserManager<UserApp> _manager;
    public DevelopmentController(UserManager<UserApp> manager)
    {
        _manager = manager;
    }

    [HttpDelete("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _manager.FindByIdAsync(id);

        if(user == null) return BadRequest();

        var result =  await _manager.DeleteAsync(user);

        if(result.Succeeded)
        {
            return Ok();
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
