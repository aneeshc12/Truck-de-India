using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public Material emptyMat;
    public Material wheatMat;
    public Material woodMat;
    public Material clothMat;

    Inventory inventory;

    GameObject allSlots;
    List<GameObject> slots = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {   
        // actual inventorys
        inventory = GetComponent<Inventory>();

        // get slot panels
        allSlots = transform.GetChild(0).gameObject;
        for(int i = 0; i < allSlots.transform.childCount; i++){
            slots.Add(allSlots.transform.GetChild(i).gameObject);
        }
    }

    // Render each inv slot
    void Update()
    {
        int invSize = inventory.inventorySize;
        for(int i = 0; i < invSize; i++){
            if(inventory.cntInventory[i] == (int) ItemTypes.Empty){
                Renderer renderer = slots[i].GetComponent<Renderer>();
                renderer.enabled = false;
            }
            else if(inventory.cntInventory[i] == (int) ItemTypes.Wheat){
                Renderer renderer = slots[i].GetComponent<Renderer>();
                renderer.enabled = true;
                renderer.material = wheatMat;
            }
            else if(inventory.cntInventory[i] == (int) ItemTypes.Brick){
                Renderer renderer = slots[i].GetComponent<Renderer>();
                renderer.enabled = true;
                renderer.material = woodMat;
            }
            else if(inventory.cntInventory[i] == (int) ItemTypes.Steel){
                Renderer renderer = slots[i].GetComponent<Renderer>();
                renderer.enabled = true;
                renderer.material = clothMat;
            }

        }
        allSlots.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.z * -1.0f);
    
        // dump inventory
        if (Input.GetKey("space")){
            for(int i = 0; i < invSize; i++){
                inventory.cntInventory[i] = (int) ItemTypes.Empty;
            }
        }
    }
}
