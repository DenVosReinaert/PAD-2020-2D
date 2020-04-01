﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.UI;

public class ActiveLineChecker : MonoBehaviour {

    public static Transform[] lines;
    public static GameObject activeLine;

    void Awake() {
        lines = new Transform[transform.childCount];
        for (int i = 0; i < lines.Length; i++) {
            lines[i] = transform.GetChild(i);
        }
        activeLine = lines[0].gameObject;
    }

    void Update() {
        for (int i = 0; i < lines.Length; i++) {
            LineRenderer lineRender = lines[i].GetComponent<LineRenderer>();
            if (lines[i].gameObject != activeLine) {
                lineRender.startColor = Color.red;
                lineRender.endColor = Color.red;
            } else {
                lineRender.startColor = Color.blue;
                lineRender.endColor = Color.blue;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { // go to next line to edit when enter key is pressed
            nextLine();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) { // go to previous line to edit when backspace is pressed
            previousLine();
        }
        string xToParse = GameObject.Find("xText").GetComponent<Text>().text;
        string yToParse = GameObject.Find("yText").GetComponent<Text>().text;
        float x = 0;
        float y = 0;
        if (!String.IsNullOrEmpty(xToParse) && CanParse(xToParse)) {
            x = ParseNumber(xToParse);
        }
        if (!String.IsNullOrEmpty(yToParse) && CanParse(yToParse)) {
            y = ParseNumber(yToParse);
        }
        activeLine.GetComponent<LineRenderer>().SetPosition(1, new Vector2(x, y));
        /*newFormula = inputText.GetComponent<Text>().text;
        string lengthInput = GameObject.Find("LengthText").GetComponent<Text>().text;
        float length = 0;
        if (!String.IsNullOrEmpty(lengthInput) && CanParse(lengthInput)) {
            length = ParseNumber(lengthInput);
        }
        // formula parsing for reading
        if (newFormula != "") {
            if (CanParseFormula(newFormula)) {
                string[] calculation = ParseFormula(newFormula);
                KeyValuePair<float, float> points = GetPoints(calculation, length);
                Debug.Log(length + ", " + points.Key + " " + points.Value);
                activeLine.GetComponent<LineRenderer>().SetPosition(1, new Vector2(points.Key, points.Value));
            } else {
                Debug.Log("Invalid formula!");
            }
        }*/
    }

    void nextLine() { // method that handles going to next line
        if (activeLine == lines[0].gameObject) {
            if (lines[1] != null) activeLine = lines[1].gameObject;
        } else if (activeLine == lines[1].gameObject) {
            if (lines[2] != null) activeLine = lines[2].gameObject;
        } else if (activeLine == lines[2].gameObject) {
            if (lines[3] != null) activeLine = lines[3].gameObject;
        }
        clearFields();
    }

    void previousLine() { // method that handles going back to previous line
        if (activeLine == lines[1].gameObject) {
            if (lines[0] != null) activeLine = lines[0].gameObject;
        } else if (activeLine == lines[2].gameObject) {
            if (lines[1] != null) activeLine = lines[1].gameObject;
        } else if (activeLine == lines[3].gameObject) {
            if (lines[2] != null) activeLine = lines[2].gameObject;
        }
        clearFields();
    }

    private void clearFields() {
        GameObject.Find("xText").GetComponent<Text>().text = "";
        GameObject.Find("yText").GetComponent<Text>().text = "";
    }

    /*public bool CanParseFormula(string formula) { // see if formula contains only digits, an x or a math operator
        foreach (char c in formula) {
            if ((c < '0' || c > '9') && (c != 'x' && c != 'X') && (c != '+' && c != '-') && c != ' ' && c != '.') { // if not any of these values, then its not a correct formula so return false
                return false;
            }
        }
        if (formula.Length == 1) {
            char[] number = formula.ToCharArray();
            if (number[0] < '0' || number[0] > '9') return false;
        }
        return true;
    }*/

    public bool CanParse(string number) {
        foreach (char c in number) {
            if ((c < '0' || c > '9') && (c != '-' && c != '+') && c != '.') {
                return false;
            }
        }
        if (number.Length == 1) {
            char[] numbers = number.ToCharArray();
            if (numbers[0] < '0' || numbers[0] > '9') return false;
        }
        return true;
    }

    /*public string[] ParseFormula(string formula) {
        string[] content = new string[15]; // get the contents in the form or -1x, -, 3 when formula is -1x - 3
        if (formula.Contains("*")) { // form: -1*x-3 or -1 * x - 3 or -1*x - 3
            if (formula.Contains(" * ")) {
                formula.Replace(" * ", "");
            } else if (formula.Contains("* ")) {
                formula.Replace(" *", "");
            } else if (formula.Contains("* ")) {
                formula.Replace("* ", "");
            } else {
                formula.Replace("*", "");
            }
        }
        if (formula.Contains(" ")) { // with this form in mind: -1x - 3
            content = formula.Split(' ');
            if (content.Length != 3) {
                if (content.Length == 2) {
                    string firstValue = content[0];
                    if (content[0].Contains("x") && !content[0].EndsWith("x")) {
                        string secondValue = content[1];
                        int xIndex = content[0].IndexOf('x');
                        string newValue = content[0].Substring(xIndex);
                        string newFirstValue = content[0].Substring(xIndex);
                        content = new string[3];
                        content[0] = newFirstValue;
                        content[1] = newValue;
                        content[2] = secondValue;
                    }
                    if (content[1].StartsWith("-") || content[1].StartsWith("+")) {
                        char[] secondValue = content[1].ToCharArray(0, content[1].Length);
                        string newSecondValue = secondValue[0].ToString();
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < secondValue.Length; i++) {
                            sb.Append(secondValue[i]);
                        }
                        string restOfFormula = sb.ToString().Trim();
                        content = new string[3];
                        content[0] = firstValue;
                        content[1] = newSecondValue;
                        content[2] = restOfFormula;
                    }
                } else {
                    Debug.Log("Unsupported formula format.");
                    // TODO: Support format?
                }
            }
        } else { // form: -1x-3
            StringBuilder sb = new StringBuilder();
            char[] formule = formula.ToCharArray();
            for (int i = 0; i < formule.Length; i++) {
                if (formule[i] == 'x') {
                    sb.Append(formule[i]).Append(" ");
                } else if ((formule[i] == '-' || formule[i] == '+') && i > 2) {
                    sb.Append(formule[i]).Append(" ");
                } else {
                    sb.Append(formule[i]);
                }
            }
            content = sb.ToString().Trim().Split(' ');
        }
        return content;
    }

    static KeyValuePair<float, float> GetPoints(string[] formula, float length) { // return in system: (x, y)
        KeyValuePair<float, float> points;
        if (formula[0].Contains("x")) {
            formula[0] = formula[0].Replace("x", "");
        }
        // y = ax + b
        float a = ParseNumber(formula[0]);
        string mathOperator = "";
        float b = 0;
        if (formula.Length > 2) { 
            mathOperator = formula[1];
            
        } 
        if (formula.Length > 3) {
            b = ParseNumber(formula[2]);
        }
        float ax = a * length;
        float y = 0;
        if (mathOperator != "" && b != 0) {
            y = mathOperator == "-" ? ax - b : ax + b;
        } else {
            y = ax;
        }
        points = new KeyValuePair<float, float>(length, y);
        return points;
    }*/

    private static float ParseNumber(string intToParse) {
        float parsed = 0;
        try {
            parsed = float.Parse(intToParse, NumberStyles.Float, CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            Debug.Log("Error parsing string: " + e);
        }
        return parsed;
    }
}
