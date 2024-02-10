using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Service.Contracts;

namespace sdlt.Service;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private User? _user;

    public AuthenticationService(ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, IConfiguration configuration,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);

        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, new List<string>() { "User" });

        return result;
    }
    public async Task<IdentityResult> CreateRole(CreateRoleViewDto model)
    {
        IdentityRole identityRole = new IdentityRole
        {
            Name = model.RoleName
        };
        IdentityResult result = await _roleManager.CreateAsync(identityRole);
        return result;
    }
    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);

        var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);

        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name of password.");

        return result;
    }

    public async Task<UserDto> GetMyself(IIdentity userIdentificator)
    {
        // User user = new();
        // if (Guid.TryParse(userIdentificator, out Guid _))
        // {
        //     user = await _userManager.GetUserAsync(new ClaimsPrincipal(
        //         new ClaimsIdentity(new[]
        //             {
        //                 new Claim(ClaimTypes.NameIdentifier, userIdentificator)
        //             })
        //         ));
        // }else{
        //     user = await _userManager.FindByNameAsync(userIdentificator);
        // }
        // if(user is null)
        //     throw new UserNotFoundException(userIdentificator);
        User user = await _userManager.GetUserAsync(new ClaimsPrincipal(userIdentificator));
        return _mapper.Map<UserDto>(user);
    }

    #region JWT
    public async Task<string> CreateToken()
    {
        var signinCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SDLT"]!);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, _user!.UserName),
            new Claim(ClaimTypes.NameIdentifier, _user!.Id)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
    #endregion
}