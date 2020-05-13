using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{

    public GameObject[] hearts;
    public static int life = 3;


    void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
            //game over screen
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void loselife(int d) // d = damage
    {
        life -= d;
    }
}
