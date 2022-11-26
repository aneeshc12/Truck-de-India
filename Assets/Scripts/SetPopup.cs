using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPopup : MonoBehaviour
{
    private ContractManager contractManager;
    private NodeBehavior nodeAttributes;


    // private TextMesh textMesh;
    private MeshRenderer material_rend;
    public Material clothMat;
    public Material woodMat;
    public Material wheatMat;


    public int node;
    public int posn;

    private void Start()
    {
        // textMesh = GetComponent<TextMesh>();
        material_rend = GetComponent<MeshRenderer>();
        contractManager = GameObject.Find("Contract Manager").GetComponent<ContractManager>();
        nodeAttributes = GameObject.Find("Node " + node).GetComponent<NodeBehavior>();
    }
    
    public Contract contract;

    private void Update()
    {
        // textMesh.text = contractManager.contracts[node][posn].time_left.ToString();

        List<int> positions = new List<int>();
        for (int i = 0; i < contractManager.contracts.Count; i++)
        {
            if (contractManager.contracts[i].dest_node_id == node)
            {
                positions.Add(i);
            }
        }

        if (positions.Count > 0 && posn < positions.Count)
        {
            int contract_posn = positions[posn];
            contract = contractManager.contracts[contract_posn];

            // textMesh.text = (contract.amount_needed - contract.amount_delivered).ToString() + "";
            if ((int)contract.resource_type == 1)
            {
                material_rend.material = wheatMat;
                // quad.GetComponent<Renderer>().material = newMat;
            }
            else if ((int)contract.resource_type == 2)
            {
                material_rend.material = woodMat;
            }
            else if ((int)contract.resource_type == 3)
            {
                material_rend.material = clothMat;
            }
           

        }
    }
}
