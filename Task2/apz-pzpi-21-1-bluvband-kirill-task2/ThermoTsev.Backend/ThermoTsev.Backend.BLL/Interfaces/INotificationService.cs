using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Notification;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface INotificationService
{
    Task<Result<NotificationDto>> GetNotificationById(int notificationId);
    
    Task<Result<List<NotificationDto>>> GetAllNotifications();
    
    Task<Result<NotificationDto>> CreateNotification(int userId, NotificationDto notificationDto);
    
    Task<Result<NotificationDto>> UpdateNotification(int notificationId, NotificationDto notificationDto);
    
    Task<Result> DeleteNotification(int notificationId);
}
