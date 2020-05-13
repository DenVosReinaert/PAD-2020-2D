using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour {

    public static Transform[] hazards;
    public int lives;

    void Awake() {
        hazards = new Transform[transform.childCount];
        for (int i = 0; i < hazards.Length; i++) {
            hazards[i] = transform.GetChild(i);
        }
    }

    void LiveChecker()
    {
        if (lives > 0)
        {
            --lives;
        }

        if (lives == 0)
        { 
            //switch scene to game over if zero lives has been reached.
        }
    }

    private void OnCollisonEnter2D(Collision other)
    {
         {
            //check for collision if the line hits hazard
            if (name.Equals("LineDrawer"))
            {
                if (!other.gameObject.name.Equals("Hazard"))
                {
                    return;
                }
            }
        }
    }
}
