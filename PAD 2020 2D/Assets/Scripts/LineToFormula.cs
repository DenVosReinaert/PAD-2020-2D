using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineToFormula : MonoBehaviour {
    
    //private Vector3 startPoint;
    //private Vector3 endPoint;
    private LineRenderer activeLine;

    //public Text formula;
    private InputField inputField;

    // constants
    private const string BasicFormula = "y = ax + b";

    void Awake() {
        activeLine = ActiveLineChecker.activeLine.GetComponent<LineRenderer>();
        //formula = GetComponent<Text>();
        inputField = GameObject.Find("FormulaField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update() {
        activeLine = ActiveLineChecker.activeLine.GetComponent<LineRenderer>();
        //startPoint = activeLine.GetPosition(0);
        //endPoint = activeLine.GetPosition(1);
        //formula.text = 
        //Debug.Log("y = " + DrawLine.GetFormulaFromVector(startPoint, endPoint));
        //inputField.GetComponent<Text>().text = "y = " + DrawLine.GetFormulaFromVector(startPoint, endPoint);

        if (!string.IsNullOrEmpty(inputField.text)) {
            //inputField.placeholder.GetComponent<Text>().text = "";
            GameObject.Find("Placeholder").GetComponent<Text>().text = "";
        } else {
            //inputField.placeholder.GetComponent<Text>().text = BasicFormula;
            GameObject.Find("Placeholder").GetComponent<Text>().text = BasicFormula;
        }
        //inputField.text = "y = " + DrawLine.GetFormulaFromVector(startPoint, endPoint);
    }
}
