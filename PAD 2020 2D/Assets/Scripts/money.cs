using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{

    public static int moneyAmount;
    public Text moneyText;

    void Start()
    {
        updateMoneyUI();
    }
    public void addMoney(int xAmount)
    {
        moneyAmount += xAmount;
        updateMoneyUI();
    }

    public void ReduceMoney(int yAmount)
    {
        moneyAmount -= yAmount;
        updateMoneyUI();
    }

    void updateMoneyUI()
    {
        moneyText.text = "Money: $" + moneyAmount.ToString();
    }


}