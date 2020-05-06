using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoints : MonoBehaviour {

    public static Transform[] waypoints;

    void Awake() {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = transform.GetChild(i);
            Debug.Log(waypoints[i].position + " " + waypoints[i].gameObject.name);
        }
        if (!Interscene.instance.retryLevel) {
            GameObject.Find("WP").transform.position = RandomPosition();
            GameObject.Find("WP (1)").transform.position = RandomPosition();
            GameObject.Find("WP (2)").transform.position = RandomPosition();
        } else {
            GameObject.Find("WP").transform.position = Interscene.instance.waypoints[0];
            GameObject.Find("WP (1)").transform.position = Interscene.instance.waypoints[1];
            GameObject.Find("WP (2)").transform.position = Interscene.instance.waypoints[2];
        }
        for (int i = 0; i < Interscene.instance.waypoints.Length; i++) {
            Interscene.instance.waypoints[i] = waypoints[i].position;
        }
    }

    void Update() {
        if (SceneManager.GetActiveScene().name.Equals("Level")) {
            if (Input.GetKeyDown(KeyCode.F1)) {
                SceneManager.LoadScene("FinishedLevel");
            }
        }
    }

    private Vector3 RandomPosition() {
        float xMaxBoundary = Objectives.objectives[0].GetComponent<SpriteRenderer>().bounds.max.x;
        float xMinBoundary = Objectives.objectives[1].GetComponent<SpriteRenderer>().bounds.min.x;
        float yMaxBoundary = Objectives.objectives[0].GetComponent<SpriteRenderer>().bounds.max.y;
        float yMinBoundary = Objectives.objectives[1].GetComponent<SpriteRenderer>().bounds.min.y;
        Vector3 rando = new Vector3(Random.Range(xMinBoundary, xMaxBoundary), Random.Range(yMinBoundary, yMaxBoundary), 0);
        foreach (Transform hazard in Hazards.hazards) {
            SpriteRenderer texture = hazard.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x) {
                    rando.x++;
                } else {
                    rando.x--;
                }
                if (rando.y > texture.bounds.center.y) {
                    rando.y++;
                } else {
                    rando.y--;
                }
            }
        }
        for (int i = 0; i < waypoints.Length; i++) {
            if (waypoints[i] == null) {
                continue;
            }
            SpriteRenderer texture = waypoints[i].GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x) {
                    rando.x++;
                } else {
                    rando.x--;
                }
                if (rando.y > texture.bounds.center.y) {
                    rando.y++;
                } else {
                    rando.y--;
                }
            }
        }
        return rando;
    }
}
