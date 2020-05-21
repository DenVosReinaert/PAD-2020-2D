using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public GameObject HorizontalLine;
    public GameObject VerticalLine;
    public Transform HorizontalLineContainer;
    public Transform VerticalLineContainer;
    private const float multiplier = 0.15f;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnHorizontal();
        SpawnVertical();
    }

    void SpawnHorizontal()
    {
        for (int i = 0; i < 11; i++)
        {
            GameObject horizontalLine = Instantiate(HorizontalLine, HorizontalLineContainer);
            horizontalLine.transform.localPosition = new Vector3(5, i, -1);
            int j = i -5 ;
            horizontalLine.GetComponentInChildren<TextMeshPro>().text = j.ToString();
            if (j == 0)
            {
                horizontalLine.GetComponentInChildren<LineRenderer>().startWidth = multiplier;
                horizontalLine.GetComponentInChildren<LineRenderer>().endWidth = multiplier;
            }

        }
    }

    void SpawnVertical()
    {
        for (int i = -1; i < 20; i++)
        {
            GameObject verticalLine = Instantiate(VerticalLine, VerticalLineContainer);
            verticalLine.transform.localPosition = new Vector3(0, -i, -1);
            int j = i - 9;
            verticalLine.GetComponentInChildren<TextMeshPro>().text = j.ToString();
            if (j == 0)
            {
                verticalLine.GetComponentInChildren<LineRenderer>().startWidth = multiplier;
                verticalLine.GetComponentInChildren<LineRenderer>().endWidth = multiplier;
            }
        }
    }
}
