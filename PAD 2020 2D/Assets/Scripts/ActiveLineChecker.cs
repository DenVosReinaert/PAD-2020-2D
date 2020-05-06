using System;
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
    private Dictionary<GameObject, string> formulas;
    public float coefficient;

    private GameObject inputText;

    void Awake() {
        lines = new Transform[transform.childCount];
        for (int i = 0; i < lines.Length; i++) {
            lines[i] = transform.GetChild(i);
        }
        activeLine = lines[0].gameObject;
        formulas = BuildDictionary();
        inputText = GameObject.Find("Formule");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) { // go to next line to edit when enter key is pressed
            NextLine();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) { // go to previous line to edit when backspace is pressed
            PreviousLine();
        }
        string newFormula = inputText.GetComponent<Text>().text;
        // formula parsing for reading
        if (newFormula != "") {
            if (CanParseFormula(newFormula)) {
                string[] calculation = ParseFormula(newFormula);
                coefficient = ParseNumber(calculation[0].Substring(0, calculation[0].Length-1));
                KeyValuePair<float, float> points = GetPoints(calculation);
                Debug.Log(points.Key + " " + points.Value);
                double angle = 0;
                Vector2 targetPosition = new Vector2(0, 0);
                switch (activeLine.name) {
                    case "LineDrawer":
                        targetPosition = Waypoints.waypoints[0].position;
                        break;
                    case "Waypoints1T2":
                        targetPosition = Waypoints.waypoints[1].position;
                        break;
                    case "Waypoints2T3":
                        targetPosition = Waypoints.waypoints[2].position;
                        break;
                    case "GoalLine":
                        targetPosition = Objectives.objectives[1].position;
                        break;
                    default:
                        Debug.Log("Incorrect active line name.");
                        break;
                }
                //double overstaande = targetPosition.x - activeLine.transform.position.x;
                double yDifference = targetPosition.y - activeLine.transform.position.y;
                if (coefficient != 0) {
                    angle = GetAngle(calculation, yDifference);
                }
                angle += 90;
                Debug.Log(angle + " " + activeLine.transform.rotation.eulerAngles.z);
                if (activeLine.transform.rotation.eulerAngles.z != (float) angle) {
                    activeLine.transform.rotation = Quaternion.Euler(0, 0, (float) angle);
                }
                // TODO: Stretch object & input rotation
            } else {
                Debug.Log("Invalid formula!");
            }
        }
    }

    //for saving entered formulas
    private static Dictionary<GameObject, string> BuildDictionary() {
        Dictionary<GameObject, string> returnItem = new Dictionary<GameObject, string>();
        for (int i = 0; i < lines.Length; i++) {
            returnItem.Add(lines[i].gameObject, "");
        }
        return returnItem;
    }

    void NextLine() { // method that handles going to next line
        GameObject oldLine = activeLine.gameObject;
        if (activeLine == lines[0].gameObject) {
            if (lines[1] != null) {
                activeLine = lines[1].gameObject;
            }
        } else if (activeLine == lines[1].gameObject) {
            if (lines[2] != null) {
                activeLine = lines[2].gameObject;
            }
        } else if (activeLine == lines[2].gameObject) {
            if (lines[3] != null) {
                activeLine = lines[3].gameObject;
            }
        }
        ClearFields(oldLine, activeLine.gameObject);
    }

    void PreviousLine() { // method that handles going back to previous line
        GameObject oldLine = activeLine.gameObject;
        if (activeLine == lines[1].gameObject) {
            if (lines[0] != null) {
                activeLine = lines[0].gameObject;
            }
        } else if (activeLine == lines[2].gameObject) {
            if (lines[1] != null) {
                activeLine = lines[1].gameObject;
            }
        } else if (activeLine == lines[3].gameObject) {
            if (lines[2] != null) {
                activeLine = lines[2].gameObject;
            }
        }
        ClearFields(oldLine, activeLine.gameObject);
    }

    private void ClearFields(GameObject oldLine, GameObject newLine) {
        string input = inputText.GetComponent<Text>().text;
        if (formulas.ContainsKey(oldLine) && !string.IsNullOrEmpty(input)) {
            formulas[oldLine] = input;
        }
        string formula = "";
        if (formulas.ContainsKey(newLine)) {
            if (formulas.TryGetValue(newLine, out string value)) {
                formula = value;
            }
        }
        GameObject.Find("FormulaField").GetComponent<InputField>().text = formula;
    }

    public bool CanParseFormula(string formula) { // see if formula contains only digits, an x or a math operator
        foreach (char c in formula) {
            if ((c < '0' || c > '9') && (c != 'x' && c != 'X') && (c != '+' && c != '-') && c != ' ' && c != '.' && c != 'y' && c != '=') { // if not any of these values, then its not a correct formula so return false
                return false;
            }
        }
        if (formula.Length == 1) {
            char[] number = formula.ToCharArray();
            if (number[0] < '0' || number[0] > '9') {
                return false;
            }
        }
        return true;
    }

    public bool CanParse(string number) {
        foreach (char c in number) {
            if ((c < '0' || c > '9') && (c != '-' && c != '+') && c != '.') {
                return false;
            }
        }
        if (number.Length == 1) {
            char[] numbers = number.ToCharArray();
            if (numbers[0] < '0' || numbers[0] > '9') {
                return false;
            }
        }
        return number.IndexOf("-") <= 0;
    }

    public string[] ParseFormula(string formula) {
        string[] content; // get the contents in the form or -1x, -, 3 when formula is -1x - 3
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
        if (formula.Contains("y=")) {
            formula.Replace("y=", "");
        } else if (formula.Contains("y =")) {
            formula.Replace("y =", "");
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
                } else if ((formule[i] == '-' || formule[i] == '+') && i > 0) {
                    sb.Append(formule[i]).Append(" ");
                } else {
                    sb.Append(formule[i]);
                }
            }
            content = sb.ToString().Trim().Split(' ');
        }
        return content;
    }

    static KeyValuePair<float, float> GetPoints(string[] formula) { // return in system: (x, y)
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

        if (formula.Length > 3)
        {
            b = ParseNumber(formula[2]);
        }

        float x = 0;

        if (activeLine.gameObject.name.Equals("LineDrawer"))
        {
            x = Waypoints.waypoints[0].position.x;
        }
        else if (activeLine.gameObject.name.Equals("Waypoints1T2"))
        {
            x = Waypoints.waypoints[1].position.x;
        }
        else if (activeLine.gameObject.name.Equals("Waypoints2T3"))
        { 
            x = Waypoints.waypoints[2].position.x;
        }
        else if (activeLine.gameObject.name.Equals("GoalLine"))
        {
            x = Objectives.objectives[1].position.x;
        }

        float ax = a * x;
        float y;
        if (mathOperator != "" && b != 0) {
            y = mathOperator == "-" ? ax - b : ax + b;
        } else {
            y = ax;
        }
        points = new KeyValuePair<float, float>(x, y);
        return points;
    }

    public double GetAngle(string[] formula, double diffenceOnYAxis) {
        double angle = 0;
        string toParse = formula[0].Replace("x", "");
        int coefficient = 0;
        if (CanParse(toParse)) {
            coefficient = int.Parse(formula[0].Replace("x", ""));
        } else {
            Debug.Log("Cannot parse angle!");
        }
        // y = ax
        // x = y / a
        double xDistance = diffenceOnYAxis / coefficient;
        double hoek = xDistance / diffenceOnYAxis; // overstaande / aanliggende
        double angleInRadians = Mathf.Atan((float) hoek);
        angle = angleInRadians * (180 / Math.PI);
        return angle;
    }

    public double GetAngle(string[] formula, double overstaande, double aanliggende) {
        double angle = 0;
        double hoek = overstaande / aanliggende;
        double angleInRadians = Mathf.Atan((float) hoek);
        angle = angleInRadians * (180 / Math.PI);
        return angle;
    }

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
