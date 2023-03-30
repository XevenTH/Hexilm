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
[Route("Api/v2/[Controller]")]
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
    public async Task<ActionResult<UserWithRoleDTO>> UserLogin([FromBody] LoginDTO loginData)
    {
        var user = await _manager.Users
            .Include(x => x.Photo)
            .FirstOrDefaultAsync(x => x.Email == loginData.Email);

        if (user == null) return NotFound();

        var checker = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

        if (checker.Succeeded)
        {
            return Ok(await CreateUserWithRoleInfoDTO(user));
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
            DisplayName = user.DisplayName,
            Email = user.Email,
            UserName = user.Username,
        };

        var result = await _manager.CreateAsync(newUser, user.password);

        if (result.Succeeded)
        {
            return Ok(new UserDTO
            {
                Id = newUser.Id,
                DisplayName = newUser.DisplayName,
                Username = newUser.UserName,
                Token = await _tokenFactory.CreateToken(newUser),
            });
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserWithRoleDTO>> GetUser()
    {
        var user = await _manager.Users
            .Include(x => x.Photo)
            .FirstOrDefaultAsync(x => x.Id == User.FindFirstValue("id"));
        if (user == null) return NotFound();

        return Ok(await CreateUserWithRoleInfoDTO(user));
    }

    private async Task<UserWithRoleDTO> CreateUserWithRoleInfoDTO(UserApp user)
    {
        var roles = await _manager.GetRolesAsync(user);
        var roleList = roles.FirstOrDefault();

        return new UserWithRoleDTO
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Username = user.UserName,
            Photo = user?.Photo?.FirstOrDefault(x => x.IsMain)?.Url,
            Role = (roleList != null) ? roleList : "user",
            Token = await _tokenFactory.CreateToken(user),
        };
    }
}