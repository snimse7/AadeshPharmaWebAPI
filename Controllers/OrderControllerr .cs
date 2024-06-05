using AadeshPharmaWeb.Interface;

using AadeshPharmaWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;


using System.IdentityModel.Tokens.Jwt;
using AuthorizeAttribute = WebApi.Helpers.AuthorizeAttribute;
using WebApi.Models;
using WebApi.Interface;
using WebApi.Entities;
using MongoDB.Bson;

namespace AadeshPharmaWeb.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderControllerr : ControllerBase
    {
       
        
  
        private readonly IOrder _order;
        public OrderControllerr(IOrder order)
        {
            _order = order;
            

        }

        [Authorize]
        [HttpPost("/CreateOrder")]
        public IActionResult createOrder(Order order)
        {
            var response = _order.createOrder(order);
            
            if (response == null)
                return BadRequest(new { message = "Something Went wrong" });

            return Ok(response.ToJson());
        }

        [Authorize]
        [HttpGet("/GetOrderByUserId")]
        public IActionResult getOrdersByUser(string userId)
        {
            var response = _order.getOrdersByUser(userId);

            if (response == null)
                return BadRequest(new { message = "Something Went wrong" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet("/GetTotalOrderCount")]
        public IActionResult getOrderCount()
        {

            var response = _order.getOrderCount();

            if (response == null)
                return BadRequest(new { message = "Something Went wrong" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet("/getTotalAmtofOrders")]
        public IActionResult getTotalAmtofOrders()
        {

            var response = _order.getTotalAmtofOrders();

            if (response == null)
                return BadRequest(new { message = "Something Went wrong" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet("/getAllOrders")]
        public IActionResult getAllOrders()
        {

            var response = _order.getAllOrders();

            if (response == null)
                return BadRequest(new { message = "Something Went wrong" });

            return Ok(response);
        }
    }
}
