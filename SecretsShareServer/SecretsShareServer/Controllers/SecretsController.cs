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
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers","*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin","*");

            if (data == null)
            {
                return BadRequest();
            }
            
            var guidKey = Guid.NewGuid().ToString();
            guidKey = GuidLogic.ReturnNewGuidIfAlreadyUsed(guidKey, _memoryCache);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            byte[] bytePassword = System.Text.Encoding.UTF8.GetBytes(data.Password);
            var byteHashedPassword = SHA256.HashData(bytePassword);
            var stringHashedPassword = Convert.ToBase64String(byteHashedPassword);

            var encryptedData = new EncryptedDataModel()
            {
                HashedPassword = stringHashedPassword,
                EncryptedSecretInput = EncryptionLogic.EncryptString(data.Password, data.SecretInput) 
            };

            _memoryCache.Set(guidKey, encryptedData, cacheEntryOptions);
            return Ok(guidKey);
        }

        [HttpOptions]
        [Route("/Secret")]
        public IActionResult OptionsSecret()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers","*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin","*");
            return Ok();
        }

        [HttpGet]
        [Route("/Secret")]
        public IActionResult GetSecret([FromHeader(Name = "Guid-Key")][Required] string guidKey, [FromHeader(Name = "Hashed-Guid-Password")][Required] string password)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers","*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin","*");

            if (string.IsNullOrEmpty(guidKey) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }

            if(!_memoryCache.TryGetValue(guidKey, out EncryptedDataModel data))
            {
                return BadRequest();
            }

            byte[] bytePassword = System.Text.Encoding.UTF8.GetBytes(password);
            var byteHashedPassword = SHA256.HashData(bytePassword);
            var stringHashedPassword = Convert.ToBase64String(byteHashedPassword);

            if (data.HashedPassword != stringHashedPassword) 
            {
                return BadRequest();
            }

            var decryptedInput = EncryptionLogic.DecryptString(password, data.EncryptedSecretInput);

            return Ok(decryptedInput);
        }
    }
}
