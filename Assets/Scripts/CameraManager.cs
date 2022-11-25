using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public GameObject[] cameraWaypoints;
    public int cameraPosition = 0;
    public float lerpSpeed = 0.1f;
    public float rotateSpeed = 0.1f;
    void Start()
    {
        transform.position = cameraWaypoints[0].transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraWaypoints[cameraPosition].transform.position, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraWaypoints[cameraPosition].transform.rotation, Time.deltaTime * rotateSpeed);        
    }
}
