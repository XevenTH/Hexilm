using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    private readonly UserManager<UserApp> _manager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<UserApp> manager, RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
        _manager = manager;

    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([Required] string name)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

            if (result.Succeeded) return Ok("Role successfully Added");
        }

        return BadRequest("Oops somthing Wrong Aded");
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> AddRoleToUser(string email)
    {
        UserApp user = await _manager.FindByEmailAsync(email);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        var result = await _manager.AddToRoleAsync(user, "admin");

        if (result.Succeeded)
        {
            return Ok("Role 'admin' added to user successfully");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
}
