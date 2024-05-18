using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ThermoTsev.Backend.API.Attributes;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.ShipmentCount;

namespace ThermoTsev.Backend.API.Controllers;

[ApiController]
[AdminRoleAuthorize]
[Route("Api/[controller]")]
public class StatisticController(IStatisticService statisticService) : ControllerBase
{
    [HttpGet("ShipmentsStartCountPerDayLastMonth")]
    public IActionResult GetShipmentsStartCountPerDayLastMonth()
    {
        Result<List<ShipmentCountPerDayDto>> result = statisticService.GetShipmentsStartCountPerDayLastMonth();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("ShipmentsEndCountPerDayLastMonth")]
    public IActionResult GetShipmentsEndCountPerDayLastMonth()
    {
        Result<List<ShipmentCountPerDayDto>> result = statisticService.GetShipmentsEndCountPerDayLastMonth();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }
    
    [HttpGet("ShipmentCountLastWeek")]
    public IActionResult GetShipmentCountLastWeek()
    {
        Result<int> result = statisticService.GetShipmentCountLastWeek();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("AverageShipmentsPerDay")]
    public IActionResult GetAverageShipmentsPerDay()
    {
        Result<double> result = statisticService.GetAverageShipmentsPerDay();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("UserCount")]
    public IActionResult GetUserCount()
    {
        Result<int> result = statisticService.GetUserCount();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("DeliveredShipmentCount")]
    public IActionResult GetDeliveredShipmentCount()
    {
        Result<int> result = statisticService.GetDeliveredShipmentCount();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }
}
