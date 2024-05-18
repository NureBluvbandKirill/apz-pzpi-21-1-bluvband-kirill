using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Analytic;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class AnalyticController(IAnalyticService analyticService) : ControllerBase
{
    [HttpGet("{analyticId:int}")]
    public async Task<IActionResult> GetAnalyticById(int analyticId)
    {
        Result<AnalyticDto> result = await analyticService.GetAnalyticByIdAsync(analyticId);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnalytics()
    {
        Result<List<AnalyticDto>> result = await analyticService.GetAllAnalyticsAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("{shipmentId:int}")]
    public async Task<IActionResult> CreateAnalytic(int shipmentId, [FromBody] AnalyticDto analyticDto)
    {
        Result<AnalyticDto> result = await analyticService.CreateAnalyticAsync(shipmentId, analyticDto);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPut("{analyticId:int}")]
    public async Task<IActionResult> UpdateAnalytic(int analyticId, [FromBody] AnalyticDto analyticDto)
    {
        Result<AnalyticDto> result = await analyticService.UpdateAnalyticAsync(analyticId, analyticDto);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("{analyticId:int}")]
    public async Task<IActionResult> DeleteAnalytic(int analyticId)
    {
        Result result = await analyticService.DeleteAnalyticAsync(analyticId);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}
