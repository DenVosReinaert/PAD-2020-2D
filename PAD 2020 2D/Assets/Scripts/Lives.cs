using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lives : MonoBehaviour
{

    public GameObject[] hearts;
    public static int life = 5;


    void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
            SceneManager.LoadScene("FinishedLevel");
            life = 5;
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        } else if (life < 4) {
            Destroy(hearts[3].gameObject);
        } else if (life < 5) {
            Destroy(hearts[4].gameObject);
        }
    }

    public void loselife(int d) // d = damage
    {
        life -= d;
    }
}
