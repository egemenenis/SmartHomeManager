using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Services.DeviceService;
using SmartHomeManager.Services.UserService;

namespace SmartHomeManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDeviceService _deviceService;

        public UserController(IDeviceService deviceService, IUserService userService)
        {
            _deviceService = deviceService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}