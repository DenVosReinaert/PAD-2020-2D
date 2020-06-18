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
    private int verticalLines = 20;
    private int horizontalLines = 11;
    private const float multiplier = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnHorizontal();
        SpawnVertical();
    }

    void SpawnHorizontal()
    {
        // A simple for loop 
        for (int i = 0; i < horizontalLines; i++)
        {
            GameObject horizontalLine = Instantiate(HorizontalLine, HorizontalLineContainer);
            // This is our starting position
            horizontalLine.transform.localPosition = new Vector3(5, i, -1);
            int j = i - 5;
            // Not only do the lines need to be on the right place but also the text displaying the numerics
            horizontalLine.GetComponentInChildren<TextMeshPro>().text = j.ToString();
            // Zero is the middle of all the lines so we want that line to be thicker then the others
            if (j == 0)
            {
                horizontalLine.GetComponentInChildren<LineRenderer>().startWidth = multiplier;
                horizontalLine.GetComponentInChildren<LineRenderer>().endWidth = multiplier;
            }

        }
    }

    // Basicly the same but then vertical
    void SpawnVertical()
    {
        for (int i = -1; i < verticalLines; i++)
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
