using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;
using SmartHomeManager.Services.AccessLevelService;
using SmartHomeManager.Services.DeviceService;
using SmartHomeManager.Services.EmailService;
using SmartHomeManager.Services.UserService;
using System.Threading.Tasks;

namespace SmartHomeManager.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IEmailService _emailService;
        private readonly IAccessLevelService _accessLevelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public SecurityController(IDeviceService deviceService, IEmailService emailService, IAccessLevelService accessLevelService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _deviceService = deviceService;
            _emailService = emailService;
            _accessLevelService = accessLevelService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var cameras = await _deviceService.GetAllDevicesAsync();
            var accessLevels = await _accessLevelService.GetAllAccessLevelsAsync();
            var users = await _userService.GetAllUsersAsync();

            ViewBag.AccessLevels = accessLevels;
            ViewBag.Users = users;

            return View(cameras);
        }

        public async Task<IActionResult> Details(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleDeviceStatus(int deviceId)
        {
            await _deviceService.ToggleDeviceStatusAsync(deviceId);
            return RedirectToAction("Index", "Security");
        }

        [HttpPost]
        public async Task<IActionResult> ReportDevice(int deviceId)
        {
            var device = await _deviceService.GetDeviceByIdAsync(deviceId);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] MailRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ReceiverEmail))
            {
                return Json(new { success = false, message = "Invalid request or missing email address." });
            }

            try
            {
                bool emailSent = await _emailService.SendEmailAsync(request.Subject, request.Body, request.ReceiverEmail);

                if (emailSent)
                {
                    return Json(new { success = true, message = "Email sent successfully!" });
                    }
                else
                {
                    return Json(new { success = false, message = "Email could not be sent!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while sending an email: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleTwoFactorAuth(int deviceId, bool status)
        {
            bool result = await _deviceService.ToggleTwoFactorAuthAsync(deviceId, status ? 1 : 0);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update Two-Factor Authentication.";
                return RedirectToAction("Index"); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetDeviceAccessLevel(int deviceId, int accessLevelId)
        {
            bool result = await _accessLevelService.UpdateDeviceAccessLevelAsync(deviceId, accessLevelId);

            if (result)
            {
                TempData["SuccessMessage"] = "Device access level updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update device access level.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetDeviceOwner(int deviceId, string ownerId)
        {
            bool result = await _userService.UpdateDeviceOwnerAsync(deviceId, ownerId);

            if (result)
            {
                TempData["SuccessMessage"] = "Device owner updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update device owner.";
            }

            return RedirectToAction("Index");
        }
    }
}