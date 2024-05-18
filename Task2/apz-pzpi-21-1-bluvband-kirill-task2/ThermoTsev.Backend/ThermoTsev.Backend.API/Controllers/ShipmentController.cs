using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class ShipmentsController(IShipmentService shipmentService, IIoTProviderService ioTProviderService) : ControllerBase
{
    [HttpGet("GetCurrentShipmentLocation/{shipmentId:int}")]
    public async Task<IActionResult> GetCurrentShipmentLocation(int shipmentId)
    {
        Result<ShipmentLocationDto?> result = await ioTProviderService.GetCurrentShipmentLocation(shipmentId);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Errors);
    }
    
    [HttpGet]
    public IActionResult GetAllShipments()
    {
        Result<List<Shipment>> result = shipmentService.GetAllShipments();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetShipmentById(int id)
    {
        Result<Shipment> result = shipmentService.GetShipmentById(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Errors);
    }

    [HttpPost]
    public IActionResult CreateShipment([FromBody] CreateShipmentDto shipment)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Result<CreateShipmentDto> result = shipmentService.CreateShipment(userId, shipment);

        if (result.IsSuccess)
            return Ok("Successfully created");

        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateShipment(int id, [FromBody] UpdateShipmentDto updatedShipment)
    {
        if (id != updatedShipment.Id)
            return BadRequest("Invalid Id");

        Result<UpdateShipmentDto> result = shipmentService.UpdateShipment(updatedShipment);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteShipment(int id)
    {
        Result result = shipmentService.DeleteShipment(id);

        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Errors);
    }
}
