using Insurance.Common.Dto;
using Insurance.Common.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize]
public class RequestController : BaseApiController
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet("load")]
    public async Task<IActionResult> Load(long id)
    {
        var result = await _requestService.Load(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(RequestApiDto dto)
    {
        var result = await _requestService.Update(dto);

        if (result.IsTransient())
            return NotFound();

        return Ok(result);
    }

    [HttpPost("update-collection")]
    public async Task<IActionResult> UpdateCollection([FromBody]CollectionApiDto dto)
    {
        var result = await _requestService.UpdateCollection(dto);

        return Ok(result);
    }

    [HttpGet("get-payment-info")]
    public async Task<IActionResult> GetPaymentInfo(long personId)
    {
        var result = await _requestService.GetPaymentInfo(personId);

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestService.Delete(id);

        return Ok();
    }
}