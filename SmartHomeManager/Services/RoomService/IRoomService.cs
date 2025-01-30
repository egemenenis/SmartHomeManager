using SmartHomeManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHomeManager.Services.RoomService
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int roomId);
    }
}