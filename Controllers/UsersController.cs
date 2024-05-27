namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
 public async  Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    //[Authorize]
    //[HttpGet]
    //public IActionResult GetAll()
    //{
    //    var users = _userService.GetAll();
    //    return Ok(users);
    //}

    [HttpPost("createUser")]
    public IActionResult CreateUser(User user, string password)
    {
        var response = _userService.Register(user,password);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }
}
