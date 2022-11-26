using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadsLook : MonoBehaviour
{
    public Transform cameraTransform;
    
    void Update(){
        transform.LookAt(transform.position - (cameraTransform.position - transform.position));
    }
}
