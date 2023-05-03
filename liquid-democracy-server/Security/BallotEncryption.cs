using System;
using System.Security.Cryptography;
using System.Text;

public class BallotEncryption
{
    public static byte[] Encrypt(byte[] data, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = new byte[16];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }
                return memoryStream.ToArray();
            }
        }
    }

    public static string Decrypt(string encryptedData, string key) {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
        using (Aes aes = Aes.Create()) {
            aes.Key = keyBytes;
            aes.IV = new byte[16];
            using (MemoryStream memoryStream = new MemoryStream(encryptedBytes)) {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read)) {
                    using (StreamReader streamReader = new StreamReader(cryptoStream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}