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
        for (int i = 0; i < 11; i++)
        {
            GameObject horizontalLine = Instantiate(HorizontalLine, HorizontalLineContainer);
            horizontalLine.transform.localPosition = new Vector3(5,i);
            horizontalLine.GetComponentInChildren<TextMeshPro>().text = i.ToString();
        }
    }

    void SpawnVertical()
    {
        for (int i = 0; i < 21; i++)
        {
            GameObject verticalLine = Instantiate(VerticalLine, VerticalLineContainer);
            verticalLine.transform.localPosition = new Vector3(0, -i);
            verticalLine.GetComponentInChildren<TextMeshPro>().text = i.ToString();
        }
    }
}
