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
    public async Task<IActionResult> CreateUser(User user, string password)
    {
        var response = await _userService.Register(user,password);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }


    [Authorize]
    [HttpGet("GetUserById")]
    public  IActionResult GetUserById(string id)
    {
        var response =  _userService.GetById(id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(User user)
    {
        var response = _userService.UpdateUser(user);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpPut("AddAddress")]
    public IActionResult AddAddress(Address address, string userId)
    {
        var response = _userService.AddAddress(address,userId);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

}
