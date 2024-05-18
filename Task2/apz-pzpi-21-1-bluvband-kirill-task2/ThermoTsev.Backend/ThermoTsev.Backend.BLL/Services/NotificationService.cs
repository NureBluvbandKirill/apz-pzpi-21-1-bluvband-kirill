using FluentResults;
using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Notification;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class NotificationService(DataContext context) : INotificationService
{
    public async Task<Result<NotificationDto>> GetNotificationById(int notificationId)
    {
        Notification? notification = await context.Notifications
            .FindAsync(notificationId);

        return notification == null ? Result.Fail<NotificationDto>($"Notification with id {notificationId} not found.") : Result.Ok(new NotificationDto(notification.Message, notification.isRead));
    }

    public async Task<Result<List<NotificationDto>>> GetAllNotifications()
    {
        List<Notification> notifications = await context.Notifications
            .ToListAsync();

        return Result.Ok(notifications.Select(n => new NotificationDto(n.Message, n.isRead)).ToList());
    }

    public async Task<Result<NotificationDto>> CreateNotification(int userId, NotificationDto notificationDto)
    {
        User? foundedUser = context.Users.FirstOrDefault(u => u.Id == userId);
        Notification notification = new Notification()
        {
            Message = notificationDto.Message,
            isRead = notificationDto.IsRead,
            User = foundedUser,
        };
       
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        return Result.Ok(new NotificationDto(notification.Message, notification.isRead));
    }

    public async Task<Result<NotificationDto>> UpdateNotification(int notificationId, NotificationDto notificationDto)
    {
        Notification? existingNotification = await context.Notifications
            .FindAsync(notificationId);

        if (existingNotification == null)
        {
            return Result.Fail<NotificationDto>($"Notification with id {notificationId} not found.");
        }

        existingNotification.Message = notificationDto.Message;
        existingNotification.isRead = notificationDto.IsRead;
        await context.SaveChangesAsync();

        return Result.Ok(new NotificationDto(existingNotification.Message, existingNotification.isRead));
    }

    public async Task<Result> DeleteNotification(int notificationId)
    {
        Notification? notification = await context.Notifications
            .FindAsync(notificationId);

        if (notification == null)
        {
            return Result.Fail($"Notification with id {notificationId} not found.");
        }

        context.Notifications.Remove(notification);
        await context.SaveChangesAsync();

        return Result.Ok();
    }
}
