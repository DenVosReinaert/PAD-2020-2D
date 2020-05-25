using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbuttons : MonoBehaviour
{
    public Button buybutton;

    void Update() {
        if (PlayerPrefs.GetInt("Money") >= 2000) {
            buybutton.interactable = true;
        } else {
            buybutton.interactable = false;
        }
    }
}