using Godot;
using System;

namespace Delve; 

public static class NodeExtensions {
    public static void GetNodeAssign<T>(this Node node, string path, out T result) {
        if (node.GetNode(path) is not T getResult)
            throw new Exception();
        result = getResult;
    }
}