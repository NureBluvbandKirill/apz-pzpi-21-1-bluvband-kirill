using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Notification;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("{notificationId:int}")]
    public async Task<IActionResult> GetNotificationById(int notificationId)
    {
        Result<NotificationDto> result = await notificationService.GetNotificationById(notificationId);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        Result<List<NotificationDto>> result = await notificationService.GetAllNotifications();

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Result<NotificationDto> result = await notificationService.CreateNotification(userId, notificationDto);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPut("{notificationId:int}")]
    public async Task<IActionResult> UpdateNotification(int notificationId, [FromBody] NotificationDto notificationDto)
    {
        Result<NotificationDto> result = await notificationService.UpdateNotification(notificationId, notificationDto);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("{notificationId:int}")]
    public async Task<IActionResult> DeleteNotification(int notificationId)
    {
        Result result = await notificationService.DeleteNotification(notificationId);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }
}
