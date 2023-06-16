using Insurance.Common.Dto;
using Insurance.Common.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize(Roles = "Admin")]
public class PersonController : BaseApiController
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("load")]
    public async Task<IActionResult> Load(long id)
    {
        var result = await _personService.Load(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(PersonDto dto)
    {
        var result = await _personService.Update(dto);

        if (result.IsTransient())
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long id)
    {
        await _personService.Delete(id);

        return Ok();
    }
}