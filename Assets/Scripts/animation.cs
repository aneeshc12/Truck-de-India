using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
     
    Animator anim;
 
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            anim.SetTrigger("Active");
        }
        if (Input.GetKey(KeyCode.P))
        {
            anim.SetTrigger("Close");
        }
    }
}
