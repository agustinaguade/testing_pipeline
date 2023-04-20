using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebApi.Controllers;
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
        return Ok("all green, all good");
    }
}