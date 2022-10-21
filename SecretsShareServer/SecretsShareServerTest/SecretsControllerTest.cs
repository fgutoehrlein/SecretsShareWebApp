using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SecretsShareServer.Controllers;
using SecretsShareServer.Logic;
using SecretsShareServer.Models;

namespace SecretsShareServerTest
{
    [TestClass]
    public class SecretsControllerTest 
    {

        public void InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context)
        {
            var mcOptions = new MemoryCacheOptions();
            cache = new MemoryCache(mcOptions);

            context = new DefaultHttpContext();
            controller = new SecretsController(cache)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = context,
                }
            };
        }

        [TestMethod]
        public void Test_if_secrets_get_put_into_memory_cache()
        {
            InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context);

            var data = new SecretDataModel()
            {
                HashedInput = "someInput",
                HashedPassword = "somePassword"
            };

            var result = controller.PostSecrets(data);

            var okResult = result as OkObjectResult;

            Assert.IsTrue(result != null);
            Assert.AreEqual(200, okResult.StatusCode);

            var guidKey = okResult.Value;

            Assert.IsTrue(cache.Count == 1);
            Assert.IsTrue(cache.TryGetValue(guidKey, out SecretDataModel cacheValue));
            Assert.IsTrue(cacheValue.HashedPassword == data.HashedPassword);
            Assert.IsTrue(cacheValue.HashedInput == data.HashedInput);
        }

        [TestMethod]
        public void Test_if_the_same_guid_is_not_used_twice()
        {
            InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context);

            var data = new SecretDataModel()
            {
                HashedInput = "someInput",
                HashedPassword = "somePassword"
            };

            var result = controller.PostSecrets(data);

            var okResult = result as OkObjectResult;

            Assert.IsTrue(result != null);
            Assert.AreEqual(200, okResult.StatusCode);

            var guidKey = okResult.Value;

            Assert.IsFalse(guidKey == null);
            Assert.IsTrue(cache.Count == 1);
            Assert.IsTrue(cache.TryGetValue(guidKey, out SecretDataModel cacheValue));
            Assert.IsTrue(cacheValue.HashedPassword == data.HashedPassword);
            Assert.IsTrue(cacheValue.HashedInput == data.HashedInput);

            var newGuidKey0 = GuidLogic.ReturnNewGuidIfAlreadyUsed(guidKey.ToString(), cache);
            Assert.IsTrue(newGuidKey0 != guidKey.ToString());

            var newGuidKey1 = GuidLogic.ReturnNewGuidIfAlreadyUsed(guidKey.ToString(), cache);
            Assert.IsFalse(guidKey.ToString() == newGuidKey1);
        }

        [TestMethod]
        public void Test_if_secrets_can_be_retrieved_by_key_and_password()
        {
            InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context);

            var data = new SecretDataModel()
            {
                HashedInput = "someInput",
                HashedPassword = "somePassword"
            };
            var postOkResult = controller.PostSecrets(data) as OkObjectResult;
            Assert.AreEqual(200, postOkResult.StatusCode);

            var getOkResult = controller.GetSecret(postOkResult.Value.ToString(), data.HashedPassword) as OkObjectResult;
            Assert.AreEqual(200, getOkResult.StatusCode);

            Assert.IsTrue(getOkResult.Value.ToString() == "someInput");
        }

        [TestMethod]
        public void Test_if_secrets_can_be_retrieved_by_wrong_key_and_password()
        {
            InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context);

            var data = new SecretDataModel()
            {
                HashedInput = "someInput",
                HashedPassword = "somePassword"
            };
            var postOkResult = controller.PostSecrets(data) as OkObjectResult;
            Assert.AreEqual(200, postOkResult.StatusCode);

            var badRequestResult = controller.GetSecret(Guid.NewGuid().ToString(), data.HashedPassword) as BadRequestResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void Test_if_secrets_can_be_retrieved_by_key_and_wrong_password()
        {
            InitializeControllerAndCacheAndContext(out SecretsController controller, out MemoryCache cache, out DefaultHttpContext context);

            var data = new SecretDataModel()
            {
                HashedInput = "someInput",
                HashedPassword = "somePassword"
            };
            var postOkResult = controller.PostSecrets(data) as OkObjectResult;
            Assert.AreEqual(200, postOkResult.StatusCode);
            var badRequestResult = controller.GetSecret(postOkResult.Value.ToString(), Guid.NewGuid().ToString()) as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
    }
}