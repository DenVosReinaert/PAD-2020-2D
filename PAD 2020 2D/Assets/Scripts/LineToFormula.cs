using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineToFormula : MonoBehaviour {

    private InputField inputField;

    // constants
    private const string BasicFormula = "y = ax + b";

    void Awake() {
        inputField = GameObject.Find("FormulaField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update() {
        if (!string.IsNullOrEmpty(inputField.text)) {
            GameObject.Find("Placeholder").GetComponent<Text>().text = "";
        } else {
            GameObject.Find("Placeholder").GetComponent<Text>().text = BasicFormula;
        }
    }
}
