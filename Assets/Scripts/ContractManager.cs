using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{

    object lock_obj = new object();

    // creating a list of contracts
    public List<Contract> contracts = new List<Contract>();


    public int max_contracts = 10;

    public int num_nodes;
    public List<int> num_contracts;

    public MoneyTimer moneyTimer;
    public float money;


    // creating a contract
    public Contract newContract;



    private void Start()
    { 

        // num_nodes = GameObject.Find("Nodes").GetComponent<NodeManager>().nodeList.Count();
        
        Debug.Log(num_nodes);
        num_contracts = new List<int>(Enumerable.Repeat(0, num_nodes));
        StartCoroutine(RandomContractGenerator());
        moneyTimer = GameObject.Find("Money Counter").GetComponent<MoneyTimer>();
        money = moneyTimer.money;
        // money = moneyCounter.money;

        
    }



    // List<int> num_contracts = List.;
    // using enumeratable range
    


    IEnumerator RandomContractGenerator()
    {

        while(true)

        {
            Debug.Log(contracts[i].contract_time);
            StartCoroutine(StartContract(contracts[i]));



                rand_node = Random.Range(0, num_nodes);
                // Debug.Log("Random Number: " + rand_node);
                if (num_contracts[rand_node] < 3)
                {
                    lock(lock_obj)
                    {
                        num_contracts[rand_node] += 1;
                    }
                    break;
                }

            }
            int rand_num_resources = Random.Range(1, 4);
            int rand_resource_type = Random.Range(1, 4);
<<<<<<< Updated upstream

=======

            newContract = new Contract(rand_node,rand_num_resources,0,(ItemTypes) rand_resource_type,50,50);
         

            contracts.Add(newContract);
            StartCoroutine(StartContract(newContract));
>>>>>>> Stashed changes

            newContract = new Contract(rand_node,rand_num_resources,0,(ItemTypes) rand_resource_type,50,50);

            contracts.Add(newContract);
            StartCoroutine(StartContract(newContract));
            
            string str ="";
            for(int j=0;j<num_contracts.Count();j++)
            {
                str += num_contracts[j].ToString() + " ";
            }
            Debug.Log(str);
            yield return new WaitForSeconds(5);
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

                // Debug.Log("Contract Delivered at "+gameObject.name);
                // deleting contract from contract list
                lock(lock_obj)
                {
                    num_contracts[contract.dest_node_id] -= 1;
                    moneyTimer.money += (float)contract.amount_needed;
                }
                if(num_contracts[contract.dest_node_id]<0)
                {
                    num_contracts[contract.dest_node_id] = 0;
                }
                contracts.Remove(contract); 
            }
            contract.time_left = i;

            //printing each value in num_contracts in one line
            // Debug.Log(str);


            yield return new WaitForSeconds(1);
        }
        if (contract.amount_delivered<contract.amount_needed)
        {

            // Debug.Log("Contract Failed at "+contract.dest_node_id);
            lock(lock_obj)
            {
                num_contracts[contract.dest_node_id] -= 1;
                moneyTimer.money -= 3f;
            }
            if(num_contracts[contract.dest_node_id]<0)
            {
                num_contracts[contract.dest_node_id] = 0;
            }
            contracts.Remove(contract);
        }

    }
    
}

public class Contract
{
    public int dest_node_id;
    public int amount_needed;
    public int amount_delivered;
    public ItemTypes resource_type;
    public int contract_time;
    public int time_left;


    public Contract(int dest_node_id, int amount_needed, int amount_delivered, ItemTypes resource_type, int contract_time, int time_left)
    {
        this.dest_node_id = dest_node_id;
        this.amount_needed = amount_needed;
        this.amount_delivered = amount_delivered;
        this.resource_type = resource_type;
        this.contract_time = contract_time;
        this.time_left = time_left;

    } 
}
