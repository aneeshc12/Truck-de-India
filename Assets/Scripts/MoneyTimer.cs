using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTimer : MonoBehaviour
{
    
    public float moneyDrainRate = 0.001f;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        for (float i = 100; i >=0 ; i-=moneyDrainRate)
        {
            // Debug.Log("Money: " + i);
            yield return null;
        }
    }
    
}
