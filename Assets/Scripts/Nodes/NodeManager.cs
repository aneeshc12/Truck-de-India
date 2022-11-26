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


    // starting connections
    void loadInitialConnections(){
        // get positions
        for(int i = 0; i < transform.childCount; i++){
            GameObject childNode = transform.GetChild(i).gameObject;
            
            if(childNode.tag == "Node"){
                Vector3 pos = childNode.transform.position;
                float x = pos[0];
                float z = pos[2];

                NodePosition np = new NodePosition(x, z);
                nodePositions.Add(np);
            }
        }

        // add connections
        addConnection(0, 0);
        addConnection(0, 1);
        addConnection(0, 2);
        addConnection(0, 4);

        addConnection(1, 1);
        addConnection(1, 2);
        addConnection(1, 3);

        addConnection(2, 2);
        addConnection(2, 3);

        addConnection(3, 3);
        addConnection(3, 4);

        addConnection(4, 4);
    }

    // add a connection from "homeID" to "destinationID"
    void addConnection(int homeID, int destinationID){
        // make sure theres enough space
        while(connections.Count < homeID + 1 | connections.Count < destinationID + 1){
            connections.Add(new NodeOut());
        }

        // add the road to the list of roads from both nodes
        EdgeData ed1 = new EdgeData(nodePositions[homeID], nodePositions[destinationID], homeID, destinationID);
        EdgeData ed2 = new EdgeData(nodePositions[homeID], nodePositions[destinationID], destinationID, homeID);

        connections[homeID].addRoad(ed1);
        connections[destinationID].addRoad(ed2);
    }
}


