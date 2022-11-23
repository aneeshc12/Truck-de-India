using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeBehavior : MonoBehaviour
{  
    Renderer renderer;
    Material baseMat;
    public Material litUpMat; 

    public int ID; 
    public List<int> connections;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        baseMat = renderer.material;      

        // get connections
        GameObject parent = transform.parent.gameObject;
        connections = parent.GetComponent<NodeManager>().connections[ID];  
    }


    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver()
    {
        renderer.material = litUpMat;
    }

    void OnMouseExit()
    {
        renderer.material = baseMat;
    }
}


