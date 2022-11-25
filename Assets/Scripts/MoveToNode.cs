using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NodeQueue 
{
    public List<Tuple<int, float>> q = new List<Tuple<int, float>>();

    public NodeQueue(){}

    public void Enqueue(int id, float dist){
        Tuple<int, float> pair = Tuple.Create(id, dist);
        q.Add(pair);

        return;
    }

    public int Dequeue(){
        int minIndex = 0;

        int minNode = q[0].Item1;
        float minCost = q[0].Item2;

        for(int i = 0; i < q.Count; i++){
            var t = q[i];
            if(t.Item2 < minCost){
                minIndex = i;

                minNode = t.Item1;
                minCost = t.Item2;
            }
        }

        q.RemoveAt(minIndex);
        return minNode;
    }

    public void UpdatePriority(int node, float dist){
        Tuple<int, float> pair = Tuple.Create(node, dist);

        for(int i = 0; i < q.Count; i++){
            if(q[i].Item1 == node){
                q[i] = pair;
            }
        }
    }
}

public class MoveToNode : MonoBehaviour
{
    // Ray variables
    Ray ray;
    RaycastHit hitData;

    // Physics
    Rigidbody rb;

    // ler
    float timeElapsed;
    public float speed = 3;     // in ms-1
    float travelDuration;
    Vector3 initPos, finalPos;
    float valueToLerp;

    float initY;

    uint nodePosNeedsToUpdate = 0;
    uint onPath = 0;
    int pathIndex = 0;

    int cntNodeID = 0;
    int destinationNodeID = 1;
    List<int> nodesToVisit;

    public const int limit = 3;
    public int carry = 50;

    public int pickupOrDrop = 0;
    Inventory inv;
    SupplyDemand destinationSupplyDemand;

    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rb = GetComponent<Rigidbody>();

        initPos = transform.position;
        initY = initPos.y;
        travelDuration = 1/speed;

