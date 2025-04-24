using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IRoomService
{
    Task<List<Room>> GetRooms();
    Task<Room>GetRoomById(int id);
    void AddRoom(Room room);
    void UpdateRoomById(int id, Room room);
    void DeleteRoomById(int id);
    
}