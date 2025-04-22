namespace Core;

public class User
{
    public int id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Rmail { get; set; }
    public string Role { get; set; }
    public List<Product> Products { get; set; }
    public List<Product> BuyerHistory { get; set; }
}