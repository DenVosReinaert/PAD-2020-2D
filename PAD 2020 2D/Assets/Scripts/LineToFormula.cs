using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineToFormula : MonoBehaviour {

    private InputField inputField; // inputfield object

    // constants
    private const string BasicFormula = "y = ax + b"; // this is the basic formula used in linear algebra

    void Awake() {
        inputField = GameObject.Find("FormulaField").GetComponent<InputField>(); // get the object
    }

    // Update is called once per frame
    void Update() {
        if (!string.IsNullOrEmpty(inputField.text)) { // if input is not null, set the placeholder to nothing
            GameObject.Find("Placeholder").GetComponent<Text>().text = "";
        } else { // if it is, set it to the basic formula
            GameObject.Find("Placeholder").GetComponent<Text>().text = BasicFormula;
        }
    }
}
