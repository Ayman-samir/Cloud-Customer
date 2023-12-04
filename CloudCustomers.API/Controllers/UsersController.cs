using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    //private readonly ILogger<UsersController> _logger;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    //user controller has dependancy on user service we donnt
    //need the implementaion of UserService test the beahvior of controller indepentally
   
    public async Task<IActionResult> Get() 
    {
        var users = await _userService.GetAllUsers();
        if(users.Any())
        {
            return Ok(users);
        }
        return NotFound();
    }
}
