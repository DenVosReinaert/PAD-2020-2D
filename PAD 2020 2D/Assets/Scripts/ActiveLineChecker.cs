using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveLineChecker : MonoBehaviour {

    public static Transform[] lines; // array of all the pipes
    public static GameObject activeLine; // the pipe that is currently active and affected by input
    public static List<GameObject> hitTheirGoal; // list of pipes that have hit their goal
    public static Dictionary<GameObject, bool> hasBCorrect; // list of pipes where the input has a correct B
    public float coefficient = 0;

    private Dictionary<GameObject, string> formulas; // list of formulas for every pipe
    private Dictionary<GameObject, bool> stretched; // list of pipes and if they are stretched
    private bool hasStretched; // boolean that tells wether active line is stretched
    private bool subtractedLife; // boolean that helps keep track of wether we have already subtracted a life or not
    private GameObject inputText; // object that has the input text

    // constants
    private const int _MaxScale = 22; // maximum length of the line when stretching
    private const float _ScaleIncrement = 0.2f; // how much the line increasing when stretching
    private const float _ScaleDecrement = 0.4f; // how much the line decreases when shrinking

    void Awake() {
        lines = new Transform[transform.childCount]; // size of the array is the amount of children
        for (int i = 0; i < lines.Length; i++) { // get the data from the children and put it in the array
            lines[i] = transform.GetChild(i);
        }
        activeLine = lines[0].gameObject; // active line is always the first object upon start
        formulas = BuildDictionary(); // make the dictionaries
        stretched = BuildStretchedDictionary();
        hasBCorrect = BuildBDictionary();
        hitTheirGoal = new List<GameObject>(); // create the list
        inputText = GameObject.Find("Formule"); // find the object that handles input text
    }

    void Update() {
        PipeCheck();
        HouseCheck();
        CheckHit();
        CheckInput();
        HandleStretching();
        bool canRun = true;
        if (SceneManager.GetActiveScene().name.Equals("TutorialIntro") && !TutorialHelper.levelHasStarted) {
            canRun = false;
        }
        if (canRun) {
            if (inputText != GameObject.Find("Formule")) {
                inputText = GameObject.Find("Formule");
            }
            string newFormula = inputText.GetComponent<Text>().text; // get the input text and store it in formula                                                       
            // formula parsing for reading
            if (!hitTheirGoal.Contains(activeLine)) { // check if the line isn't already completed
                if (!string.IsNullOrEmpty(newFormula)) { // check if the string is empty
                    if (CanParseFormula(newFormula)) { // check if the string if parsable and their arent any weird characters
                        string[] calculation = ParseFormula(newFormula); // parse the formula in the form of 1x + 3
                        formulas.Remove(activeLine); // remove line from formula's because the formula will be updated
                        formulas.Add(activeLine, GetStringFromArray(calculation));
                        HandleAngle(calculation);
                        if (hasStretched) {
                            CheckB(newFormula, calculation);
                        }
                    } else {
                        //Debug.Log("Invalid formula!");
                    }
                } else {
                    activeLine.transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
            }
        }
    }

    private void HandleAngle(string[] calculation) {
        coefficient = GetSlopeFromFormula(calculation);
        double angle = 0;
        Vector2 targetPosition = GetTargetPosition();
        double yDifference = targetPosition.y - activeLine.transform.position.y; // get the difference in y
        if (coefficient != 0) { // if the slope isn't 0, change the angle
            angle = GetAngle(calculation, yDifference);
        }
        angle += 90; // add 90 to force the player to only go to the right
        if (activeLine.transform.rotation.eulerAngles.z != (float) angle) { // if the angle is different, than what it should be, change it.
            activeLine.transform.rotation = Quaternion.Euler(0, 0, (float) angle);
        }
    }

    private void CheckB(string newFormula, string[] calculation) {
        bool answeredCorrect = false; // hasnt checked yet
        float differenceX = activeLine.transform.position.x * -1; // difference between position and 0 x, when x is zero the line crosses the y-axis
        float yPos = coefficient * differenceX;  // multiply difference to get the the difference
        int yPosRounded = (int) Mathf.Round(yPos);
        int roundedY = (int) activeLine.transform.position.y + (int) yPosRounded;
        if (!string.IsNullOrEmpty(calculation[2]) && CanParse(calculation[2])) { // check if the B isn't empty and parsable
            float beginPoint = ParseNumber(calculation[2]); // parse the number
            if (beginPoint == roundedY && calculation[1].Equals("+")) { // if parsed number && the rounded y are the same and the math operator is '+'
                answeredCorrect = true; // than player answered correctly
            } else {
                if (roundedY < 0) { // else if rounded y is lower than 0
                    if (calculation[1].Equals("-")) { // and if the math operator is '-'
                        beginPoint *= -1; // multiply by -1 so that the separate number is positive so it looks like '1x - 3' instead of '1x + -3'
                        if (beginPoint == roundedY) { // if it then equals each other answered correctly
                            answeredCorrect = true;
                        }
                    }
                }
                if (!subtractedLife && !answeredCorrect) { // if there isn't a life subtracted and hasn't answered correctly,
                    Lives.DecrementLife(); // subtract a life and make the boolean true so the player doesnt die in 5 frames
                    subtractedLife = true;
                }
            }
        } else {
            if (roundedY == 0) {
                answeredCorrect = true;
            }
        }
        int index = 2;
        int secondIndex = 3;
        bool longB = false;
        if (newFormula.Length > 1) {
            if (calculation[2].Length == 1) {
                index = newFormula.IndexOf(calculation[2], 2); // get the index number of where the B is
            } else if (calculation[2].Length == 2) {
                char[] numbers = calculation[2].ToCharArray();
                index = newFormula.IndexOf(numbers[0], 2); // get the index number of where the B is
                secondIndex = newFormula.IndexOf(numbers[1], 2);
                longB = true;
            }
            if (index > 2) {
                string newB;
                if (longB) {
                    newB = answeredCorrect ? "<color=green>" + newFormula[index].ToString() + newFormula[secondIndex].ToString() + "</color>"
                        : "<color=red>" + newFormula[index].ToString() + newFormula[secondIndex].ToString() + "</color>";
                } else {
                    newB = answeredCorrect ? "<color=green>" + newFormula[index].ToString() + "</color>" : "<color=red>" + newFormula[index].ToString() + "</color>";
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < calculation.Length; i++) {
                    if (calculation[i].Equals(calculation[2])) {
                        sb.Append(newB).Append(" ");
                    } else {
                        sb.Append(calculation[i]).Append(" ");
                    }
                }
                GameObject.Find("FormulaField").GetComponent<InputField>().text = sb.ToString().Trim();
            }
        } else {
            if (newFormula.Length == 1) {
                string temp = answeredCorrect ? "<color=green>" + newFormula + "</color>" : "<color=red>" + newFormula + "</color>";
                GameObject.Find("FormulaField").GetComponent<InputField>().text = temp;
            }
        }
        if (answeredCorrect) { // if answered correct, update in the list
            if (hasBCorrect.ContainsKey(activeLine)) {
                hasBCorrect.Remove(activeLine);
                hasBCorrect.Add(activeLine, true);
            }
        }
    }

    private float GetSlopeFromFormula(string[] formula) {
        float slope = 0;
        if (formula[0].EndsWith("x") && !formula[0].Equals("x")) { // this part retrieves the coefficient (slope) from the formula
            slope = ParseNumber(formula[0].Substring(0, formula[0].Length - 1));
        } else if (!formula[0].StartsWith("-") && !formula[0].StartsWith("+")) {
            slope = ParseNumber(formula[0]);
        } else if (formula[0].Equals("x")) {
            slope = 1;
        }
        return slope;
    }

    private string GetStringFromArray(string[] array) {
        StringBuilder sb = new StringBuilder(); // build the string for the dictionary
        for (int i = 0; i < array.Length; i++) {
            sb.Append(array[i]).Append(" ");
        }
        return sb.ToString().Trim();
    }

    public static Vector2 GetTargetPosition() {
        Vector2 targetPosition;
        switch (activeLine.name) { // target position of the is different for each line
            case "LineDrawer": // so that is handled here, for every line (switched by name) the target position is changed accordingly
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
                targetPosition = new Vector2(0, 0);
                Debug.Log("Incorrect active line name."); // if somehow the name is not any of the lines, print to the console because that shouldnt happen
                break;
        }
        return targetPosition;
    }

    private void CheckInput() {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !hasStretched && activeLine.transform.localScale.y <= 1) { // this is for when the player presses enter so that the pipe will stretched
            hasStretched = true;
            if (stretched.ContainsKey(activeLine)) {
                stretched.Remove(activeLine);
                stretched.Add(activeLine, true); // lines is stretched, so change the boolean
            }
        }
    }

    private void HandleStretching() {
        Vector3 newScale = activeLine.transform.localScale;
        if (hasStretched && activeLine.transform.localScale.y < _MaxScale) { // line hasn't reached its max yet, so it must grow
            if (!hitTheirGoal.Contains(activeLine)) {
                newScale.y += _ScaleIncrement;
            }
            activeLine.transform.localScale = newScale;
        } else if (hasStretched && activeLine.transform.localScale.y >= _MaxScale) { // it has reached its max, now its time to shrink
            hasStretched = false;
            Lives.DecrementLife(); ; // also remove a live because the pipe missed its goal
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(1, 0, 0);
        }

        if (activeLine.transform.localScale.y > 1 && !hasStretched) { // the actual stretching part is done here
            if (!hitTheirGoal.Contains(activeLine)) {
                newScale.y -= _ScaleDecrement;
            }
            activeLine.transform.localScale = newScale;
        }
        if (activeLine.transform.localScale.y < 1) {
            newScale.y = 1;
            activeLine.transform.localScale = newScale;
        }
    }

    private void CheckHit() {
        if (hitTheirGoal.Contains(activeLine) && hasBCorrect.TryGetValue(activeLine, out bool isBCorrect)) { // if current pipe has hit their goal, and their B is correct than make the text uneditable.
            if (isBCorrect) {
                if (formulas.TryGetValue(activeLine, out string formula)) {
                    GameObject.Find("FormulaField").GetComponent<InputField>().text = formula;
                    SoundManagerScript.PlaySound("Right");
                    NextLine();
                }
            } else { // if not, the line isnt complete so remove it from the 'hit their goal' list.
                hitTheirGoal.Remove(activeLine);
                SoundManagerScript.PlaySound("Wrong");
            }
        }
        if (hitTheirGoal.Count == 4) { // if the list contains 4 objects, the level is complete
            int completedLines = 0; // iterate through the hasBCorrect list to see if their B is also correct
            for (int i = 0; i < lines.Length; i++) {
                if (hasBCorrect.TryGetValue(lines[i].gameObject, out bool isBCorrecto)) {
                    if (isBCorrecto) {
                        completedLines++;
                    }
                }
            }
            if (completedLines == 4) { // if B is also correct, give the player money according to their lives, reset the lives and go to the finishedlevel scene.
                int newMoney = Interscene.instance.money + Lives.life * 100;
                Interscene.instance.money = newMoney;
                Interscene.instance.PutData(Interscene.instance.userName, newMoney);
                SceneManager.LoadScene("WinScreen");
                Lives.ResetLives();
            }
        }
    }

    private void PipeCheck() { // draw the lines if they are active or if they've hit their goal
        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].gameObject != activeLine && !hitTheirGoal.Contains(lines[i].gameObject)) {
                lines[i].gameObject.SetActive(false);
            } else {
                lines[i].gameObject.SetActive(true);
            }
        }
    }

    private void HouseCheck() { // draw the houses according to which line is active or if a house has been hit by a line that is completed
        for (int i = 0; i < lines.Length; i++) {
            if (i <= 2) {
                if (!hitTheirGoal.Contains(lines[i].gameObject)) {
                    Waypoints.waypoints[i].gameObject.SetActive(false);
                } else {
                    Waypoints.waypoints[i].gameObject.SetActive(true);
                }
                if (lines[i].gameObject == activeLine && !hitTheirGoal.Contains(lines[i].gameObject)) {
                    Waypoints.waypoints[i].gameObject.SetActive(true);
                }
            } else {
                if (lines[i].gameObject == lines[3].gameObject && lines[i].gameObject == activeLine) {
                    Objectives.objectives[1].gameObject.SetActive(true);
                } else {
                    Objectives.objectives[1].gameObject.SetActive(false);
                }
            }
        }
    }

    private static Dictionary<GameObject, string> BuildDictionary() { // setup dictionary with the formulas
        Dictionary<GameObject, string> returnItem = new Dictionary<GameObject, string>();
        for (int i = 0; i < lines.Length; i++) {
            returnItem.Add(lines[i].gameObject, "0x");
        }
        return returnItem;
    }

    private static Dictionary<GameObject, bool> BuildStretchedDictionary() { // setup dictionary for the stretched lines
        Dictionary<GameObject, bool> returnItem = new Dictionary<GameObject, bool>();
        for (int i = 0; i < lines.Length; i++) {
            returnItem.Add(lines[i].gameObject, false);
        }
        return returnItem;
    }

    private static Dictionary<GameObject, bool> BuildBDictionary() { // setup dictionary for the hasBCorrect list
        Dictionary<GameObject, bool> returnItem = new Dictionary<GameObject, bool>();
        for (int i = 0; i < lines.Length; i++) {
            returnItem.Add(lines[i].gameObject, false);
        }
        return returnItem;
    }

    void NextLine() { // method that handles going to next line
        GameObject oldLine = activeLine.gameObject; // set new activeLine
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
        if (stretched.ContainsKey(oldLine)) { // update dictionary
            stretched.Remove(oldLine);
            stretched.Add(oldLine, hasStretched);
        } else {
            stretched.Add(oldLine, hasStretched);
        }
        hasStretched = false;
        ClearFields(oldLine, activeLine.gameObject); // clear text fields
        if (!hitTheirGoal.Contains(activeLine)) { // if line isn't completed, make the text black
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(0, 0, 0);
            if (GameObject.Find("Formule").GetComponent<Text>().text.Contains("<color=green>")) {
                GameObject.Find("Formule").GetComponent<Text>().text.Replace("<color=green>", "");
            } else if (GameObject.Find("Formule").GetComponent<Text>().text.Contains("<color=red>")) {
                GameObject.Find("Formule").GetComponent<Text>().text.Replace("<color=red>", "");
            } else if (GameObject.Find("Formule").GetComponent<Text>().text.Contains("</color>")) {
                GameObject.Find("Formule").GetComponent<Text>().text.Replace("</color>", "");
            }
        }
    }

    private void ClearFields(GameObject oldLine, GameObject newLine) {
        string input = inputText.GetComponent<Text>().text; // get current text
        if (formulas.ContainsKey(oldLine) && !string.IsNullOrEmpty(input)) { // if its empty
            formulas[oldLine] = input; // retrieve data from list
        }
        string formula = "";
        if (formulas.ContainsKey(newLine)) { // if the formula list contains the newLine, get the data
            if (formulas.TryGetValue(newLine, out string value)) {
                formula = value;
            }
        }
        GameObject.Find("FormulaField").GetComponent<InputField>().text = formula; // set that data as text
    }

    public bool CanParseFormula(string formula) { // see if formula contains only digits, an x or a math operator
        foreach (char c in formula) {
            if ((c < '0' || c > '9') && (c != 'x' && c != 'X') && (c != '+' && c != '-') && c != ' ' && c != '.' && c != 'y' && c != '=') { // if not any of these values, then its not a correct formula so return false
                return false;
            }
        }
        if (formula.Length == 1) { // if there is only one character it shouldnt be anything else than a number or it isnt parsable
            char[] number = formula.ToCharArray();
            if ((number[0] < '0' || number[0] > '9') && number[0] != 'x' && number[0] != 'X') {
                return false;
            }
        }
        return true;
    }

    public bool CanParse(string number) { // check if its a number
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
            if (formula.Contains(" * ")) { // remove multiply signs
                formula = formula.Replace(" * ", "");
            } else if (formula.Contains("* ")) {
                formula = formula.Replace(" *", "");
            } else if (formula.Contains("* ")) {
                formula = formula.Replace("* ", "");
            } else {
                formula = formula.Replace("*", "");
            }
        }
        if (formula.Contains("y=")) { // remove 'y=' from the formula
            formula = formula.Replace("y=", "");
        } else if (formula.Contains("y =")) {
            formula = formula.Replace("y =", "");
        }
        if (formula.Contains(" ")) { // with this form in mind: -1x - 3
            content = formula.Split(' ');
            if (content.Length != 3) {
                if (content.Length == 2) {
                    string firstValue = content[0];
                    if (firstValue.Equals("-x") || firstValue.Equals("x")) {
                        firstValue = firstValue.Replace("x", "1x");
                    }
                    if (firstValue.Contains("x") && !firstValue.EndsWith("x")) { // if the first bit has x in it, but doesnt end with it
                        string secondValue = content[1]; // make it end with x
                        int xIndex = content[0].IndexOf('x');
                        string newValue = content[0].Substring(xIndex);
                        string newFirstValue = content[0].Substring(xIndex);
                        content = new string[3];
                        content[0] = newFirstValue;
                        content[1] = newValue; // math operator was behind the x so it will be the second object in the array
                        content[2] = secondValue;
                    }
                    if (content[1].StartsWith("-") || content[1].StartsWith("+")) { // if the second bit starts with the math operator
                        char[] secondValue = content[1].ToCharArray(0, content[1].Length);
                        string newSecondValue = secondValue[0].ToString();
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < secondValue.Length; i++) {
                            sb.Append(secondValue[i]);
                        }
                        string restOfFormula = sb.ToString().Trim();
                        content = new string[3];
                        content[0] = firstValue;
                        content[1] = newSecondValue; // make split the math operator from the rest
                        content[2] = restOfFormula;
                    }
                } else {
                    Debug.Log("Unsupported formula format.");
                }
            } else {
                string value = content[0];
                if (value.Equals("-x") || value.Equals("x")) {
                    value = value.Replace("x", "1x");
                    content[0] = value;
                }
            }
        } else { // form: -1x-3
            if (formula.EndsWith("x")) {
                formula += " + 0";
            }
            if (formula.StartsWith("x") || formula.StartsWith("-x")) {
                formula = formula.Replace("x", "1x");
            }
            if (formula.Equals("x")) {
                string temp = "1x + 0";
                content = temp.Split(' ');
            } else {
                StringBuilder sb = new StringBuilder();
                char[] formule = formula.ToCharArray(); // if there's no spaces at all, make the spaces yourself
                for (int i = 0; i < formule.Length; i++) {
                    if (formule[i] == 'x') { // space if current char is x
                        sb.Append(formule[i]).Append(" ");
                    } else if ((formule[i] == '-' || formule[i] == '+') && i > 0) { // if math operator and the position is greater than 0, space
                        sb.Append(formule[i]).Append(" ");
                    } else {
                        sb.Append(formule[i]); // normally, no space
                    }
                }
                content = sb.ToString().Trim().Split(' '); // return the string but trim it so the last space goes away and split it at the spaces
            }
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
        if (formula.Length > 3) {
            b = ParseNumber(formula[2]);
        }

        float x = 0;

        if (activeLine.gameObject.name.Equals("LineDrawer")) {
            x = Waypoints.waypoints[0].position.x;
        } else if (activeLine.gameObject.name.Equals("Waypoints1T2")) {
            x = Waypoints.waypoints[1].position.x;
        } else if (activeLine.gameObject.name.Equals("Waypoints2T3")) {
            x = Waypoints.waypoints[2].position.x;
        } else if (activeLine.gameObject.name.Equals("GoalLine")) {
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

    public double GetAngle(string[] formula, double differenceOnYAxis) { // get the angle according to formula
        string toParse;
        if (formula[0].Equals("x") || formula[0].Equals("-x")) {
            toParse = formula[0].Replace("x", "1x");
            toParse = toParse.Replace("x", "");
        } else {
            if (formula[0].Contains("x")) {
                toParse = formula[0].Replace("x", "");
            } else {
                toParse = "0";
                Debug.Log("No x value to get an angle.");
            }
        }
        double coefficient = 0d;
        if (CanParse(toParse)) { // parse angle
            coefficient = double.Parse(formula[0].Replace("x", ""));
        } else {
            Debug.Log("Cannot parse angle!");
        }
        // y = ax
        // x = y / a
        double xDistance = differenceOnYAxis / coefficient;
        double hoek = differenceOnYAxis / xDistance; // tan(hoek a) = overstaande / aanliggende
        double angleInRadians = Mathf.Atan((float) hoek); // hoek is returned in radians, a = tan^-1(overstaande/aanliggende)
        double angle = angleInRadians * (180d / Math.PI); // convert radians to degrees
        return angle;
    }

    private static float ParseNumber(string intToParse) { // parse a number
        float parsed = 0;
        try {
            parsed = float.Parse(intToParse, NumberStyles.Float, CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            Debug.Log("Error parsing string: " + e);
        }
        return parsed;
    }
}
