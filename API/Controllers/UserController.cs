using API.Repositories.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        // Validering af email
        if (string.IsNullOrEmpty(request.Email) || !request.Email.Contains("@"))
        {
            return BadRequest("Invalid email address.");
        }

        // Kontroller om brugeren allerede findes
        var existingUser = await _userRepository.GetUserByEmail(request.Email);
        if (existingUser != null)
            return BadRequest("User already exists.");

        var newUser = new User
        {
            Email = request.Email,
            Username = request.Username,
            Password = request.Password,
            Role = request.Role,
            id = await _userRepository.GetMaxUserId() + 1
        };

        // Tilføj ny bruger
        await _userRepository.AddUser(newUser);
        return Ok(newUser);
    }

    [HttpGet]
    [Route("login/{username}/{password}")]
    public async Task<User?> Login(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAndPassword(username, password);
        if (user != null)
        {
            return new User()
            {
                id = user.id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
            };
        }
        else
        {
           return new User()
            {
               
            };
        }
        
    }

    [HttpGet]
    [Route("getbyid/{id:int}")]
    public async Task<User> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    [HttpGet]
    [Route("{userId:int}/products")]
    public async Task<IActionResult> GetProductsByUserId(int userId)
    {
        var products = await _userRepository.GetProductsByUserId(userId);
        return Ok(products);
    }

    [HttpGet]
    [Route("{userId:int}/buyhistory")]
    public async Task<IActionResult> GetBuyHistoryByUserId(int userId)
    {
        var buyHistory = await _userRepository.GetBuyHistoryByUserId(userId);
        return Ok(buyHistory);
    }
}
