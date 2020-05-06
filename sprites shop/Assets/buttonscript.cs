using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public Button buybutton;

    void Update()
    {
        if (money.moneyAmount >= 2000)
            buybutton.interactable = true;

        else

            buybutton.interactable = false;
    }
}