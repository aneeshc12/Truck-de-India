using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public GameObject[] cameraWaypoints;
    public int cameraPosition = 0;
    private Vector3 CameraPosition;
    public float panSpeed = 20f;
    public float lerpSpeed = 0.1f;
    public float rotateSpeed = 0.1f;
    private int MapCamflag=0;
    private Camera cam;
    public static Vector2 mouseScrollDelta;


    [SerializeField]
    private float zoomStep=1f,minCamsize=30f,maxCamsize=80f;

    void Start()
    {
        transform.position = cameraWaypoints[0].transform.position;
    }

    void FixedUpdate()
    {
        if (MapCamflag==0 || cameraPosition != 1){
            transform.position = Vector3.Lerp(transform.position, cameraWaypoints[cameraPosition].transform.position, Time.deltaTime * lerpSpeed);
        }
        if (cameraPosition == 1)
        {
            MoveOverMap();
            if (Input.GetKey(KeyCode.Escape))
            {
                cameraPosition = 2;
                MapCamflag = 0;
            }
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraWaypoints[cameraPosition].transform.rotation, Time.deltaTime * rotateSpeed);        
    }

    void MoveOverMap()
    {
        CameraPosition = this.transform.position;
        Debug.Log(CameraPosition);
        // bounds
        if (CameraPosition.x <= 23 && CameraPosition.x >= 3.5 && CameraPosition.z <= 28 && CameraPosition.z >= 13)
        {
            if (Input.GetKey(KeyCode.W))
            {
                CameraPosition.z += panSpeed * Time.deltaTime;
                MapCamflag=1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                CameraPosition.z -= panSpeed * Time.deltaTime;
                MapCamflag=1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                CameraPosition.x -= panSpeed * Time.deltaTime;
                MapCamflag=1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                CameraPosition.x += panSpeed * Time.deltaTime;
                MapCamflag=1;
            }
        }
        else
        {
            if (CameraPosition.x > 23)
            {
                CameraPosition.x = 23;
            }
            if (CameraPosition.x < 3.5)
            {
                CameraPosition.x = 3.5f;
            }
            if (CameraPosition.z > 28)
            {
                CameraPosition.z = 28;
            }
            if (CameraPosition.z < 13)
            {
                CameraPosition.z = 13;
            }
        }
        this.transform.position = CameraPosition;
        
        mouseScrollDelta = Input.mouseScrollDelta;
        if (mouseScrollDelta.y != 0)
        {
            cam = Camera.main;
            cam.fieldOfView  = Mathf.Clamp(cam.fieldOfView  - mouseScrollDelta.y * zoomStep, minCamsize, maxCamsize);
        }

    }
}
