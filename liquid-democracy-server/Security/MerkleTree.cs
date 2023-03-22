using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class MerkleTree
{
    public List<string> _leaves;
    public List<string> _nodes;

    public MerkleTree(List<string> data)
    {
        _leaves = new List<string>(data);
        _nodes = new List<string>();
        BuildTree();
    }

    public string RootHash
    {
        get { return _nodes[0]; }
    }

    public void BuildTree()
    {
        // If there is only one leaf node, the Merkle Tree is just the root node
        if (_leaves.Count == 1)
        {
            _nodes.Add(ComputeHash(_leaves[0]));
            return;
        }

        // If there is an odd number of leaves, duplicate the last leaf to make it even
        if (_leaves.Count % 2 != 0)
        {
            _leaves.Add(_leaves[_leaves.Count - 1]);
        }

        // Create parent nodes by hashing pairs of leaf nodes
        for (int i = 0; i < _leaves.Count; i += 2)
        {
            string left = _leaves[i];
            string right = _leaves[i + 1];
            _nodes.Add(ComputeHash(left + right));
        }

        // Recursively build the rest of the tree
        _leaves = _nodes;
        _nodes = new List<string>();
        BuildTree();
    }

    public bool VerifyDataIntegrity(List<string> data, string rootHash)
    {
        // Build a Merkle Tree using the provided data
        MerkleTree merkleTree = new MerkleTree(data);

        // Verify that the computed root hash matches the provided root hash
        if (merkleTree.RootHash != rootHash)
        {
            return false;
        }

        // Verify that each leaf node matches the data it represents
        for (int i = 0; i < _leaves.Count; i++)
        {
            if (ComputeHash(_leaves[i]) != _nodes[i])
            {
                return false;
            }
        }

        // Recursively verify each parent node
        for (int i = _leaves.Count; i < _nodes.Count; i++)
        {
            string left = _nodes[2 * i - _leaves.Count];
            string right = _nodes[2 * i - _leaves.Count + 1];
            string parent = _nodes[i];

            if (ComputeHash(left + right) != parent)
            {
                return false;
            }
        }

        // All data is verified
        return true;
    }


    private string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}