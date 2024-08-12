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
    [HttpPut("UpdateUser")]
    public IActionResult UpdateUser(User user)
    {
        var response = _userService.UpdateUser(user);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("GetUserById")]
    public IActionResult GetUser(string id)
    {
        var response = _userService.GetUserById(id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpPut("AddAddress")]
    public IActionResult AddAddress(Address address, string id)
    {
        var response = _userService.AddAddress(address, id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpPut("UpdateAddress")]
    public IActionResult UpdateAddress(Address address, string id)
    {
        var response = _userService.UpdateAddress(address,id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete("DeleteAddress")]
    public IActionResult DeleteAddress(string addressId, string id)
    {
        var response = _userService.DeleteAddress(addressId, id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("GetUserCount")]
    public IActionResult getUserCount()
    {
        var response = _userService.getUserCount();

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("GetAllUsers")]
    public IActionResult getAllUsers()
    {
        var response = _userService.getAllUsers();

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("DeleteUser")]
    public IActionResult deleteUser(string id)
    {
        var response = _userService.deleteUser(id);

        if (response == null)
            return BadRequest(new { message = "Something Went wrong" });

        return Ok(response);
    }

}
