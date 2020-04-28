using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    private static float currentCoefficient = 0;
    public Vector3 dir;
    public Vector2 Middle = new Vector2(0, 0);

    private LineRenderer lineRedender;


    void Awake() {
         lineRedender = GetComponent<LineRenderer>(); // get the component from unity
         lineRedender.positionCount = 2; // set amount of positions
         
        if (gameObject.name.Equals("LineDrawer")) {
            lineRedender.SetPosition(0, Objectives.objectives[0].position);
            lineRedender.SetPosition(1, Waypoints.pipes[0].position);
            //renderer.SetPosition(1, new Vector3(Objectives.objectives[0].position.x + 1, Objectives.objectives[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints1T2")) {
            lineRedender.SetPosition(0, Waypoints.pipes[0].position);
            lineRedender.SetPosition(1, new Vector3(Waypoints.pipes[0].position.x + 1, Waypoints.pipes[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints2T3")) {
            lineRedender.SetPosition(0, Waypoints.pipes[1].position);
            lineRedender.SetPosition(1, new Vector3(Waypoints.pipes[1].position.x + 1, Waypoints.pipes[1].position.y - 1, 0f));
        } else if (gameObject.name.Equals("GoalLine")) {
            lineRedender.SetPosition(0, Waypoints.pipes[2].position);
            lineRedender.SetPosition(1, new Vector3(Waypoints.pipes[2].position.x + 1, Waypoints.pipes[2].position.y - 1, 0f));
        }
    }

    private void FixedUpdate()
    {
      
        dir = lineRedender.GetPosition(1);

        //raycast is going in a straight line problem is that the renderer has an offset
        if (gameObject.name.Equals("LineDrawer")) {
            RaycastHit2D hitWaypoint1 = Physics2D.Raycast(Objectives.objectives[0].position, dir);
            Debug.DrawRay(Objectives.objectives[0].position, dir, Color.blue);
            if (hitWaypoint1.collider.CompareTag("Pipe")) {
                Debug.Log("check me out!");
                lineRedender.SetPosition(1, Waypoints.pipes[0].position);
            }
        } else if (gameObject.name.Equals("Waypoints1T2")) {
            RaycastHit2D hitWaypoint2 = Physics2D.Raycast(Waypoints.pipes[0].position, dir);
            Debug.DrawRay(Waypoints.pipes[0].position, dir, Color.blue);
            if (hitWaypoint2.collider != null) {
                //Debug.Log("check me out!");
            }
        } else if (gameObject.name.Equals("Waypoints2T3")) {
            RaycastHit2D hitWaypoint3 = Physics2D.Raycast(Waypoints.pipes[1].position, dir);
            Debug.DrawRay(Waypoints.pipes[1].position, dir, Color.blue);
            if (hitWaypoint3.collider != null) {
                //Debug.Log("check me out!");
            }
        } else if (gameObject.name.Equals("GoalLine")) {
            RaycastHit2D goal = Physics2D.Raycast(Waypoints.pipes[2].position, dir);
            Debug.DrawRay(Waypoints.pipes[2].position, dir, Color.blue);
            if (goal.collider != null) {
                //Debug.Log("check me out!");
            }
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
}
