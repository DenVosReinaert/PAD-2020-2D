using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelswitch : MonoBehaviour
{
    public GameObject Panel;

    int counter;

    public void showhidePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
