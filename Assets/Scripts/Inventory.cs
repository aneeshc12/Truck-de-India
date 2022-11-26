using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes {
    Empty = 0,
    Wheat = 1,
    Brick = 2,
    Steel = 3
}

public class Inventory : MonoBehaviour
{
    public int inventorySize = 5;
    public List<int> cntInventory;

    void Start()
    {
        // init inventory
        cntInventory = new List<int> {};
        for(int i = 0; i < inventorySize; i++){
            cntInventory.Add((int) ItemTypes.Empty);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // deliver from inventory to node
    public int Deliver(ItemTypes itemType, int quantity){
        // verify if params are valid
        if(!(quantity <= inventorySize & quantity > 0)){
            return 1;
        }

        // check whether the delivery is possible
        int itemsSoFar = 0;
        for(int i = 0; i < cntInventory.Count; i ++){
            if(cntInventory[i] == (int) itemType){
                itemsSoFar += 1;
            } 
        }
        
        if(itemsSoFar < quantity)
            return 1;

        // subtract items from inventory
        int itemsRemoved = 0;
        for(int i = 0; i < cntInventory.Count & itemsRemoved < quantity; i++){
            if(cntInventory[i] == (int) itemType){
                cntInventory[i] = (int) ItemTypes.Empty;
                itemsRemoved += 1;              // replace slots with Empty
            }
        }

        return 0;
    }

    public int Load(ItemTypes itemType, int quantity){
        // verify if params are valid
        if(!(quantity <= inventorySize & quantity > 0)){
            return 1;
        }

        // check whether there is enough space
        int freeSpace = 0;
        for(int i = 0; i < cntInventory.Count; i ++){
            if(cntInventory[i] == (int) ItemTypes.Empty) 
                freeSpace += 1;
        }
        
        if(freeSpace < quantity)
            return 1;

        // subtract items from inventory
        int itemsAdded = 0;
        for(int i = 0; i < cntInventory.Count & itemsAdded < quantity; i++){
            if(cntInventory[i] == (int) ItemTypes.Empty){
                cntInventory[i] = (int) itemType;
                itemsAdded += 1;              // replace slots with Empty
            }
        }

        return 0;   
    }
}
