using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;
using SmartHomeManager.Services.AccessLevelService;
using SmartHomeManager.Services.DeviceService;
using SmartHomeManager.Services.RoomService;
using SmartHomeManager.Services.UserService;

public class DeviceController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly IRoomService _roomService;
    private readonly IAccessLevelService _accessLevelService;
    private readonly IUserService _userService;

    public DeviceController(IDeviceService deviceService, IRoomService roomService, IAccessLevelService accessLevelService, IUserService userService)
    {
        _deviceService = deviceService;
        _roomService = roomService;
        _accessLevelService = accessLevelService;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var devices = await _deviceService.GetAllDevicesAsync();

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        return View(devices);
    }

    public async Task<IActionResult> Details(int id)
    {
        var devices = await _deviceService.GetDeviceByIdAsync(id);

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        var accessLevels = await _accessLevelService.GetAllAccessLevelsAsync();
        ViewBag.AccessLevels = accessLevels;

        var owners = await _userService.GetAllUsersAsync();
        ViewBag.Users = owners;

        return View(devices);
    }

    public async Task<IActionResult> Manage(int id)
    {
        var device = await _deviceService.GetDeviceByIdAsync(id);
        if (device == null)
        {
            return NotFound();
        }
        return View(device);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleDevice(int id)
    {
        await _deviceService.ToggleDeviceStatusAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CreateCamera()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        var owners = await _userService.GetAllUsersAsync();
        ViewBag.Users = owners;

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> CreateCamera(Camera camera)
    {
        if (ModelState.IsValid)
        {
            await _deviceService.CreateCameraAsync(camera);
            return RedirectToAction("Index");
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

        foreach (var error in errors)
        {
            Console.WriteLine("Model Error: " + error);
        }

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;
        return View(camera);
    }

    public async Task<IActionResult> CreateThermostat()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        var owners = await _userService.GetAllUsersAsync();
        ViewBag.Users = owners;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateThermostat(Thermostat thermostat)
    {
        if (ModelState.IsValid)
        {
            await _deviceService.CreateThermostatAsync(thermostat);
            return RedirectToAction("Index");
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

        foreach (var error in errors)
        {
            Console.WriteLine("Model Error: " + error);
        }

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;
        return View(thermostat);
    }

    public async Task<IActionResult> CreateDoorLock()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        var owners = await _userService.GetAllUsersAsync();
        ViewBag.Users = owners;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoorLock(DoorLock doorLock)
    {
        if (ModelState.IsValid)
        {
            await _deviceService.CreateDoorLockAsync(doorLock);
            return RedirectToAction("Index");
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

        foreach (var error in errors)
        {
            Console.WriteLine("Model Error: " + error);
        }

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;
        return View(doorLock);
    }

    public async Task<IActionResult> CreateSensor()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;

        var owners = await _userService.GetAllUsersAsync();
        ViewBag.Users = owners;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSensor(Sensor sensor)
    {
        if (ModelState.IsValid)
        {
            await _deviceService.CreateSensorAsync(sensor);
            return RedirectToAction("Index");
        }


        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

        foreach (var error in errors)
        {
            Console.WriteLine("Model Error: " + error);
        }

        var rooms = await _roomService.GetAllRoomsAsync();
        ViewBag.Rooms = rooms;
        return View(sensor);
    }


    [HttpPost]
    public async Task<IActionResult> ToggleDeviceStatus(int deviceId)
    {
        await _deviceService.ToggleDeviceStatusAsync(deviceId);
        return RedirectToAction("Index", "Device");
    }

    public IActionResult CreateDevice()
    {
        return View();
    }
}