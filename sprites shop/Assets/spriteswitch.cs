/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteswitch : MonoBehaviour
{
    private Sprite;
    private Sprite greenPipe, silverPipe;//, bluePipe, redPipe, goldPipe; 

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        greenPipe = Resources.Load<Sprite>("pipe");
        silverPipe = Resources.Load<Sprite>("silver pipe");
        //bluePipe = Resources.Load<Sprite>("blue pipe");
        //redPipe = Resources.Load<Sprite>("red pipe");
        //goldPipe = Resources.Load<Sprite>("gold pipe");


    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rend.sprite == greenPipe)
                rend.sprite == silverPipe;
            else if (rend.sprite == silverPipe)
                rend.sprite = greenPipe;

        }
    }
}*/