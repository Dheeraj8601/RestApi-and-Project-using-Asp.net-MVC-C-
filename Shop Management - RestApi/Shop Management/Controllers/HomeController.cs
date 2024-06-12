using Microsoft.AspNetCore.Mvc;
using Shop_Management.Models;
using System.Diagnostics;

namespace Shop_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

       

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the API!");
        }

        [HttpGet("privacy")]
        public IActionResult GetPrivacy()
        {
            return Ok("This is the privacy policy of the API.");
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            return NotFound();
        }
    }
}
