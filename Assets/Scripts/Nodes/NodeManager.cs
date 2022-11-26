using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public int[] nodeList = new int[] {0,1,2,3,4};
    public List<List<int>> connections = new List<List<int>>();

    void Awake()
    {
        // add connections
        connections.Add(new List<int> {0,1,2,4});
        connections.Add(new List<int> {0,1,2,3});
        connections.Add(new List<int> {0,1,2,3});
        connections.Add(new List<int> {1,2,3,4});
        connections.Add(new List<int> {0,3,4});
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


