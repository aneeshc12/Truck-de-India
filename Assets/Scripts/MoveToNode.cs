using UnityEngine;
using System;

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
            GameObject nextNode =  GetNode();

            if (nextNode.tag == "Node")
            {
                gameObject.GetComponent<BallAttributes>().TransferResources(nextNode);
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
            }
            // Debug.Log(transform.position);
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
            Debug.Log("Node : " + hitObject.name + " hit!");
        
            // init movement if not already moving
            if(isMoving == 0){
                isMoving = 1;
                initPos = transform.position;
                finalPos = hitData.transform.position;

                timeElapsed = 0;
            }
        }
        return hitObject;
    }
}
