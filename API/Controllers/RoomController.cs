using API.Repositories.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    
    private IRoomRepository _repository;

    public RoomController(IRoomRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    [Route("getall")]
    public async Task<List<Room>> GetRooms()
    {
        return await _repository.GetRooms();
    }

    [HttpGet]
    [Route("getbyid/{id:int}")]
    public async Task<Room> GetRoomById(int id)
    {
        return await _repository.GetRoomById(id);
    }
    
    [HttpPost]
    [Route("add")]
    public void AddRoom(Room room)
    {
        _repository.AddRoom(room);
    }

    [HttpPut]
    [Route("update/{id:int}")]
    public void UpdateRoomById(int id, Room room)
    {
        _repository.UpdateRoomById(id, room);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public void DeleteRoomById(int id)
    {
        _repository.DeleteRoomById(id);
    }
}