using System.Net.Http.Json;
using Core;
using MiniProjektGenbrug.Services.Interfaces;

namespace MiniProjektGenbrug.Services;

public class RoomServiceServer : IRoomService
{
    HttpClient _client;
    private string BaseURL = "http://localhost:5189/api/rooms";
    
    public RoomServiceServer(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<List<Room>> GetRooms()
    {
        return await _client.GetFromJsonAsync<List<Room>>($"{BaseURL}/getall");
    }

    public async Task<Room> GetRoomById(int id)
    {
        return await _client.GetFromJsonAsync<Room>($"{BaseURL}/getbyid/{id}");
    }

    public void AddRoom(Room room)
    {
        _client.PostAsJsonAsync($"{BaseURL}/addroom", room);
    }

    public void UpdateRoomById(int id, Room room)
    {
        _client.PutAsJsonAsync($"{BaseURL}/update/{id}", room);
    }

    public void DeleteRoomById(int id)
    {
        _client.DeleteAsync($"{BaseURL}/delete/{id}");
    }
}