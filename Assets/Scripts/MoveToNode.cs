using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    uint isMoving = 0;
    int cntNodeID = 0;
    int destinationNodeID = 1;

    public const int limit = 100;
    public int carry = 50;

    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rb = GetComponent<Rigidbody>();

        initPos = transform.position;
        initY = initPos.y;
        travelDuration = 1/speed;

        // Debug.Log(initPos);
    }

    public float a = 2f;
    public float b = 3f;
    public float c = 2f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            GameObject nextNode = GetNode();
            if (nextNode.tag == "Node"){
                TransferResources(cntNodeID, destinationNodeID);
            }
        }

        if(isMoving == 1) {
            if (timeElapsed < travelDuration){
                // smooth step
                float t = timeElapsed / travelDuration;
                t = a * t * t * (b - (b-1) * t);

                Vector3 intermediatePos = Vector3.Lerp(initPos, finalPos, t);
                intermediatePos.y = initY;
                transform.position = intermediatePos;
                
                timeElapsed += Time.deltaTime;
            }
            else {
                isMoving = 0;
                timeElapsed = 0;
                cntNodeID = destinationNodeID;
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
            if(isMoving == 0){
                // get clicked node ID
                if (hitData.collider.gameObject.GetComponent<NodeBehavior>() != null){
                    GameObject chosenNode = hitData.collider.gameObject;
                    destinationNodeID = chosenNode.GetComponent<NodeBehavior>().ID;
                    List<int> nodeConnections = chosenNode.GetComponent<NodeBehavior>().connections;

                    // Debug.Log("Chosen destination node: " + destinationNodeID);

                    // check if that node is connected
                    if(nodeConnections.Contains(cntNodeID)){
                        // initiate movement
                        // Debug.Log("Moving to " + destinationNodeID);
                        isMoving = 1;
                        initPos = transform.position;
                        finalPos = hitData.transform.position;

                        timeElapsed = 0;
                    }
                    else{
                        // Debug.Log("That movement is not allowed");
                    }
                }
            }

        }
        transform.rotation = Quaternion.LookRotation(hitData.point - transform.position);
        return hitObject;
    }

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
                Debug.Log("FRom: " + transferFromID);
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
