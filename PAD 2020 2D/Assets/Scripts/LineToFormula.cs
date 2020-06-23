using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineToFormula : MonoBehaviour {

    private InputField inputField; // inputfield object

    // constants
    private const string BasicFormula = "ax + b"; // this is the basic formula used in linear algebra

    void Awake() {
        inputField = GameObject.Find("FormulaField").GetComponent<InputField>(); // get the object
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            bool replaced = false;
            string input = inputField.text;
            if (input.Contains("<color=green>")) {
                input = input.Replace("<color=green>", "");
                replaced = true;
            } 
            if (input.Contains("<color=red>")) {
                input = input.Replace("<color=red>", "");
                replaced = true;
            } 
            if (input.Contains("</color>")) {
                input = input.Replace("</color>", "");
                replaced = true;
            }
            if (input.Contains("</color")) {
                input = input.Replace("</color", "");
                replaced = true;
            }
            if (input.Length > 1 && replaced) {
                input = input.Substring(0, input.Length - 1);
            }
            inputField.text = input;
        }
        if (!string.IsNullOrEmpty(inputField.text)) { // if input is not null, set the placeholder to nothing
            GameObject.Find("Placeholder").GetComponent<Text>().text = "";
        } else { // if it is, set it to the basic formula
            GameObject.Find("Placeholder").GetComponent<Text>().text = BasicFormula;
        }
    }
}
