using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<bool> UpdateDeviceOwnerAsync(int deviceId, string ownerId);
    }
}