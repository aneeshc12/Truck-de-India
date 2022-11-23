using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttributes : MonoBehaviour
{
    // set limit to unchangeable value
    public const int limit = 100;
    // public int limit = 100;
    public int carry = 0;
    public GameObject currNode;

    public void TransferResources(GameObject nextNode)
    {
        nextNode.GetComponent<NodeAttributes>().resource += carry;
        currNode.GetComponent<NodeAttributes>().resource -= carry;
        currNode = nextNode;
    }

    private void OnEnable()
    {
        // limit = 100;
        carry = 0;
        Debug.Log("BallAttributes: OnEnable");
    }

}
