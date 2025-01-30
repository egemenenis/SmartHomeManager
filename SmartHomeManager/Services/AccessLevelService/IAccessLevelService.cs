using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartHomeManager.Services.AccessLevelService
{
    public interface IAccessLevelService
    {
        Task<IEnumerable<AccessLevel>> GetAllAccessLevelsAsync();
        Task<AccessLevel> GetAccessLevelByIdAsync(int accessLevelId);
        Task<bool> UpdateDeviceAccessLevelAsync(int deviceId, int accessLevelId);
    }
}