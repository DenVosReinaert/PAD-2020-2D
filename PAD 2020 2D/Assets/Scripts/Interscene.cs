using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interscene : MonoBehaviour {

    public static Interscene instance; // instance of this class
    public bool retryLevel = false;
    public Vector3[] waypoints; // waypoints, they are stored here as well because this script doesnt get destroyed and helps with retrying or going to the next level

    void Awake() {
        if (instance == null) { // if instance is null, set it to this and make it so this script doesnt get destroyed on load. Also give size to waypoints
            instance = this;
            DontDestroyOnLoad(this);
            waypoints = new Vector3[3];
        } else if (instance != this) { // if instance is not equal to this, destroy.
            Destroy(this);
        }
    }
}
