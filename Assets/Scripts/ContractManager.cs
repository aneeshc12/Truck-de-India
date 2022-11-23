using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Contract
{
    public int contract_time;
    public int required_resource;
    public int current_resource;


    public Contract(int contract_time, int required_resource, int current_resource)
    {
        this.contract_time = contract_time;
        this.required_resource = required_resource;
        this.current_resource = current_resource;
    }

    IEnumerator StartContract(Contract contract)
    {

        for (int i = contract.contract_time;i > 0; i--)
        {
            // Debug.Log(i.ToString()+" "+gameObject.name);
            // yield return new WaitForSeconds(1);
            if (contract.current_resource >= contract.required_resource)
            {
                // Debug.Log("Contract Delivered at "+gameObject.name);
            }
            else
            {
                // Debug.Log((contract.required_resource-contract.current_resource).ToString()+" resources left to deliver at "+gameObject.name+" in the next "+i.ToString()+" seconds");
            }
            yield return new WaitForSeconds(1);
        }
        if (contract.current_resource<contract.required_resource)
        {
            // Debug.Log("Contract Failed at "+gameObject.name);
        }
    }
}
