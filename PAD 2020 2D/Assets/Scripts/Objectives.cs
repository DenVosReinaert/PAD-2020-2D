using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour {

    public static Transform[] objectives; // list of objectives (start and end point)

    void Awake() {
        objectives = new Transform[transform.childCount]; // size of array should be amount of children
        for (int i = 0; i < objectives.Length; i++) { // get data from children
            objectives[i] = transform.GetChild(i);
        }
    }
}
