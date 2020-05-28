using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbuttons : MonoBehaviour
{
    public Button buybutton;

    void Update() {
        if (PlayerPrefs.GetInt("Money") >= 2000) { // 2000 = the price for a new pipe
            buybutton.interactable = true; //if money amount is >= 2000 buybuttons are interactable
        } else {
            buybutton.interactable = false; //if money amount is < 2000 buybuttons are not interactable
        }
    }
}