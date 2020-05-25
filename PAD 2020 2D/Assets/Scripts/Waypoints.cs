using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoints : MonoBehaviour {

    public static Transform[] waypoints; // array of waypoints
    public int z = 0;

    private const int _ScreenSizeX = 22; // size of our screen
    private const int _ScreenDivide = 5; // used to divide the screen in segments
    private const int _ScreenOffset = 11; // offset
    private const int _YMin = -5; // minimum of y
    private const int _YMax = 5; // maximum of y

    void Awake() {
        waypoints = new Transform[transform.childCount]; // size of array is amount of childs
        for (int i = 0; i < waypoints.Length; i++) { // get data from children
            waypoints[i] = transform.GetChild(i);
        }
        if (!Interscene.instance.retryLevel) { // if retry is false, give each waypoint a random position on the screen
            GameObject.Find("WP").transform.position = RandomPosition1();
            GameObject.Find("WP (1)").transform.position = RandomPosition2();
            GameObject.Find("WP (2)").transform.position = RandomPosition3();
        } else { // else, get the position from Interscene script
            GameObject.Find("WP").transform.position = Interscene.instance.waypoints[0];
            GameObject.Find("WP (1)").transform.position = Interscene.instance.waypoints[1];
            GameObject.Find("WP (2)").transform.position = Interscene.instance.waypoints[2];
        }
        for (int i = 0; i < Interscene.instance.waypoints.Length; i++) { // store positions in interscene
            Interscene.instance.waypoints[i] = waypoints[i].position;
        }
    }

    private Vector3 RandomPosition1() {
        float xMaxBoundary = _ScreenSizeX / _ScreenDivide * 2 - _ScreenOffset; // boundaries of the screen
        float xMinBoundary = _ScreenSizeX / _ScreenDivide - _ScreenOffset;
        float yMaxBoundary = _YMax;
        float yMinBoundary = _YMin;
        Vector3 rando = new Vector3(Random.Range(xMinBoundary, xMaxBoundary), Random.Range(yMinBoundary, yMaxBoundary), z); // create random position based on screen boundaries
        foreach (Transform hazard in Hazards.hazards) { // make sure random position doesnt have collision with a hazard
            SpriteRenderer texture = hazard.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x) { // move it away from the hazard if it has
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
        foreach (Transform objective in Objectives.objectives) {
            SpriteRenderer texture = objective.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x) { // move it away from the objectives if it has
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
        for (int i = 0; i < waypoints.Length; i++) { // make sure the waypoint doesnt have collision with other waypoints
            if (waypoints[i] == null) {
                continue;
            }
            SpriteRenderer texture = waypoints[i].GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x) { // move them away from the other waypoint(s)
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

    private Vector3 RandomPosition2() { // repeat
        float xMaxBoundary = _ScreenSizeX / _ScreenDivide * 3 - _ScreenOffset;
        float xMinBoundary = _ScreenSizeX / _ScreenDivide * 2- _ScreenOffset;
        float yMaxBoundary = _YMax;
        float yMinBoundary = _YMin;
        Vector3 rando = new Vector3(Random.Range(xMinBoundary, xMaxBoundary), Random.Range(yMinBoundary, yMaxBoundary), z);
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

    private Vector3 RandomPosition3() {
        float xMaxBoundary = _ScreenSizeX / _ScreenDivide * 4 - _ScreenOffset;
        float xMinBoundary = _ScreenSizeX / _ScreenDivide * 3 - _ScreenOffset;
        float yMaxBoundary = _YMax;
        float yMinBoundary = _YMin;
        Vector3 rando = new Vector3(Random.Range(xMinBoundary, xMaxBoundary), Random.Range(yMinBoundary, yMaxBoundary), z);
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
