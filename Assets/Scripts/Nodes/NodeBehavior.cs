using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeBehavior : MonoBehaviour
{  
    Renderer renderer;
    Material baseMat;
    // public Material litUpMat; 

    public float brightnessFactor = 1.5f;
    private Color baseColor;
    private Color litUpColor;
    private Renderer rend;

    public int ID; 
    public NodeOut connections;

    public int resource = 100;
    public ItemTypes supplyType;
    public ItemTypes demandType;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        baseMat = renderer.material;      

        // get connections
        GameObject parent = transform.parent.gameObject;
        connections = parent.GetComponent<NodeManager>().connections[ID];  

        // setup supply and demand
        if(ID == 0){
            supplyType = ItemTypes.Wheat;
            demandType = ItemTypes.Brick;
        }
        else if(ID == 1){
            supplyType = ItemTypes.Brick;
            demandType = ItemTypes.Steel;
        }
        else {
            supplyType = ItemTypes.Steel;
            demandType = ItemTypes.Wheat;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    // light up
    private void OnEnable()
    {
        // Debug.Log("NodeAttributes: OnEnable");
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        litUpColor = new Color(baseColor.r * brightnessFactor, baseColor.g * brightnessFactor, baseColor.b * brightnessFactor);
        resource = 100;

        // Debug.Log(resource);
    }

    // void OnMouseOver()
    // {
    //     renderer.material = litUpMat;
    // }

    // void OnMouseExit()
    // {
    //     renderer.material = baseMat;
    // }
}


