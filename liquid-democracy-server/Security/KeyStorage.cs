using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

public static class KeyStorage
{
    public static readonly ConcurrentDictionary<string, RSAParameters> keyDictionary = new ConcurrentDictionary<string, RSAParameters>();

    public static void AddKey(string keyId, RSAParameters key)
    {
        if (!keyDictionary.TryAdd(keyId, key))
        {
            throw new Exception($"Key with id {keyId} already exists");
        }
    }

    public static RSAParameters GetKey(string keyId)
    {
        if (!keyDictionary.TryGetValue(keyId, out RSAParameters key))
        {
            throw new Exception($"Key with id {keyId} not found");
        }
        return key;
    }
}