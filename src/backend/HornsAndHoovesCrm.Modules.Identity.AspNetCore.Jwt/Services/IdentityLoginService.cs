using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HornsAndHoovesCrm.AspNetCore.Jwt.Models;
using HornsAndHoovesCrm.AspNetCore.Jwt.Services;
using HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services.Abstractions;
using HornsAndHoovesCrm.Modules.Identity.Domain;
using HornsAndHoovesCrm.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HornsAndHoovesCrm.Modules.Identity.AspNetCore.Jwt.Services;

public class IdentityLoginService<TIdentityUser, TIdentityRole> : ILoginService<LoginModel>
    where TIdentityRole : CrmIdentityRole
    where TIdentityUser : CrmIdentityUser<TIdentityRole>
{
    private readonly IJwtConfigurationProvider _configurationProvider;
    private readonly SignInManager<TIdentityUser> _signInManager;
    private readonly UserManager<TIdentityUser> _userManager;
    private readonly RoleManager<TIdentityRole> _roleManager;

    public IdentityLoginService(IJwtConfigurationProvider configurationProvider,
        SignInManager<TIdentityUser> signInManager, UserManager<TIdentityUser> userManager,
        RoleManager<TIdentityRole> roleManager)
    {
        _configurationProvider = configurationProvider;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task<LoginResult?> LoginAsync(LoginModel loginModel)
    {
        var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            var claims = await GetValidClaims(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();
            foreach (var roleName in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if (!rolesClaims.Any(x => x.Type == roleClaim.Type))
                    {
                        rolesClaims.Add(roleClaim);
                    }
                }
            }

            claims.AddRange(rolesClaims);
            claims.Insert(0, new Claim(ApplicationClaimTypes.UserId, user.Id.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_configurationProvider.Configuration.TokenLifetimeMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationProvider.Configuration.Secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return new LoginResult(token, user.UserName, user.DisplayUserName, userRoles.ToArray(),
                rolesClaims.Select(x => x.Value).Distinct().ToArray());
        }

        return null;
    }

    private async Task<List<Claim>> GetValidClaims(TIdentityUser user)
    {
        IdentityOptions options = new IdentityOptions();
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };
        var userClaims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userClaims);
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (Claim roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }
        }

        return claims;
    }
}