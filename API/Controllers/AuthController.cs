using API.Controllers.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

[Authorize(Roles = "admin")]
public class AuthController : BaseApiController
{
    private readonly UserManager<UserApp> _manager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<UserApp> manager, RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _manager = manager;
    }

    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole([FromBody] AuthDTO requestAuth)
    {
        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(requestAuth.RoleName));

        if (result.Succeeded) return Ok(CreateResponseAuth(StatusCodes.Status200OK, $"Successfully Create {requestAuth.RoleName} Role"));

        return BadRequest(CreateResponseAuth(StatusCodes.Status400BadRequest, $"Something Wrong While Creating {requestAuth.RoleName} Role"));
    }

    [HttpDelete("deleteRole")]
    public async Task<IActionResult> DeleteRole([FromBody] AuthDTO requestAuth)
    {
        var userInRoles = await _manager.GetUsersInRoleAsync("admin");
        List<string> rolesToDelete = new List<string> { "admin" };

        if (userInRoles.Any())
        {
            foreach (UserApp user in userInRoles)
            {
                await _manager.RemoveFromRolesAsync(user, rolesToDelete);
            }
        }

        var role = await _roleManager.FindByNameAsync(requestAuth.RoleName);

        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return Ok(CreateResponseAuth(StatusCodes.Status200OK, $"Successfully Delete {requestAuth.RoleName} Role"));
        }
        else
        {
            return NotFound(CreateResponseAuth(StatusCodes.Status404NotFound, $"Can't Find {requestAuth.RoleName} Role"));
        }

        return BadRequest();
    }

    [HttpPost("addToRole")]
    public async Task<IActionResult> AddRoleToUser([FromBody] AuthDTO requestAuth)
    {
        UserApp user = await _manager.FindByEmailAsync(requestAuth.Email);

        if (user == null) return NotFound(CreateResponseAuth(StatusCodes.Status404NotFound, $"Can't Find User With Email {requestAuth.Email}"));

        var userRoleCheck = await _manager.IsInRoleAsync(user, "admin");

        if (!userRoleCheck)
        {
            var result = await _manager.AddToRoleAsync(user, "admin");

            if (result.Succeeded)
            {
                return Ok(CreateResponseAuth(StatusCodes.Status200OK, $"Successfully Add {requestAuth.Email} To {requestAuth.RoleName}"));
            }
            else
            {
                return NotFound(CreateResponseAuth(StatusCodes.Status404NotFound, $"Can't Find {requestAuth.RoleName} Role"));
            }
        }
        else
        {
            return BadRequest(CreateResponseAuth(StatusCodes.Status400BadRequest, $"Can't Add {requestAuth.RoleName} Role, User Can Only have One role"));
        }
    }
}
