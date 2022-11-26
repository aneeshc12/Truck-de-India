using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTimer : MonoBehaviour
{
    public float money = 100f;
    public float moneyDrainRate = 0.001f;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        // for (float money = 100; money >=0 ; money -= moneyDrainRate)
        // {
        //     Debug.Log("Money: " + money);
        //     yield return null;
        // }
        while ( money >=0)
        {
            Debug.Log("Money: " + money);
            money -= moneyDrainRate;
            yield return null;
        }
    }
    
}
