using System.Security.Claims;
using API.Controllers.DTO;
using API.Services;
using Controllers.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("Api/[Controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserApp> _manager;
    private readonly SignInManager<UserApp> _signInManager;
    private readonly TokenFactory _tokenFactory;

    public AccountController(UserManager<UserApp> manager, SignInManager<UserApp> signInManager, TokenFactory tokenFactory)
    {
        _manager = manager;
        _signInManager = signInManager;
        _tokenFactory = tokenFactory;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> UserLogin([FromBody] LoginDTO loginData)
    {
        var user = await _manager.FindByEmailAsync(loginData.Email);

        if (user == null) return NotFound();

        var checker = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

        if (checker.Succeeded)
        {
            return Ok(await CreateUserDTO(user));
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> UserRegister([FromBody] RegisterDTO user)
    {
        if (await _manager.Users.AnyAsync(data => data.Email == user.Email))
        {
            ModelState.AddModelError("email", "Email Already Taken");
            return ValidationProblem();
        }
        if (await _manager.Users.AnyAsync(data => data.UserName == user.Username))
        {
            ModelState.AddModelError("username", "Username Already Taken");
            return ValidationProblem();
        }

        UserApp newUser = new UserApp
        {
            Displayname = user.Displayname,
            Email = user.Email,
            UserName = user.Username,
        };

        var result = await _manager.CreateAsync(newUser, user.password);

        if (result.Succeeded)
        {
            return Ok(await CreateUserDTO(newUser));
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserAdminDTO>> GetUser()
    {
        var user = await _manager.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue("id"));
        if (user == null) return NotFound();

        var roleChecker = await _manager.IsInRoleAsync(user, "admin");

        return Ok(new UserAdminDTO {
            Displayname = user.Displayname,
            Username = user.UserName,
            IsAdmin = roleChecker,
            Token = await _tokenFactory.CreateToken(user),
        });
    }

    private async Task<UserDTO> CreateUserDTO(UserApp user)
    {
        return new UserDTO
        {
            Displayname = user.Displayname,
            Username = user.UserName,
            Token = await _tokenFactory.CreateToken(user),
        };
    }
}