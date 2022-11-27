using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class NodeManager : MonoBehaviour
{
    public int[] nodeList = new int[] {0,1,2,3,4};
    public List<List<int>> connections = new List<List<int>>();
=======
// Classes to represent graph substructures
// represent node position
[System.Serializable]
public class NodePosition{
    public float[] position;

    public NodePosition(){}

    public NodePosition(float x, float y){
        position = new float[] {x,y};
        Debug.Log("Added: " + x + " " + y);
    }
}

// represent one edge
[System.Serializable]
public class EdgeData{
    public float distance;
    public int homeID;
    public int destinationID;

    public EdgeData(){}

    // create an edge from two node positions and IDs
    public EdgeData(NodePosition np1, NodePosition np2, int hID, int dID){
        float[] pos1 = np1.position;
        float[] pos2 = np2.position;

        distance = Mathf.Sqrt(Mathf.Pow(pos1[0] - pos2[0], 2.0f) + Mathf.Pow(pos1[1] - pos2[1], 2.0f));
        homeID = hID;
        destinationID = dID;
    }
}

// represents all edges from one node
[System.Serializable]
public class NodeOut{
    public List<EdgeData> roads = new List<EdgeData>();

    public NodeOut(){}

    public void addRoad(EdgeData ed){
        roads.Add(ed);
    }
}


public class NodeManager : MonoBehaviour
{
    public int[] nodeList = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14};
    public List<NodePosition> nodePositions = new List<NodePosition>();        // tracks x,y of each node

    public List<NodeOut> connections = new List<NodeOut>();
>>>>>>> Stashed changes

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

            Debug.Log("!!!!" + i);
            
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

        addConnection(1, 5);
        addConnection(3, 5);
        addConnection(5, 5);

        addConnection(6,6);
        addConnection(6,4);
        addConnection(6,7);

        addConnection(7,7);
        addConnection(7,8);
        addConnection(7,10);

        addConnection(8,8);
        addConnection(8,9);
        addConnection(8,10);

        addConnection(9,9);
        addConnection(9,1);
        addConnection(9,11);
        addConnection(9,12);

        addConnection(10,10);
        addConnection(10,14);

        addConnection(11,11);
        addConnection(11,3);
        addConnection(11,12);

        addConnection(12,12);
        addConnection(12,13);

        addConnection(13,13);
        addConnection(13,14);

        addConnection(14,14);
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


