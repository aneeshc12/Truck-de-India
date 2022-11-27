using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moneyCount : MonoBehaviour
{
    private TextMeshProUGUI moneyText;
    public MoneyTimer moneyTimer;
    public string money;

    void Start()
    {
        moneyTimer = GameObject.Find("Money Counter").GetComponent<MoneyTimer>();
        moneyText = GetComponent<TextMeshProUGUI>();
        print("SUIIII"+moneyTimer.money);
    }

    void Update()
    {
        money = moneyTimer.money.ToString("F2");
        moneyText.SetText("Money:" + money);
    }
}
