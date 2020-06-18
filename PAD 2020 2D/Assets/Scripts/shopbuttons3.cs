using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbuttons3 : MonoBehaviour
{
    public Button buybutton;

    void Update()
    {
        if (Interscene.instance.money >= 7500)
        { // 7500 = the price for a new pipe
            buybutton.interactable = true; //if money amount is >= 7500 buybuttons are interactable
        }
        else
        {
            buybutton.interactable = false; //if money amount is < 7500 buybuttons are not interactable
        }
    }
}