using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Data;
using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms
                                 .FirstOrDefaultAsync(r => r.Id == roomId);
        }
    }
}