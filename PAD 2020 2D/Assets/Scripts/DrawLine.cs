using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    private static float currentCoefficient = 0;
    public Vector3 dir;
    public Vector2 Middle = new Vector2(0, 0);
    public float offsetSource = 2.982594f;
    public bool rightLine = false;
    public GameObject cube;

    void Awake() {
        CheckPosition();
    }

    void Update() {
        CheckPosition();
    }
       
    public static string GetFormulaFromVector(Vector2 startPos, Vector2 endPos) {
        string formule;
        // algebra
        // y = ax + b, where a = dy / dx
        if (startPos != endPos) {
            float dy = endPos.y - startPos.y;
            float dx = endPos.x - startPos.x;
            float coefficient = dy / dx;
            currentCoefficient = coefficient;
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

    public static float getCurrentCoefficient() {
        return currentCoefficient;
    }

    private void CheckPosition() {
        if (gameObject.name.Equals("LineDrawer")) {
            transform.position = new Vector2(Objectives.objectives[0].position.x, Objectives.objectives[0].position.y);
        } else if (gameObject.name.Equals("Waypoints1T2")) {
            transform.position = new Vector2(Waypoints.waypoints[0].position.x, Waypoints.waypoints[0].position.y - 0.5f);
        } else if (gameObject.name.Equals("Waypoints2T3")) {
            transform.position = new Vector2(Waypoints.waypoints[1].position.x, Waypoints.waypoints[1].position.y - 0.5f);
        } else if (gameObject.name.Equals("GoalLine")) {
            transform.position = new Vector2(Waypoints.waypoints[2].position.x, Waypoints.waypoints[2].position.y - 0.5f);
        }
    }
}
