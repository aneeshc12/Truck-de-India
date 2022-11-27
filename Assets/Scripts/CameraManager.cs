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
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;


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
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraWaypoints[cameraPosition].transform.rotation, Time.deltaTime * rotateSpeed);  
        } 
        if (cameraPosition ==0)
        {
            MapCamflag=0;
            if (Input.GetKey("1"))
            {
                cameraPosition=0;
            }
            if (Input.GetKey("2"))
            {
                cameraPosition=1;
            }
            if (Input.GetKey("3"))
            {
                cameraPosition=2;
            }
            if (Input.GetKey("4"))
            {
                cameraPosition=3;
            }
        }
        if (cameraPosition == 1)
        {
            MoveOverMap();
        }
        if (cameraPosition == 2)
        {
            MapCamflag=0;
            if (Input.GetKey("1"))
            {
                cameraPosition=0;
            }
            if (Input.GetKey("2"))
            {
                cameraPosition=1;
            }
            if (Input.GetKey("3"))
            {
                cameraPosition=2;
            }
            if (Input.GetKey("4"))
            {
                cameraPosition=3;
            }
        }
        if (cameraPosition == 3)
        {
            MapCamflag=0;
            if (Input.GetKey("1"))
            {
                cameraPosition=0;
            }
            if (Input.GetKey("2"))
            {
                cameraPosition=1;
            }
            if (Input.GetKey("3"))
            {
                cameraPosition=2;
            }
            if (Input.GetKey("4"))
            {
                cameraPosition=3;
            }
        }     
    }

    void MoveOverMap()
    {
        CameraPosition = this.transform.position;
        print(CameraPosition.z);
        print(CameraPosition.x);
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
            if (Input.GetKey(KeyCode.Escape))
            {
                cameraPosition = 2;
                MapCamflag = 0;
            }
            if (Input.GetKey("1"))
            {
                audioSource1.PlayOneShot(audioClip1);
            }
            if (Input.GetKey("2"))
            {
                audioSource2.PlayOneShot(audioClip2);
            }
            if (Input.GetKey("3"))
            {
                audioSource3.PlayOneShot(audioClip3);
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
        
        mouseScrollDelta = Input.mouseScrollDelta;
        if (mouseScrollDelta.y != 0)
        {
            cam = Camera.main;
            cam.fieldOfView  = Mathf.Clamp(cam.fieldOfView  - mouseScrollDelta.y * zoomStep, minCamsize, maxCamsize);
        }
    this.transform.position=CameraPosition;
    }
}
