using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    // creating a list of contracts
    public List<Contract> contracts = new List<Contract>();

    // creating a contract
    public Contract newContract;

    private void Start()
    {
        // creating a new contract
        newContract = new Contract(2,5,0,1,50,50);
        // adding the contract to the list
        contracts.Add(newContract);
        newContract = new Contract(3,4,0,2,60,60);
        contracts.Add(newContract);
        newContract = new Contract(4,5,0,3,70,70);
        contracts.Add(newContract);
        newContract = new Contract(2,3,0,2,80,80);
        contracts.Add(newContract);
        newContract = new Contract(4,5,0,2,90,90);
        contracts.Add(newContract);

        for (int i = 0; i < contracts.Count; i++)
        {
            Debug.Log(contracts[i].contract_time);
            StartCoroutine(StartContract(contracts[i]));


        }

    }


    
    

    

    IEnumerator StartContract(Contract contract)
    {

        for (int i = contract.contract_time;i > 0; i--)
        {
            // Debug.Log(i.ToString()+" "+gameObject.name);
            // yield return new WaitForSeconds(1);
            if (contract.amount_delivered >= contract.amount_needed)
            {
                Debug.Log("Contract Delivered at "+gameObject.name);
            }
            else
            {
                Debug.Log((contract.amount_needed-contract.amount_delivered).ToString()+" resources left to deliver at "+contract.dest_node_id.ToString()+" in the next "+i.ToString()+" seconds");
            }
            contract.time_left = i;
            yield return new WaitForSeconds(1);
        }
        if (contract.amount_delivered<contract.amount_needed)
        {
            Debug.Log("Contract Failed at "+contract.dest_node_id);
        }

    }
    
}

public class Contract
{
    public int dest_node_id;
    public int amount_needed;
    public int amount_delivered;
    public int resource_type;
    public int contract_time;
    public int time_left;


    public Contract(int dest_node_id, int amount_needed, int amount_delivered, int resource_type, int contract_time, int time_left)
    {
        this.dest_node_id = dest_node_id;
        this.amount_needed = amount_needed;
        this.amount_delivered = amount_delivered;
        this.resource_type = resource_type;
        this.contract_time = contract_time;
        this.time_left = time_left;
    }


    
}
