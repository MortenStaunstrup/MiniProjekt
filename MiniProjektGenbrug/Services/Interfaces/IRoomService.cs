using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IRoomService
{
    List<Room> GetRooms();
    Room GetRoomById(int id);
    
}