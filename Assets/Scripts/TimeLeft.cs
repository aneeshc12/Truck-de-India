using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    private ContractManager contractManager;
    private NodeBehavior nodeAttributes;

    private Transform transformer;


    public int node = 2;
    public int posn = 0;
    private float height;

    private void Start()
    {
        transformer = GetComponent<Transform>();
        contractManager = GameObject.Find("Contract Manager").GetComponent<ContractManager>();
        nodeAttributes = GameObject.Find("Node " + node).GetComponent<NodeBehavior>();

        // height is the height of cylinder
        height = transformer.localScale.y;



    }

    public Contract contract; //= contractManager.contracts[0];
    private void Update()
    {
        contract = contractManager.contracts[posn];
        if (contract.dest_node_id == node)
        {
            // Debug.Log("suiiiiiii");
            Debug.Log(contract.time_left);
            Debug.Log(contract.contract_time);
            Debug.Log(transformer.localScale);
            // change height of cylinder
            //converting to float
            float timeLeft = contract.time_left;
            transformer.localScale = new Vector3(transformer.localScale.x, (height*(timeLeft/contract.contract_time)), transformer.localScale.z);

        }
    }




}
