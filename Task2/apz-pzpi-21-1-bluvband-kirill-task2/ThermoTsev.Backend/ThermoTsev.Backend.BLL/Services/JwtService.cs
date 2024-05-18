using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public IConfiguration Configuration { get; set; } = configuration;

    public string GenerateToken(int userId, Role role)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                Configuration.GetSection("Jwt:Token")
                    .Value!
            )
        );
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        string? jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
