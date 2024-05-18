using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class ShipmentService(DataContext context) : IShipmentService
{
    public Result<List<Shipment>> GetAllShipments()
    {
        try
        {
            List<Shipment> shipments = context.Shipments
                .Include(s => s.StartLocation)
                .Include(s => s.EndLocation)
                .Include(s => s.ShipmentCondition)
                .ToList();
            return Result.Ok(shipments);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<Shipment>>(ex.Message);
        }
    }

    public Result<Shipment> GetShipmentById(int id)
    {
        try
        {
            Shipment? shipment = context.Shipments
                .Include(s => s.StartLocation)
                .Include(s => s.EndLocation)
                .Include(s => s.ShipmentCondition)
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);

            return shipment == null
                ? Result.Fail<Shipment>("Shipment not found")
                : Result.Ok(shipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<Shipment>(ex.Message);
        }
    }

    public Result<CreateShipmentDto> CreateShipment(int userId, CreateShipmentDto shipment)
    {
        try
        {
            EntityEntry<Location> startLocation = context.Locations.Add(new Location()
            {
                Latitude = shipment.StartLocationLatitude,
                Longitude = shipment.StartLocationLongitude,
            });
            
            EntityEntry<Location> endLocation = context.Locations.Add(new Location()
            {
                Latitude = shipment.EndLocationLatitude,
                Longitude = shipment.EndLocationLongitude,
            });

            EntityEntry<ShipmentCondition> shipmentCondition = context.ShipmentConditions.Add(new ShipmentCondition()
            {
                MinTemperature = shipment.MinTemperature,
                MaxTemperature = shipment.MaxTemperature,
                MinHumidity = shipment.MinHumidity,
                MaxHumidity = shipment.MaxHumidity,
            });
            
            User? foundedUser = context.Users.FirstOrDefault(u => u.Id == userId);
            
            context.Shipments.Add(new Shipment()
            {
                StartDate = shipment.StartDate,
                StartLocation = startLocation.Entity,
                EndDate = shipment.EndDate,
                EndLocation = endLocation.Entity,
                ShipmentCondition = shipmentCondition.Entity,
                Status = ShipmentStatus.Pending,
                User = foundedUser,
            });
            context.SaveChanges();
            return Result.Ok(shipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<CreateShipmentDto>(ex.Message);
        }
    }

    public Result<UpdateShipmentDto> UpdateShipment(UpdateShipmentDto updatedShipment)
    {
        try
        {
            Shipment? existingShipment = context.Shipments
                .Include(s => s.StartLocation)
                .Include(s => s.EndLocation)
                .Include(s => s.ShipmentCondition)
                .FirstOrDefault(s => s.Id == updatedShipment.Id);
        
            if (existingShipment == null)
                return Result.Fail<UpdateShipmentDto>("Shipment not found");

            existingShipment.StartDate = updatedShipment.StartDate;
            existingShipment.StartLocation.Latitude = updatedShipment.StartLocationLatitude;
            existingShipment.StartLocation.Longitude = updatedShipment.StartLocationLongitude;
            existingShipment.EndDate = updatedShipment.EndDate;
            existingShipment.EndLocation.Latitude = updatedShipment.EndLocationLatitude;
            existingShipment.EndLocation.Longitude = updatedShipment.EndLocationLongitude;
            existingShipment.ShipmentCondition.MinTemperature = updatedShipment.MinTemperature;
            existingShipment.ShipmentCondition.MaxTemperature = updatedShipment.MaxTemperature;
            existingShipment.ShipmentCondition.MinHumidity = updatedShipment.MinHumidity;
            existingShipment.ShipmentCondition.MaxHumidity = updatedShipment.MaxHumidity;
            existingShipment.Status = updatedShipment.Status;
            
            context.SaveChanges();
        
            return Result.Ok(updatedShipment);
        }
        catch (Exception ex)
        {
            return Result.Fail<UpdateShipmentDto>(ex.Message);
        }
    }

    public Result DeleteShipment(int id)
    {
        try
        {
            Shipment? shipmentToDelete = context.Shipments.Find(id);

            if (shipmentToDelete == null)
                return Result.Fail("Shipment not found");

            context.Shipments.Remove(shipmentToDelete);
            context.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public List<Shipment> GetShipmentsByStatus(ShipmentStatus status)
    {
        return context.Shipments.Where(s => s.Status == status).ToList();
    }
}
