using System;
using System.Security.Cryptography;
using System.Text;

public class RSAEncryption
{
    public static string EncryptVoteAsString(string plainText, string publicKey)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            byte[] encryptedBytes = rsa.Encrypt(plainBytes, true);
            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public static string DecryptVoteFromString(string encryptedText, string privateKey)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    public static (string privateKey, string publicKey) CreateAndAddKeysToStorage(int ballotId)
    {

        string ballot = ballotId.ToString();


        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true); //true argument creats a public key
        
            // Store the keys in memory
            KeyStorage.AddKey(ballot + "_private", privateKey);
            KeyStorage.AddKey(ballot + "_public", publicKey);

            return (privateKey, publicKey);
        }
    }

    public static (string privateKey, string publicKey) RetrieveKeys(int ballotId)
    {

        string ballot = ballotId.ToString();

        // Retrieve the keys from memory
        var retrievedPrivateKey = KeyStorage.GetKey(ballot + "_private");
        var retrievedPublicKey = KeyStorage.GetKey(ballot + "_public");

        return (retrievedPrivateKey, retrievedPublicKey);
    }
}