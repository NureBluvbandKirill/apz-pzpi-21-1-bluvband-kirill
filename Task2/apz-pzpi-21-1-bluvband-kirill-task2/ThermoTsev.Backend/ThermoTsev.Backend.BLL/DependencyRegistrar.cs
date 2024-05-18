﻿using Microsoft.Extensions.DependencyInjection;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.BLL.Services;

namespace ThermoTsev.Backend.BLL;

public static class DependencyRegistrar
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IShipmentService, ShipmentService>();
        services.AddScoped<IStatisticService, StatisticService>();
        services.AddScoped<IIoTProviderService, IoTProviderService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IAnalyticService, AnalyticService>();
    }
}
