using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour {

    public static Transform[] hazards; // array of hazards

    void Awake() {
        hazards = new Transform[transform.childCount]; // size of hazards should be the size of children
        for (int i = 0; i < hazards.Length; i++) { // get childrens data
            hazards[i] = transform.GetChild(i);
        }
    }
}
