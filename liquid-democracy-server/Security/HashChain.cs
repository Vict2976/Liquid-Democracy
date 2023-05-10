using System.Text;
using System.Security.Cryptography;

namespace Security;

public class HashChain
{
    private List<string> _hashChain;

    public HashChain(string initialHash)
    {
        _hashChain = new List<string> { initialHash };
    }

    public void AddToChain(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Combine previous hash and input to form new hash input
            string previousHash = _hashChain[_hashChain.Count - 1];
            string combinedInput = previousHash + input;

            // Compute new hash and add it to the hash chain
            byte[] newHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedInput));
            string newHash = BitConverter.ToString(newHashBytes).Replace("-", "");
            _hashChain.Add(newHash);
        }
    }

    public string GetLatestHash()
    {
        return _hashChain[_hashChain.Count - 1];
    }
}

