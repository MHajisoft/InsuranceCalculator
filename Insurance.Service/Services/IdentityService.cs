using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Insurance.Common.Base;
using Insurance.Common.Dto;
using Insurance.Common.Entity;
using Insurance.Common.Interface;
using Insurance.Service.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Insurance.Service.Services;

public class IdentityService : ServiceBase<BaseEntity>, IIdentityService
{
    private readonly UserManager<AppUser> _userManager;

    public IdentityService(AppDbContext dbContext, IMapper mapper, UserManager<AppUser> userManager) : base(dbContext, mapper)
    {
        _userManager = userManager;
    }

    private string GetToken(List<Claim> authClaims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(InsuranceConstant.SecurityKey)), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<AppUserDto> SignIn(UserInfoDto userInfoDto)
    {
        var user = await _userManager.FindByNameAsync(userInfoDto.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, userInfoDto.Password))
            return new AppUserDto();

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
        };

        if (!string.IsNullOrEmpty(user.Email))
            authClaims.Add(new(ClaimTypes.Email, user.Email));

        var roles = await _userManager.GetRolesAsync(user);
        authClaims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        var result = Mapper.Map<AppUserDto>(user);
        result.Token = GetToken(await GetAuthClaims(user));

        return result;
    }

    public async Task<AppUserDto> SignUp(UserInfoDto userInfoDto)
    {
        var appUser = await _userManager.FindByNameAsync(userInfoDto.Username);

        if (appUser is not null)
            throw new Exception($"User with email {userInfoDto.Email} or username {userInfoDto.Username} already exists.");

        var user = new AppUser { Email = userInfoDto.Email, UserName = userInfoDto.Username, Title = userInfoDto.Title, SecurityStamp = Guid.NewGuid().ToString() };

        var createResult = await _userManager.CreateAsync(user, userInfoDto.Password);

        if (!createResult.Succeeded)
            throw new Exception($"Unable to register user {userInfoDto.Username} errors: {GetErrorsText(createResult.Errors)}");

        appUser = await _userManager.FindByNameAsync(userInfoDto.Username);
        var result = Mapper.Map<AppUserDto>(user);
        result.Token = GetToken(await GetAuthClaims(appUser));

        return result;
    }

    private async Task<List<Claim>> GetAuthClaims(AppUser user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
        };

        if (!string.IsNullOrEmpty(user.Email))
            authClaims.Add(new(ClaimTypes.Email, user.Email));

        var roles = await _userManager.GetRolesAsync(user);
        authClaims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        return authClaims;
    }

    public async Task Delete(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new Exception("Id is not valid!");

        await _userManager.DeleteAsync(user);
    }

    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join("\r\n", errors.Select(error => error.Description).ToArray());
    }
}