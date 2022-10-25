using Microsoft.Extensions.Caching.Memory;
using SecretsShareServer.Models;

namespace SecretsShareServer.Logic
{
    public static class GuidLogic
    {
        public static string ReturnNewGuidIfAlreadyUsed(string guidKey, IMemoryCache memoryCache)
        {
            while (memoryCache.TryGetValue(guidKey, out EncryptedDataModel result))
            {
                guidKey = Guid.NewGuid().ToString();
            }

            return guidKey;
        }
    }
}
