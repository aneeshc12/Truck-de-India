using UnityEngine;

public class LightUp : MonoBehaviour
{  
    Renderer rendererComp;
    Material baseMat;
    public Material litUpMat; 

    // Start is called before the first frame update
    void Start()
    {
        rendererComp = GetComponent<Renderer>();
        baseMat = rendererComp.material;        
    }


    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver()
    {
        rendererComp.material = litUpMat;
    }

    void OnMouseExit()
    {
        rendererComp.material = baseMat;
    }
}


