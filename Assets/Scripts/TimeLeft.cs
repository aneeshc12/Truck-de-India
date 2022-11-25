using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    private ContractManager contractManager;
    private NodeBehavior nodeAttributes;

    private Transform transformer;
    private Transform glass_transformer;

    public List<int> num_contracts;

    public int node = 1;
    public int posn = 0;
    private float height;
    private float height_g;

    private void Start()
    {
        transformer = GetComponent<Transform>();
        // renderer = GetComponent<Renderer>();
        contractManager = GameObject.Find("Contract Manager").GetComponent<ContractManager>();
        // nodeAttributes = GameObject.Find("Node " + node).GetComponent<NodeBehavior>();
        num_contracts = contractManager.num_contracts;
        glass_transformer = GameObject.Find(gameObject.name + "_" + node.ToString() +  "g").GetComponent<Transform>();
        height_g = glass_transformer.localScale.y;




        // height is the height of cylinder
        height = transformer.localScale.y;



    }

    public Contract contract; //= contractManager.contracts[0];
    private void Update()
    {
        // contractManager = GameObject.Find("Contract Manager").GetComponent<ContractManager>();
        num_contracts = contractManager.num_contracts;
        
        if (posn >= num_contracts[node])
        {
            //make cylinder invisible
            // gameObject.SetActive(false);
            transformer.localScale = new Vector3(transformer.localScale.x, 0, transformer.localScale.z);
            glass_transformer.localScale = new Vector3(glass_transformer.localScale.x, 0, glass_transformer.localScale.z);

            // GetComponent<Renderer>().enabled = false;
        }
        else
        {
            // gameObject.SetActive(true);
            transformer.localScale = new Vector3(transformer.localScale.x, (height), transformer.localScale.z);
            glass_transformer.localScale = new Vector3(glass_transformer.localScale.x, (height_g), glass_transformer.localScale.z);
            // GetComponent<Renderer>().enabled = true;
        }

        // iterating over contracts

        List<int> positions = new List<int>();
        for (int i = 0; i < contractManager.contracts.Count; i++)
        {
            if (contractManager.contracts[i].dest_node_id == node)
            {
                positions.Add(i);
            }
        }

        Debug.Log(positions.Count);
        Debug.Log(posn);
        int contract_posn = positions[posn];    

        contract = contractManager.contracts[contract_posn];
        if (contract.dest_node_id == node)
        {
            
            // Debug.Log(contract.time_left);
            // Debug.Log(contract.contract_time);
            // Debug.Log(transformer.localScale);
            float timeLeft = contract.time_left;
            transformer.localScale = new Vector3(transformer.localScale.x, (height*(timeLeft/contract.contract_time)), transformer.localScale.z);

        }
    }




}
