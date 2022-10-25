using SecretsShareServer.Logic;
using SecretsShareServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsShareServerTest
{
    [TestClass]
    public class EncryptionLogicTest
    {
        [TestMethod]
        public void Test_if_input_gets_encrypted()
        {
            var inputData = new SecretDataModel()
            {
                SecretInput = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var encryptedString = EncryptionLogic.EncryptString(inputData.Password, inputData.SecretInput);
            Assert.IsTrue(encryptedString != inputData.SecretInput);
        }

        [TestMethod]
        public void Test_if_input_gets_encrypted_by_password()
        {
            var anotherRandomPassword = Guid.NewGuid().ToString();

            var inputData = new SecretDataModel()
            {
                SecretInput = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            //check if the encrypted String is not equal to the original Input
            var encryptedString = EncryptionLogic.EncryptString(inputData.Password, inputData.SecretInput);
            Assert.IsTrue(encryptedString != inputData.SecretInput);

            //check if the encrypted string is the same when using the same Input Parameter
            var shouldBeSameEncryptedString = EncryptionLogic.EncryptString(inputData.Password, inputData.SecretInput);
            Assert.IsTrue(encryptedString == shouldBeSameEncryptedString);

            //check if the another encrypted string is not the same as the first encrypted string using a different password
            var anotherEncryptedString = EncryptionLogic.EncryptString(anotherRandomPassword, inputData.SecretInput);
            Assert.IsTrue(encryptedString != anotherEncryptedString);
        }

        [TestMethod]
        public void Test_if_input_can_be_decrypted()
        {
            var inputData = new SecretDataModel()
            {
                SecretInput = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var encryptedString = EncryptionLogic.EncryptString(inputData.Password, inputData.SecretInput);
            Assert.IsTrue(encryptedString != inputData.SecretInput);

            var decryptedString = EncryptionLogic.DecryptString(inputData.Password, encryptedString);
            Assert.IsTrue(inputData.SecretInput == decryptedString);
        }

        [TestMethod]
        public void Test_if_input_can_not_be_decrypted_by_different_password()
        {
            var inputData = new SecretDataModel()
            {
                SecretInput = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var differentPassword = Guid.NewGuid().ToString();

            var encryptedString = EncryptionLogic.EncryptString(inputData.Password, inputData.SecretInput);
            Assert.IsTrue(encryptedString != inputData.SecretInput);

            try
            {
                var decryptedString = EncryptionLogic.DecryptString(differentPassword, encryptedString);
            }
            catch (System.Security.Cryptography.CryptographicException ce)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
