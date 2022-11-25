using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupplyDemand
{
    public ItemTypes supplyItemType;
    
    public ItemTypes demandItemType;
    public int demandNum;

    public SupplyDemand(ItemTypes s, ItemTypes d, int dNum){
        supplyItemType = s;
        demandItemType = d;
        demandNum = dNum;
    }
}

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
    public SupplyDemand supplyDemand;

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
            supplyDemand = new SupplyDemand(ItemTypes.Wheat, ItemTypes.Brick, 3);
        }
        else {
            supplyDemand = new SupplyDemand(ItemTypes.Brick, ItemTypes.Wheat, 3);
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


