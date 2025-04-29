using API.Models;
using API.Repositories.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    private IProductRepository _productRepository;

    public UserController(IUserRepository userRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {

        if (string.IsNullOrEmpty(request.Email) || !request.Email.Contains("@"))
        {
            return BadRequest("Invalid email address.");
        }
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
        
        await _userRepository.AddUser(newUser);
        return Ok(newUser);
    }

    [HttpGet("login/{username}/{password}")]
    public async Task<ActionResult<User>> Login(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAndPassword(username, password);
        if (user == null)
        {
            return NotFound("Bruger ikke fundet.");
        }

        return Ok(new User
        {
            id = user.id,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role
        });
    }


    [HttpGet]
    [Route("{id:int}")]
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
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        var existingUser = await _userRepository.GetUserById(user.id);
        if (existingUser == null)
            return NotFound("User not found.");

        await _userRepository.UpdateUser(user);
        return Ok(user);
    }
    [HttpPost("{userId}/addToCart")]
    public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartRequest data)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
            return NotFound("User not found.");

        var product = await _productRepository.GetProductById(data.productId);
        if (product == null)
            return NotFound("Product not found.");
        
        var existingItem = user.Cart?.FirstOrDefault(ci => ci.ProductId == product.id);
        if (existingItem != null)
        {
            existingItem.antal += 1;
        }
        else
        {
            user.Cart.Add(new CartItem
            {
                UserId = userId,
                ProductId = product.id,
                Product = product,
                antal = 1
            });
        }

        await _userRepository.UpdateUser(user);

        return Ok(user.Cart);
    }


}
