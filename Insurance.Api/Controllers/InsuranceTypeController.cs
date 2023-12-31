﻿using Insurance.Common.Dto;
using Insurance.Common.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize(Roles = "Admin")]
public class InsuranceTypeController : BaseApiController
{
    private readonly IInsuranceTypeService _insuranceTypeService;

    public InsuranceTypeController(IInsuranceTypeService insuranceTypeService)
    {
        _insuranceTypeService = insuranceTypeService;
    }

    [HttpGet("load")]
    public async Task<IActionResult> Load(long id)
    {
        var result = await _insuranceTypeService.Load(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(InsuranceTypeDto dto)
    {
        var result = await _insuranceTypeService.Update(dto);

        if (result.IsTransient())
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long id)
    {
        await _insuranceTypeService.Delete(id);

        return Ok();
    }
}