using Controllers.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<UserApp> _manager;
    private readonly SignInManager<UserApp> _signInManager;

    public AccountController(UserManager<UserApp> manager, SignInManager<UserApp> signInManager)
    {
        _manager = manager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> UserLogin([FromBody] LoginDTO loginData)
    {
        var user = await _manager.Users.FirstOrDefaultAsync(x => x.Email == loginData.Email);

        if(user == null) return Unauthorized();

        var checker = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

        if(checker.Succeeded)
        {
            return new UserDTO
            {
                Displayname = user.DisplayName,
                Username = user.UserName,
                Token = "",
            };
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> UserRegister([FromBody] RegisterDTO user)
    {
        if (await _manager.Users.AnyAsync(data => data.Email == user.Email))
        {
            ModelState.AddModelError("Email", "Email Already Taken");
            return ValidationProblem();
        }
        if (await _manager.Users.AnyAsync(data => data.UserName == user.Username))
        {
            ModelState.AddModelError("Username", "Username Already Taken");
            return ValidationProblem();
        }

        UserApp newUser = new UserApp
        {
            DisplayName = user.Displayname,
            Email = user.Email,
            UserName = user.Username,
        };

        var result = await _manager.CreateAsync(newUser, user.Password);

        if (result.Succeeded)
        {
            return new UserDTO
            {
                Displayname = newUser.DisplayName,
                Username = newUser.UserName,
                Token = "",
            };
        }

        return Ok(result);
    }
}
