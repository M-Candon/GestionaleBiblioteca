using Microsoft.AspNetCore.Mvc;

namespace GestionaleBiblioteca.Server.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Backend OK");
    }
}