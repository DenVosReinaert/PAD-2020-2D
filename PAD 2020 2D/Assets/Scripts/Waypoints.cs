using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoints : MonoBehaviour {

    public static Transform[] waypoints; // array of waypoints
    public int z = 0;

    private const int _Positie1MaxX = -4; // all the boundaries of each house
    private const int _Positie1MinX = -7;
    private const int _Positie2MaxX = 2;
    private const int _Positie2MinX = -2;
    private const int _Positie3MaxX = 6;
    private const int _Positie3MinX = 4;
    private const int _YMin = -5; // minimum of y
    private const int _YMax = 5; // maximum of y

    void Awake()
    {
        waypoints = new Transform[transform.childCount]; // size of array is amount of childs
        for (int i = 0; i < waypoints.Length; i++)
        { // get data from children
            waypoints[i] = transform.GetChild(i);
        }

        if (SceneManager.GetActiveScene().name.Equals("Level"))
        {
            if (!Interscene.instance.retryLevel)
            { // if retry is false, give each waypoint a random position on the screen
                GameObject.Find("WP").transform.position = RandomPosition1();
                GameObject.Find("WP (1)").transform.position = RandomPosition2();
                GameObject.Find("WP (2)").transform.position = RandomPosition3();
            }
            else
            { // else, get the position from Interscene script
                GameObject.Find("WP").transform.position = Interscene.instance.waypoints[0];
                GameObject.Find("WP (1)").transform.position = Interscene.instance.waypoints[1];
                GameObject.Find("WP (2)").transform.position = Interscene.instance.waypoints[2];
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("TutorialIntro"))
        {
            GameObject.Find("WP").transform.position = new Vector3(-6, -3, -2);
            GameObject.Find("WP (1)").transform.position = new Vector3(0, 2, -2);
            GameObject.Find("WP (2)").transform.position = new Vector3(3, -4, -2);
        }

        for (int i = 0; i < Interscene.instance.waypoints.Length; i++)
        { // store positions in interscene
            Interscene.instance.waypoints[i] = waypoints[i].position;
        }
    }

    private Vector3 RandomPosition1() {
        Vector3 rando = new Vector3(Random.Range(_Positie1MinX, _Positie1MaxX), Random.Range(_YMin, _YMax), z); // create random position based on screen boundaries
        foreach (Transform hazard in Hazards.hazards) { // make sure random position doesnt have collision with a hazard
            SpriteRenderer texture = hazard.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie1MinX)) { // move it away from the hazard if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie1MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        foreach (Transform objective in Objectives.objectives) {
            SpriteRenderer texture = objective.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie1MinX)) { // move it away from the objectives if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie1MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        for (int i = 0; i < waypoints.Length; i++) { // make sure the waypoint doesnt have collision with other waypoints
            if (waypoints[i] == null) {
                continue;
            }
            SpriteRenderer texture = waypoints[i].GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie1MinX)) {  // move them away from the other waypoint(s)
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie1MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        rando.x = (int) Mathf.Round(rando.x);
        rando.y = (int) Mathf.Round(rando.y);
        if (rando.x >= 8) {
            rando.x = 7;
        } else if (rando.x <= -8) {
            rando.x = -7;
        }
        return rando;
    }

    private Vector3 RandomPosition2() { // repeat
        Vector3 rando = new Vector3(Random.Range(_Positie2MinX, _Positie2MaxX), Random.Range(_YMin, _YMax), z);
        foreach (Transform hazard in Hazards.hazards) { // make sure random position doesnt have collision with a hazard
            SpriteRenderer texture = hazard.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie2MinX)) { // move it away from the hazard if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie2MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        foreach (Transform objective in Objectives.objectives) {
            SpriteRenderer texture = objective.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie2MinX)) { // move it away from the objectives if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie2MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        for (int i = 0; i < waypoints.Length; i++) { // make sure the waypoint doesnt have collision with other waypoints
            if (waypoints[i] == null) {
                continue;
            }
            SpriteRenderer texture = waypoints[i].GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie2MinX)) {  // move them away from the other waypoint(s)
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie2MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        rando.x = (int) Mathf.Round(rando.x);
        rando.y = (int) Mathf.Round(rando.y);
        if (rando.x >= 8) {
            rando.x = 7;
        } else if (rando.x <= -8) {
            rando.x = -7;
        }
        return rando;
    }

    private Vector3 RandomPosition3() {
        Vector3 rando = new Vector3(Random.Range(_Positie3MinX, _Positie3MaxX), Random.Range(_YMin, _YMax), z);
        foreach (Transform hazard in Hazards.hazards) { // make sure random position doesnt have collision with a hazard
            SpriteRenderer texture = hazard.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie3MinX)) { // move it away from the hazard if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie3MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        foreach (Transform objective in Objectives.objectives) {
            SpriteRenderer texture = objective.GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie3MinX)) { // move it away from the objectives if it has
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie3MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        for (int i = 0; i < waypoints.Length; i++) { // make sure the waypoint doesnt have collision with other waypoints
            if (waypoints[i] == null) {
                continue;
            }
            SpriteRenderer texture = waypoints[i].GetComponent<SpriteRenderer>();
            while (rando.x > texture.bounds.min.x && rando.x < texture.bounds.max.x && rando.y > texture.bounds.min.y && rando.y < texture.bounds.max.y) {
                if (rando.x > texture.bounds.center.x && !(rando.x - texture.bounds.center.x < _Positie3MinX)) {  // move them away from the other waypoint(s)
                    rando.x++;
                } else {
                    if (rando.x + texture.bounds.center.x > _Positie3MaxX) {
                        rando.x++;
                    } else {
                        rando.x--;
                    }
                }
                if (rando.y > texture.bounds.center.y && !(rando.y - texture.bounds.center.y < _YMin)) {
                    rando.y++;
                } else {
                    if (rando.y + texture.bounds.center.y > _YMax) {
                        rando.y++;
                    } else {
                        rando.y--;
                    }
                }
            }
        }
        rando.x = (int) Mathf.Round(rando.x);
        rando.y = (int) Mathf.Round(rando.y);
        if (rando.x >= 8) {
            rando.x = 7;
        } else if (rando.x <= -8) {
            rando.x = -7;
        }
        return rando;
    }
}
