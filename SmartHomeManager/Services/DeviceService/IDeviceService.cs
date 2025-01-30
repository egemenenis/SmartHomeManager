using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.DeviceService
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task ToggleDeviceStatusAsync(int deviceId);
        Task SetThermostatTemperatureAsync(int thermostatId, int temperature);
        Task ToggleCameraRecordingAsync(int cameraId);
        Task ToggleDoorLockAsync(int doorLockId);
        Task AddDeviceAsync(Device device);
        Task CreateCameraAsync(Camera camera);
        Task UpdateCameraAsync(int id, Camera updatedCamera);
        Task DeleteCameraAsync(int id);
        Task CreateDoorLockAsync(DoorLock doorLock);
        Task UpdateDoorLockAsync(int id, DoorLock updatedDoorLock);
        Task DeleteDoorLockAsync(int id);
        Task CreateSensorAsync(Sensor sensor);
        Task UpdateSensorAsync(int id, Sensor updatedSensor);
        Task DeleteSensorAsync(int id);
        Task CreateThermostatAsync(Thermostat thermostat);
        Task UpdateThermostatAsync(int id, Thermostat updatedThermostat);
        Task DeleteThermostatAsync(int id);
        Task<bool> ToggleTwoFactorAuthAsync(int deviceId, int status);
    }
}