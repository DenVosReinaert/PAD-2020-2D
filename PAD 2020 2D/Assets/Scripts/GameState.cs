using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    // vectors for detection
    Vector3 firstPos;
    Vector3 beginPunt;
    Vector3 tussenPunt;
    Vector3 eindPunt;
    Vector3 doel;

    // constants
    private const double Margin = 0.4;

    // gameobjects to check if there is collision with a hazard
    GameObject lines;
    Collider2D lineCollider;

    // Start is called before the first frame update
    void Start() {
        lines = GameObject.Find("LineDrawer");
        lineCollider = lines.GetComponent<Collider2D>();
        firstPos = lines.GetComponent<LineRenderer>().GetPosition(0);
        beginPunt = lines.GetComponent<LineRenderer>().GetPosition(1);
        tussenPunt = GameObject.Find("Waypoints1T2").GetComponent<LineRenderer>().GetPosition(1);
        eindPunt = GameObject.Find("Waypoints2T3").GetComponent<LineRenderer>().GetPosition(1);
        doel = GameObject.Find("GoalLine").GetComponent<LineRenderer>().GetPosition(1);
    }

    // Update is called once per frame
    void Update() {
        if (!SceneManager.GetActiveScene().name.Equals("Level") || Waypoints.pipes == null) {
            return;
        }
        lineCollider = lines.GetComponent<Collider2D>();
        firstPos = lines.GetComponent<LineRenderer>().GetPosition(0);
        beginPunt = lines.GetComponent<LineRenderer>().GetPosition(1);
        tussenPunt = GameObject.Find("Waypoints1T2").GetComponent<LineRenderer>().GetPosition(1);
        eindPunt = GameObject.Find("Waypoints2T3").GetComponent<LineRenderer>().GetPosition(1);
        doel = GameObject.Find("GoalLine").GetComponent<LineRenderer>().GetPosition(1);
        foreach (Transform t in Hazards.hazards) {
            Collider2D hazardCollider = t.GetComponent<Collider2D>();
            Debug.Log(lineCollider.bounds.Intersects(hazardCollider.bounds));
            if (lineCollider.bounds.Intersects(hazardCollider.bounds) && DetectHit(doel, Objectives.objectives[1].position, Margin)) { // hit hazard when line fully deployed, so level failed
                Debug.Log("hazard!");
                SceneManager.LoadScene("FinishedLevel");
            }
        }
        bool hitsFirstPos = DetectHit(firstPos, Objectives.objectives[0].position, Margin);
        bool hitsSecondPos = DetectHit(beginPunt, Waypoints.pipes[0].position, Margin);
        bool hitsThirdPos = DetectHit(tussenPunt, Waypoints.pipes[1].position, Margin);
        bool hitsFourthPos = DetectHit(eindPunt, Waypoints.pipes[2].position, Margin);
        bool hitsFinalPos = DetectHit(doel, Objectives.objectives[1].position, Margin);
        if (hitsFirstPos && hitsSecondPos && hitsThirdPos && hitsFourthPos && hitsFinalPos) { // points match, so level completed
            SceneManager.LoadScene("FinishedLevel");
        }
    }

    private bool DetectHit(Vector3 firstLocation, Vector3 secondLocation, double margin) {
        bool hit = false;
        float differenceInX = secondLocation.x - firstLocation.x;
        float differenceInY = secondLocation.y - firstLocation.y;
        if ((differenceInX > -margin && differenceInX < margin) && (differenceInY > -margin && differenceInY < margin)) {
            hit = true;
        }
        return hit;
    }
}
