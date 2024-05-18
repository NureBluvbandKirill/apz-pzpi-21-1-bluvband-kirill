using FluentResults;
using ThermoTsev.Backend.Domain.DTO.Analytic;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IAnalyticService
{
    Task<Result<AnalyticDto>> GetAnalyticByIdAsync(int analyticId);

    Task<Result<List<AnalyticDto>>> GetAllAnalyticsAsync();

    Task<Result<AnalyticDto>> CreateAnalyticAsync(int shipmentId, AnalyticDto analyticDto);

    Task<Result<AnalyticDto>> UpdateAnalyticAsync(int analyticId, AnalyticDto analyticDto);

    Task<Result> DeleteAnalyticAsync(int analyticId);
}
