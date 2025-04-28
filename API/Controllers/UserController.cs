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

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        // Login funktionalitet
        var user = await _userRepository.GetUserByUsernameAndPassword(request.Username, request.Password);
        if (user == null)
            return Unauthorized("Incorrect username or password.");

        return Ok(new { user.id, user.Username, user.Email, user.Role });
    }

    [HttpGet]
    [Route("getbyid/{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
            return NotFound("User not found.");
        return Ok(user);
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
