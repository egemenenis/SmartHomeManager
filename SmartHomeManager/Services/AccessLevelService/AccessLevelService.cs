using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Data;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.AccessLevelService
{
    public class AccessLevelService : IAccessLevelService
    {
        private readonly ApplicationDbContext _context;

        public AccessLevelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccessLevel>> GetAllAccessLevelsAsync()
        {
            return await _context.AccessLevels.ToListAsync();
        }

        public async Task<AccessLevel> GetAccessLevelByIdAsync(int accessLevelId)
        {
            return await _context.AccessLevels
                                 .FirstOrDefaultAsync(r => r.Id == accessLevelId);
        }

        public async Task<bool> UpdateDeviceAccessLevelAsync(int deviceId, int accessLevelId)
        {
            var device = await _context.Devices
                                       .Include(d => d.AccessLevel)
                                       .FirstOrDefaultAsync(d => d.Id == deviceId);

            if (device == null)
            {
                return false;
            }

            var accessLevel = await _context.AccessLevels
                                             .FirstOrDefaultAsync(a => a.Id == accessLevelId);

            if (accessLevel == null)
            {
                return false;
            }

            device.AccessLevel = accessLevel;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}