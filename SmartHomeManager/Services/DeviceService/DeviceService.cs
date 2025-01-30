using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SmartHomeManager.Data;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;
 
        public DeviceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(d => d.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleDeviceStatusAsync(int deviceId)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device != null)
            {
                device.IsOn = !device.IsOn;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SetThermostatTemperatureAsync(int thermostatId, int temperature)
        {
            var thermostat = await _context.Thermostats.FindAsync(thermostatId);
            if (thermostat != null)
            {
                thermostat.Temperature = temperature;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleCameraRecordingAsync(int cameraId)
        {
            var camera = await _context.Cameras.FindAsync(cameraId);
            if (camera != null)
            {
                camera.IsRecording = !camera.IsRecording;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleDoorLockAsync(int doorLockId)
        {
            var doorLock = await _context.DoorLocks.FindAsync(doorLockId);
            if (doorLock != null)
            {
                doorLock.IsLocked = !doorLock.IsLocked;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCameraAsync(Camera camera)
        {
            _context.Cameras.Add(camera);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCameraAsync(int id, Camera updatedCamera)
        {
            var camera = await _context.Cameras.FindAsync(id);
            if (camera != null)
            {
                camera.Name = updatedCamera.Name;
                camera.Type = updatedCamera.Type;
                camera.ApplicationUser = updatedCamera.ApplicationUser;
                camera.IsOn = updatedCamera.IsOn;
                camera.IsRecording = updatedCamera.IsRecording;
                camera.RoomId = updatedCamera.RoomId;
                camera.DeviceType = updatedCamera.DeviceType;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCameraAsync(int id)
        {
            var camera = await _context.Cameras.FindAsync(id);
            if (camera != null)
            {
                _context.Cameras.Remove(camera);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateDoorLockAsync(DoorLock doorLock)
        {
            _context.DoorLocks.Add(doorLock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoorLockAsync(int id, DoorLock updatedDoorLock)
        {
            var doorLock = await _context.DoorLocks.FindAsync(id);
            if (doorLock != null)
            {
                doorLock.Name = updatedDoorLock.Name;
                doorLock.Type = updatedDoorLock.Type;
                doorLock.ApplicationUser = updatedDoorLock.ApplicationUser;
                doorLock.IsOn = updatedDoorLock.IsOn;
                doorLock.IsLocked = updatedDoorLock.IsLocked;
                doorLock.RoomId = updatedDoorLock.RoomId;
                doorLock.DeviceType = updatedDoorLock.DeviceType;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDoorLockAsync(int id)
        {
            var doorLock = await _context.DoorLocks.FindAsync(id);
            if (doorLock != null)
            {
                _context.DoorLocks.Remove(doorLock);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateSensorAsync(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSensorAsync(int id, Sensor updatedSensor)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                sensor.Name = updatedSensor.Name;
                sensor.Type = updatedSensor.Type;
                sensor.ApplicationUser = updatedSensor.ApplicationUser;
                sensor.IsOn = updatedSensor.IsOn;
                sensor.IsActive = updatedSensor.IsActive;
                sensor.RoomId = updatedSensor.RoomId;
                sensor.DeviceType = updatedSensor.DeviceType;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSensorAsync(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateThermostatAsync(Thermostat thermostat)
        {
            _context.Thermostats.Add(thermostat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateThermostatAsync(int id, Thermostat updatedThermostat)
        {
            var thermostat = await _context.Thermostats.FindAsync(id);
            if (thermostat != null)
            {
                thermostat.Name = updatedThermostat.Name;
                thermostat.Type = updatedThermostat.Type;
                thermostat.ApplicationUser = updatedThermostat.ApplicationUser;
                thermostat.IsOn = updatedThermostat.IsOn;
                thermostat.Temperature = updatedThermostat.Temperature;
                thermostat.RoomId = updatedThermostat.RoomId;
                thermostat.DeviceType = updatedThermostat.DeviceType;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteThermostatAsync(int id)
        {
            var thermostat = await _context.Thermostats.FindAsync(id);
            if (thermostat != null)
            {
                _context.Thermostats.Remove(thermostat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ToggleTwoFactorAuthAsync(int deviceId, int status)
        {
            var device = await _context.Devices.FindAsync(deviceId);

            if (device == null)
            {
                return false;
            }

            device.IsTwoFactorEnabled = status == 1;

            _context.Devices.Update(device);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}