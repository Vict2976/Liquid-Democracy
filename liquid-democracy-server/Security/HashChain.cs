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
            string previousHash = _hashChain[_hashChain.Count - 1];
            string combinedInput = previousHash + input;

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

