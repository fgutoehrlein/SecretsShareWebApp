namespace SecretsShareServer.Models
{
    public class EncryptedDataModel
    {
        public string HashedPassword { get; set; }
        public string EncryptedSecretInput { get; set; }
    }
}
