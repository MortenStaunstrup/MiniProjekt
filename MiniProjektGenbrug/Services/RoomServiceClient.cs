using Core;
using MiniProjektGenbrug.Services.Interfaces;

namespace MiniProjektGenbrug.Services;

public class RoomServiceClient : IRoomService
{
    List<Room> rooms = new List<Room>
    {
        new Room
        {
            id = 1,
            Name = "Conference Room A",
            OpeningHour = new DateTime(2023, 10, 1, 8, 0, 0),
            ClosingHour = new DateTime(2023, 10, 1, 18, 0, 0),
            Staffing = 2
        },
        new Room
        {
            id = 2,
            Name = "Conference Room B",
            OpeningHour = new DateTime(2023, 10, 1, 9, 0, 0),
            ClosingHour = new DateTime(2023, 10, 1, 17, 0, 0),
            Staffing = 3
        },
        new Room
        {
            id = 3,
            Name = "Meeting Room A",
            OpeningHour = new DateTime(2023, 10, 1, 7, 30, 0),
            ClosingHour = new DateTime(2023, 10, 1, 19, 0, 0),
            Staffing = 1
        },
        new Room
        {
            id = 4,
            Name = "Common Room",
            OpeningHour = new DateTime(2023, 10, 1, 7, 30, 0),
            ClosingHour = new DateTime(2023, 10, 1, 19, 0, 0),
            Staffing = 1
        },
        new Room
        {
            id = 5,
            Name = "Meeting Room Z",
            OpeningHour = new DateTime(2023, 10, 1, 7, 30, 0),
            ClosingHour = new DateTime(2023, 10, 1, 19, 0, 0),
            Staffing = 1
        }
    };

    public Task<List<Room>> GetRooms()
    {
        throw new NotImplementedException();
    }

    public Task<Room> GetRoomById(int id)
    {
        throw new NotImplementedException();
    }

    public void AddRoom(Room room)
    {
        throw new NotImplementedException();
    }

    public void UpdateRoomById(int id, Room room)
    {
        throw new NotImplementedException();
    }

    public void DeleteRoomById(int id)
    {
        throw new NotImplementedException();
    }
}