using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

public static class KeyStorage
{
    public static readonly ConcurrentDictionary<string, string> keyDictionary = new ConcurrentDictionary<string, string>();

    public static void AddKey(string keyId, string key)
    {
        if (!keyDictionary.TryAdd(keyId, key))
        {
            throw new Exception($"Key with id {keyId} already exists");
        }
    }

    public static string GetKey(string keyId)
    {
        if (!keyDictionary.TryGetValue(keyId, out string key))
        {
            throw new Exception($"Key with id {keyId} not found");
        }
        return key;
    }
}