using Core;

namespace API.Repositories.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetRooms();
    Task<Room>GetRoomById(int id);
    Task<int> GetMaxRoomId();
    void AddRoom(Room room);
    void UpdateRoomById(int id, Room room);
    void DeleteRoomById(int id);
}