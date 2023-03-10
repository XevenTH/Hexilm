namespace API.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Model;

public class TokenFactory
{
    public readonly IConfiguration _config;
    private readonly UserManager<UserApp> _manager;
    public TokenFactory(IConfiguration config, UserManager<UserApp> manager)
    {
        _manager = manager;
        _config = config;
    }

    public async Task<string> CreateToken(UserApp user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("id", user.Id),
            new Claim("email", user.Email),
            new Claim("username", user.UserName),
        };

        var Roles = await _manager.GetRolesAsync(user);
        claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credential
        };

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
