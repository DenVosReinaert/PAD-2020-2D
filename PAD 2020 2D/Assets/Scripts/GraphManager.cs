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


    // Start is called before the first frame update
    void Start()
    {
        SpawnHorizontal();
        SpawnVertical();
    }

    void SpawnHorizontal()
    {
        for (int i = 1; i < 11; i++)
        {
            GameObject horizontalLine = Instantiate(HorizontalLine, HorizontalLineContainer);
            horizontalLine.transform.localPosition = new Vector3(5, i, -1);
            horizontalLine.GetComponentInChildren<TextMeshPro>().text = i.ToString();
        }
    }

    void SpawnVertical()
    {
        for (int i = 0; i < 22; i++)
        {
            GameObject verticalLine = Instantiate(VerticalLine, VerticalLineContainer);
            verticalLine.transform.localPosition = new Vector3(0, -i, -1);
            verticalLine.GetComponentInChildren<TextMeshPro>().text = i.ToString();
        }
    }
}
