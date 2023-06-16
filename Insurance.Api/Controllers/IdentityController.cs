using Insurance.Common.Dto;
using Insurance.Common.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

public class IdentityController : BaseApiController
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] UserInfoDto userInfoDto)
    {
        var result = await _identityService.SignIn(userInfoDto);

        if (result.IsTransient())
            return NotFound();

        return Ok(result);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] UserInfoDto userInfoDto)
    {
        var result = await _identityService.SignUp(userInfoDto);

        if (result.IsTransient())
            return NotFound();

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long userId)
    {
        await _identityService.Delete(userId);

        return Ok();
    }
}