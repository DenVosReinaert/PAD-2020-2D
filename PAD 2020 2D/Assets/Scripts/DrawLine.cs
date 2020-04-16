using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour {

    void Awake() {
        LineRenderer renderer = GetComponent<LineRenderer>(); // get the component from unity
        renderer.positionCount = 2; // set amount of positions
        if (gameObject.name.Equals("LineDrawer")) {
            renderer.SetPosition(0, Objectives.objectives[0].position);
            renderer.SetPosition(1, new Vector3(Objectives.objectives[0].position.x + 1, Objectives.objectives[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints1T2")) {
            renderer.SetPosition(0, Waypoints.pipes[0].position);
            renderer.SetPosition(1, new Vector3(Waypoints.pipes[0].position.x + 1, Waypoints.pipes[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints2T3")) {
            renderer.SetPosition(0, Waypoints.pipes[1].position);
            renderer.SetPosition(1, new Vector3(Waypoints.pipes[1].position.x + 1, Waypoints.pipes[1].position.y - 1, 0f));
        } else if (gameObject.name.Equals("GoalLine")) {
            renderer.SetPosition(0, Waypoints.pipes[2].position);
            renderer.SetPosition(1, new Vector3(Waypoints.pipes[2].position.x + 1, Waypoints.pipes[2].position.y - 1, 0f));
        }
    }

    public static string GetFormulaFromVector(Vector2 startPos, Vector2 endPos) {
        string formule;
        // algebra
        // y = ax + b, where a = dy / dx
        if (startPos != endPos) {
            float dy = endPos.y - startPos.y;
            float dx = endPos.x - startPos.x;
            float coefficient = dy / dx;
            // use mousePos for y & x, multiply coefficient by x and subtract result from y, that way you have b.
            float startPoint;
            float ax = coefficient * endPos.x; // coefficient calculation
            startPoint = endPos.y - ax; // calculation of b (b = y - ax)
            string mathOperator = (startPoint > 0 ? " + " : " - "); // get the math operator for the equation depending on the value of startpoint (b)
            float roundedStart = Mathf.Ceil(startPoint); // get rid of redundant '-' when b is negative
            string start = Mathf.Ceil(startPoint).ToString();
            if (roundedStart <= 0) {
                start = start.Substring(1).Trim(); // remove the redundant minus
            }
            formule = roundedStart != 0 ? coefficient.ToString("F1") + "x" + mathOperator + start : coefficient.ToString("F1") + "x";
        } else {
            formule = "Geen formule";
        }
        return formule;
    }

    public static string GetFormulaFromVector(Vector3 startPos, Vector3 endPos) {
        return GetFormulaFromVector(new Vector2(startPos.x, startPos.y), new Vector2(endPos.x, endPos.y));
    }
}
