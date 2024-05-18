using FluentResults;
using Microsoft.EntityFrameworkCore;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Analytic;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class AnalyticService(DataContext context) : IAnalyticService
{
    public async Task<Result<AnalyticDto>> GetAnalyticByIdAsync(int analyticId)
    {
        Analytic? analytic = await context.Analytics
            .FindAsync(analyticId);

        return analytic == null
            ? Result.Fail<AnalyticDto>($"Analytic with id {analyticId} not found.")
            : Result.Ok(
                new AnalyticDto(
                    analytic.Metric,
                    analytic.Value,
                    analytic.Timestamp
                )
            );
    }

    public async Task<Result<List<AnalyticDto>>> GetAllAnalyticsAsync()
    {
        List<Analytic> analytics = await context.Analytics
            .ToListAsync();

        return Result.Ok(
            analytics.Select(
                    a => new AnalyticDto(
                        a.Metric,
                        a.Value,
                        a.Timestamp
                    )
                )
                .ToList()
        );
    }

    public async Task<Result<AnalyticDto>> CreateAnalyticAsync(int shipmentId, AnalyticDto analyticDto)
    {
        Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

        if (shipment is null)
        {
            return Result.Fail<AnalyticDto>($"Shipment with id {shipmentId} not found.");
        }

        Analytic analytic = new Analytic()
        {
            Metric = analyticDto.Metric,
            Value = analyticDto.Value,
            Timestamp = analyticDto.Timestamp,
            Shipment = shipment,
        };
        context.Analytics.Add(analytic);
        await context.SaveChangesAsync();

        return Result.Ok(analyticDto);
    }

    public async Task<Result<AnalyticDto>> UpdateAnalyticAsync(int analyticId, AnalyticDto analyticDto)
    {
        Analytic? existingAnalytic = await context.Analytics
            .FindAsync(analyticId);

        if (existingAnalytic == null)
        {
            return Result.Fail<AnalyticDto>($"Analytic with id {analyticId} not found.");
        }

        existingAnalytic.Metric = analyticDto.Metric;
        existingAnalytic.Value = analyticDto.Value;
        existingAnalytic.Timestamp = analyticDto.Timestamp;
        await context.SaveChangesAsync();

        return Result.Ok(analyticDto);
    }

    public async Task<Result> DeleteAnalyticAsync(int analyticId)
    {
        Analytic? analytic = await context.Analytics
            .FindAsync(analyticId);

        if (analytic == null)
        {
            return Result.Fail($"Analytic with id {analyticId} not found.");
        }

        context.Analytics.Remove(analytic);
        await context.SaveChangesAsync();

        return Result.Ok();
    }
}
