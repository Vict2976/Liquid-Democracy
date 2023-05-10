using System.Security.Cryptography;
using System.Text;

public class RSAEncryption
{
    public static byte[] EncryptVote(string vote, RSAParameters publicKey)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(publicKey);
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(vote);
            byte[] encryptedBytes = rsa.Encrypt(bytesToEncrypt, false);
            return encryptedBytes;
        }
    }

    public static string EncryptVoteAsString(string vote, RSAParameters publicKey)
    {
        byte[] encryptedBytes = EncryptVote(vote, publicKey);
        string encryptedString = Convert.ToBase64String(encryptedBytes);
        return encryptedString;
    }

    // Decrypt the vote using the private key
    public static string DecryptVote(byte[] encryptedVote, RSAParameters privateKey)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(privateKey);
            byte[] decryptedBytes = rsa.Decrypt(encryptedVote, false);
            string decryptedVote = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedVote;
        }
    }

    public static string DecryptVoteFromString(string encryptedVote, RSAParameters privateKey)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedVote);
        string decryptedVote = DecryptVote(encryptedBytes, privateKey);
        return decryptedVote;
    }
    public static RSAParameters GeneratePublicKey(RSAParameters privateKey)
    {
        var publicKey = new RSAParameters
        {
            Modulus = privateKey.Modulus,
            Exponent = new byte[] { 1, 0, 1 } // public exponent value is usually 65537 or {1, 0, 1}
        };
        return publicKey;
    }

    public static (RSAParameters privateKey, RSAParameters publicKey) CreateAndAddKeysToStorage(int ballotId)
    {

        string ballot = ballotId.ToString();

        using (var rsa = new RSACryptoServiceProvider())

        {
            RSAParameters privateKey = rsa.ExportParameters(true);
            var publicKey = RSAEncryption.GeneratePublicKey(privateKey);

            // Store the keys in memory
            KeyStorage.AddKey(ballot + "_private", privateKey);
            KeyStorage.AddKey(ballot + "_public", publicKey);

            return (privateKey, publicKey);
        }
    }

    public static (RSAParameters privateKey, RSAParameters publicKey) RetrieveKeys(int ballotId)
    {

        string ballot = ballotId.ToString();

        // Retrieve the keys from memory
        RSAParameters retrievedPrivateKey = KeyStorage.GetKey(ballot + "_private");
        RSAParameters retrievedPublicKey = KeyStorage.GetKey(ballot + "_public");

        return (retrievedPrivateKey, retrievedPublicKey);
    }
}

