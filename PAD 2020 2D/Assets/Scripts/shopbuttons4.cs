using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbuttons4 : MonoBehaviour
{
    public Button buybutton;

    void Update()
    {
        if (Interscene.instance.money >= 10000)
        { // 10000 = the price for a new pipe
            buybutton.interactable = true; //if money amount is >= 10000 buybuttons are interactable
        }
        else
        {
            buybutton.interactable = false; //if money amount is < 10000 buybuttons are not interactable
        }
    }
}