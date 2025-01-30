using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;
using SmartHomeManager.Services.DeviceService;
using SmartHomeManager.Services.RoomService;
using SmartHomeManager.Services.UserService;

namespace SmartHomeManager.Controllers
{
    public class ManageController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public ManageController(IDeviceService deviceService, IRoomService roomService, IUserService userService)
        {
            _deviceService = deviceService;
            _roomService = roomService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var cameras = await _deviceService.GetAllDevicesAsync();
            return View(cameras);
        }

        [HttpPost]
        public async Task<IActionResult> GetDeviceByIdAsync(int id)
        {
            await _deviceService.GetDeviceByIdAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleDeviceStatus(int deviceId)
        {
            await _deviceService.ToggleDeviceStatusAsync(deviceId);
            return RedirectToAction("Index", "Manage");
        }

        public async Task<IActionResult> UpdateCamera(int id)
        {
            var camera = await _deviceService.GetDeviceByIdAsync(id) as Camera;
            if (camera == null)
            {
                return NotFound();
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewBag.Rooms = rooms;

            var owners = await _userService.GetAllUsersAsync();
            ViewBag.Users = owners;

            return View("Camera/UpdateCamera", camera);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCamera(int id, Camera updatedCamera)
        {
            if (ModelState.IsValid)
            {
                await _deviceService.UpdateCameraAsync(id, updatedCamera);
                return RedirectToAction("Index", "Manage");
            }

            return View(updatedCamera);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCamera(int id)
        {
            var camera = await _deviceService.GetDeviceByIdAsync(id) as Camera;
            if (camera == null)
            {
                return NotFound();
            }

            return View("Camera/DeleteCamera", camera);
        }

        [HttpPost, ActionName("DeleteCamera")]
        public async Task<IActionResult> DeleteCameraConfirmed(int id)
        {
            var camera = await _deviceService.GetDeviceByIdAsync(id) as Camera;
            if (camera == null)
            {
                return NotFound();
            }

            await _deviceService.DeleteCameraAsync(id);

            return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDoorLock(int id)
        {
            var doorLock = await _deviceService.GetDeviceByIdAsync(id) as DoorLock;
            if (doorLock == null)
            {
                return NotFound();
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewBag.Rooms = rooms;

            var owners = await _userService.GetAllUsersAsync();
            ViewBag.Users = owners;

            return View("DoorLock/UpdateDoorLock", doorLock);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoorLock(int id, DoorLock updatedDoorLock)
        {
            if (ModelState.IsValid)
            {
                await _deviceService.UpdateDoorLockAsync(id, updatedDoorLock);
                return RedirectToAction("Index", "Manage");
            }

            return View(updatedDoorLock);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDoorLock(int id)
        {
            var doorLock = await _deviceService.GetDeviceByIdAsync(id) as DoorLock;
            if (doorLock == null)
            {
                return NotFound();
            }

            return View("DoorLock/DeleteDoorlock", doorLock);
        }

        [HttpPost, ActionName("DeleteDoorLock")] 
        public async Task<IActionResult> DeleteDoorLockConfirmed(int id)
        {
            var doorLock = await _deviceService.GetDeviceByIdAsync(id) as DoorLock;
            if (doorLock == null)
            {
                return NotFound();
            }


            await _deviceService.DeleteDoorLockAsync(id);

            return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSensor(int id)
        {
            var sensor = await _deviceService.GetDeviceByIdAsync(id) as Sensor;
            if (sensor == null)
            {
                return NotFound();
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewBag.Rooms = rooms;

            var owners = await _userService.GetAllUsersAsync();
            ViewBag.Users = owners;

            return View("Sensor/UpdateSensor", sensor);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSensor(int id, Sensor updatedSensor)
        {
            if (ModelState.IsValid)
            {
                await _deviceService.UpdateSensorAsync(id, updatedSensor);
                return RedirectToAction("Index", "Manage");
            }

            return View(updatedSensor);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _deviceService.GetDeviceByIdAsync(id) as Sensor;
            if (sensor == null)
            {
                return NotFound();
            }

            return View("Sensor/DeleteSensor", sensor);
        }

        [HttpPost, ActionName("DeleteSensor")]
        public async Task<IActionResult> DeleteSensorConfirmed(int id)
        {
            var sensor = await _deviceService.GetDeviceByIdAsync(id) as Sensor;
            if (sensor == null)
            {
                return NotFound();
            }

            await _deviceService.DeleteSensorAsync(id);

            return RedirectToAction("Index", "Manage");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateThermostat(int id)
        {
            var thermostat = await _deviceService.GetDeviceByIdAsync(id) as Thermostat;
            if (thermostat == null)
            {
                return NotFound();
            }

            var rooms = await _roomService.GetAllRoomsAsync();
            ViewBag.Rooms = rooms;

            var owners = await _userService.GetAllUsersAsync();
            ViewBag.Users = owners;

            return View("Thermostat/UpdateThermostat", thermostat);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateThermostat(int id, Thermostat updatedThermostat)
        {
            if (ModelState.IsValid)
            {
                await _deviceService.UpdateThermostatAsync(id, updatedThermostat);
                return RedirectToAction("Index", "Manage");
            }
            return View(updatedThermostat);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteThermostat(int id)
        {
            var thermostat = await _deviceService.GetDeviceByIdAsync(id) as Thermostat;
            if (thermostat == null)
            {
                return NotFound();
            }

            return View("Thermostat/DeleteThermostat", thermostat);
        }

        [HttpPost, ActionName("DeleteThermostat")]
        public async Task<IActionResult> DeleteThermostatConfirmed(int id)
        {
            var thermostat = await _deviceService.GetDeviceByIdAsync(id) as Thermostat;
            if (thermostat == null)
            {
                return NotFound();
            }

            await _deviceService.DeleteThermostatAsync(id);

            return RedirectToAction("Index", "Manage");
        }
    }
}