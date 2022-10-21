using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SecretsShareServer.Logic;
using SecretsShareServer.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace SecretsShareServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly MemoryCache _memoryCache;
        public SecretsController(MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("/Secret")]
        public IActionResult PostSecrets([FromBody]SecretDataModel data)
        {
            if (data == null)
            {
                return BadRequest();
            }

            var guidKey = Guid.NewGuid().ToString();
            guidKey = GuidLogic.ReturnNewGuidIfAlreadyUsed(guidKey, _memoryCache);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _memoryCache.Set(guidKey, data, cacheEntryOptions);

            return Ok(guidKey);
        }


        [HttpGet]
        [Route("Secret")]
        public IActionResult GetSecret([FromHeader(Name = "Guid-Key")][Required] string guidKey, [FromHeader(Name = "Hashed-Guid-Password")][Required] string hashedGuidPassword)
        {
            if (string.IsNullOrEmpty(guidKey) || string.IsNullOrEmpty(hashedGuidPassword))
            {
                return BadRequest();
            }

            if(!_memoryCache.TryGetValue(guidKey, out SecretDataModel data))
            {
                return BadRequest();
            }

            if(data.HashedPassword != hashedGuidPassword) 
            {
                return BadRequest();
            }

            return Ok(data.HashedInput);
        }
    }
}
