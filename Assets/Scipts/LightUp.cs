using UnityEngine;

public class LightUp : MonoBehaviour
{  
    Renderer renderer;
    Material baseMat;
    public Material litUpMat; 

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        baseMat = renderer.material;        
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


