using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnections : MonoBehaviour
{
    public int value = 10;
    public int[,] adjacencyMatrix = {{1, 0, 1, 0, 1, 0},
                                     {0, 1, 1, 1, 1, 1},
                                     {1, 1, 1, 1, 0, 0},
                                     {1, 1, 1, 1, 0, 0},
                                     {1, 1, 0, 0, 1, 0},
                                     {0, 1, 0, 0, 0, 1}};

    public void printNum()
    {
        Debug.Log(value);
    }
}