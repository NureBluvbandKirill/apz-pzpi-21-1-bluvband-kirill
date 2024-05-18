using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class IoTProviderService(DataContext context, IConfiguration configuration) : IIoTProviderService
{
    private readonly HttpClient _httpClient = new();

    private readonly string _apiUrl = configuration.GetSection("IoT:ApiUrl")
        .Value!;

    public async Task<Result<ShipmentLocationDto?>> GetCurrentShipmentLocation(int shipmentId)
    {
        try
        {
            Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

            if (shipment == null)
                return Result.Fail<ShipmentLocationDto?>("Shipment not found");

            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentShipmentLocation?shipmentId={shipmentId}");

            if (!response.IsSuccessStatusCode)
                return Result.Fail<ShipmentLocationDto?>($"Failed to retrieve location from API. Status code: {response.StatusCode}");

            string content = await response.Content.ReadAsStringAsync();
            ShipmentLocationDto? locationDto = JsonConvert.DeserializeObject<ShipmentLocationDto>(content);

            return Result.Ok(locationDto);
        }
        catch (Exception ex)
        {
            return Result.Fail<ShipmentLocationDto?>(ex.Message);
        }
    }

    public async Task<Result<ShipmentConditionDto?>> GetCurrentShipmentCondition(int shipmentId)
    {
        try
        {
            Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

            if (shipment == null)
                return Result.Fail<ShipmentConditionDto?>("Shipment not found");

            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentShipmentCondition?shipmentId={shipmentId}");

            if (!response.IsSuccessStatusCode)
                return Result.Fail<ShipmentConditionDto?>($"Failed to retrieve condition from API. Status code: {response.StatusCode}");

            string content = await response.Content.ReadAsStringAsync();
            ShipmentConditionDto? conditionDto = JsonConvert.DeserializeObject<ShipmentConditionDto>(content);

            return Result.Ok(conditionDto);
        }
        catch (Exception ex)
        {
            return Result.Fail<ShipmentConditionDto?>(ex.Message);
        }
    }
}
