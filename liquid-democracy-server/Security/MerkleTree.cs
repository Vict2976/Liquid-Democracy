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