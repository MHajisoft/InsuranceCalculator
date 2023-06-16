using Insurance.Common.Dto;

namespace Insurance.Common.Interface;

public interface IIdentityService : IServiceBase
{
    public Task<AppUserDto> SignIn(UserInfoDto userInfoDto);

    public Task<AppUserDto> SignUp(UserInfoDto userInfoDto);

    public Task Delete(long userId);
}