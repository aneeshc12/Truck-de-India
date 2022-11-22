using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAttributes : MonoBehaviour
{
    public float brightnessFactor = 1.5f;
    private Color baseColor;
    private Color litUpColor;
    // private Material mat;
    private Renderer rend;


    public int resource = 100; 

    
    // resources[0] =100;



    private void OnEnable()
    {
        // Debug.Log("NodeAttributes: OnEnable");
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        litUpColor = new Color(baseColor.r * brightnessFactor, baseColor.g * brightnessFactor, baseColor.b * brightnessFactor);
        resource = 100;

        Debug.Log(resource);
    }

    void OnMouseOver()
    {
        rend.material.color = litUpColor;
    }

    void OnMouseExit()
    {
        rend.material.color = baseColor;
    }




}
