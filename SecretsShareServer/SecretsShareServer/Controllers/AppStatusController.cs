using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SecretsShareServer.Logic;
using SecretsShareServer.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;

namespace SecretsShareServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppStatusController : ControllerBase
    {
        private readonly MemoryCache _memoryCache;
        public AppStatusController(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("/Ready")]
        public IActionResult GetReadiness()
        {
            if(_memoryCache == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("/Healthy")]
        public IActionResult GetHealthieness()
        {
            return Ok();
        }
    }
}
