using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SmartHomeManager.Data;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
 
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateDeviceOwnerAsync(int deviceId, string ownerId)
        {
            var device = await _context.Devices
                                       .Include(d => d.ApplicationUser)
                                       .FirstOrDefaultAsync(d => d.Id == deviceId);

            if (device == null)
            {
                return false;
            }

            var owner = await _context.Users
                                             .FirstOrDefaultAsync(a => a.Id == ownerId);

            if (owner == null)
            {
                return false;
            }

            device.ApplicationUser = owner;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}