        inv = gameObject.GetComponent<Inventory>() as Inventory;
    }

    public float a = 2f;
    public float b = 3f;
    public float c = 2f;

    // Update is called once per frame
    void Update()
    {
        // sense clicks
        int clicked = 0;
        if(Input.GetMouseButtonDown(0)){
            pickupOrDrop = 0;
            clicked = 1;
            // Debug.Log("Left");
        }
        else if (Input.GetMouseButtonDown(1)){
            pickupOrDrop = 1;
            clicked = 1;
            // Debug.Log("Right");
        }

        // move and get node
        if(clicked == 1)
        {
            GameObject nextNode = GetNode();
            if (nextNode.tag == "Node"){
                // TransferResources(cntNodeID, destinationNodeID);
                destinationSupplyDemand = nextNode.GetComponent<NodeBehavior>().supplyDemand;
            }
        }

        if(onPath == 1){
            if(pathIndex < nodesToVisit.Count - 1) {
                if(nodePosNeedsToUpdate == 1){
                    List<GameObject> Children = new List<GameObject> {};
                    foreach (Transform child in GameObject.Find("Nodes").transform)
                    {
                        if (child.tag == "Node")
                        {
                            if(child.GetComponent<NodeBehavior>().ID == nodesToVisit[pathIndex]){
                                initPos = child.transform.position;
                            }
                            else if(child.GetComponent<NodeBehavior>().ID == nodesToVisit[pathIndex + 1]){
                                finalPos = child.transform.position;
                            }
                        }
                    }

                    nodePosNeedsToUpdate = 0;
                }

                if (timeElapsed < travelDuration){
                    // smooth step
                    float t = timeElapsed / travelDuration;
                    t = a * t * t * (b - (b-1) * t);

                    Vector3 intermediatePos = Vector3.Lerp(initPos, finalPos, t);
                    intermediatePos.y = initY;
                    transform.position = intermediatePos;
                    
                    timeElapsed += Time.deltaTime;
                }
                // moved to the next node
                else {
                    timeElapsed = 0;
                    pathIndex++;
                    nodePosNeedsToUpdate = 1;
                }
                
            }
            // movement completed, start loading/unloading
            else {
                onPath = 0;
                pathIndex = 0;
                nodePosNeedsToUpdate = 0;

                timeElapsed = 0;
                cntNodeID = nodesToVisit[nodesToVisit.Count - 1];

                // pickup
                if(pickupOrDrop == 0){
                    inv.Deliver(0, destinationSupplyDemand.demandItemType, 1);
                }
                // drop
                else {
                    inv.Load(destinationSupplyDemand.supplyItemType, 1);
                }
            }
        }

    }

    void OnCollisionEnter(Collision collision){
        // clean initY
        initY = transform.position.y;
    }


    GameObject GetNode(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        GameObject hitObject = hitData.transform.gameObject;
        if(hitObject.tag == "Node"){        
            // init movement if not already moving
            if(onPath == 0){
                // get clicked node ID
                if (hitData.collider.gameObject.GetComponent<NodeBehavior>() != null){
                    GameObject chosenNode = hitData.collider.gameObject;
                    destinationNodeID = chosenNode.GetComponent<NodeBehavior>().ID;
                    NodeOut nodeConnections = chosenNode.GetComponent<NodeBehavior>().connections;

                    nodesToVisit = ShortestPath(cntNodeID, destinationNodeID);
                    for(int i = 0; i < nodesToVisit.Count; i++){
                        Debug.Log(nodesToVisit[i]);
                    }

                    onPath = 1;
                    nodePosNeedsToUpdate = 1;

                    // Debug.Log("Chosen destination node: " + destinationNodeID);

                    // check if the chosen node is connected to the current node
                    // foreach(EdgeData ed in nodeConnections.roads){
                    //     if(ed.destinationID == cntNodeID){
                    //         isMoving = 1;
                    //         initPos = transform.position;
                    //         finalPos = hitData.transform.position;

                    //         timeElapsed = 0;

                    //         break;
                    //     }
                    // }
                }
            }

        }
        return hitObject;
    }

    // ----------------------------------------------------------------------------

    // utility function to find shortest path
    // essentially Dijkstras
    public List<int> ShortestPath(int homeID, int destinationID){
        if(homeID == destinationID){
            List<int> temp = new List<int>();
            temp.Add(homeID);
            return temp;
        }

        NodeManager nodeManager = GameObject.Find("Nodes").GetComponent("NodeManager") as NodeManager;

        // util functions
        float GetDistance(int homeID, int destinationID){
            foreach(EdgeData ed in nodeManager.connections[homeID].roads){
                if(ed.homeID == homeID & ed.destinationID == destinationID){
                    return ed.distance;
                }
            }

            // not found
            return float.MaxValue;
        }

        void Relax(int homeID, int destinationID, float weight, float[] distances, int[] previous)
        {
            if (distances[homeID] != float.MaxValue & distances[destinationID] > distances[homeID] + weight)
            {
                distances[destinationID] = distances[homeID] + weight;
                previous[destinationID] = homeID;
            }
        }

        // main code
        int numNodes = nodeManager.nodeList.Length;
        float[] distances = new float[numNodes];
        int[] previous = new int[numNodes];

        NodeQueue nodeQueue = new NodeQueue();

        for(int i = 0; i < numNodes; i++){
            previous[i] = -1;

            if(i == homeID){
                distances[i] = 0.0f;
            }
            else{
                distances[i] = float.MaxValue;
            }
            nodeQueue.Enqueue(i, distances[i]);
        }

        while(nodeQueue.q.Count > 0){

            // extract min
            int closestNode = nodeQueue.Dequeue();

            int connectedNum = nodeManager.connections[closestNode].roads.Count;
            for (int v = 0; v < connectedNum; v++)
            {
                int queryNode = nodeManager.connections[closestNode].roads[v].destinationID;
                if(queryNode == closestNode){
                    continue;
                }
                
                float dist = GetDistance(closestNode, queryNode);
                if (dist > 0)
                {
                    Relax(closestNode, queryNode, dist, distances, previous);
                    //updating priority value since distance is changed
                    nodeQueue.UpdatePriority(queryNode, distances[queryNode]);
                }
            }

            // Debug.Log("Logs: ");
            // for (int k = 0; k < numNodes; k++){
            //     Debug.Log(k + " : " + distances[k] + " " +  previous[k]);
            // }
            // Debug.Log("");
        }

        // recursively print path
        List<int> path = new List<int>();
        void GeneratePath(int u, int v, float[] distance, int[] parent)
        {
            if (v < 0 || u < 0)
            {
                return;
            }
            if (v != u)
            {
                GeneratePath(u, parent[v], distance, parent);
                // Debug.Log("Vertex " + v +  ", weight:" + distance[v]);
                
                path.Add(v);
            }
            else{
                // Debug.Log("Vertex " + v +  ", weight:" + distance[v]);
                path.Add(v);
            }
        }

        // Debug.Log("Shortest path: ");
        GeneratePath(homeID, destinationID, distances, previous);
        return path;
    }

    // ----------------------------------------------------------------------------

    public void TransferResources(int transferFromID, int transferToID)
    {
        List<GameObject> Children = new List<GameObject> {};
        foreach (Transform child in GameObject.Find("Nodes").transform)
        {
            if (child.tag == "Node")
            {
                Children.Add(child.gameObject);
            }
        }

        foreach (GameObject node in Children){
            if(node.GetComponent<NodeBehavior>().ID == transferFromID){
                Debug.Log("From: " + transferFromID);
                Debug.Log(node.GetComponent<NodeBehavior>().resource);
                node.GetComponent<NodeBehavior>().resource -= carry;
            }
            else if(node.GetComponent<NodeBehavior>().ID == transferToID){
                Debug.Log("To: " + transferToID);
                node.GetComponent<NodeBehavior>().resource += carry;
            }
        }
    }
}
