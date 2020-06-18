﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbuttons2 : MonoBehaviour
{
    public Button buybutton;

    void Update()
    {
        if (Interscene.instance.money >= 5000)
        { // 5000 = the price for a new pipe
            buybutton.interactable = true; //if money amount is >= 5000 buybuttons are interactable
        }
        else
        {
            buybutton.interactable = false; //if money amount is < 5000 buybuttons are not interactable
        }
    }
}