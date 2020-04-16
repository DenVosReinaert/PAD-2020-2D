﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interscene : MonoBehaviour {

    public static Interscene instance;
    public bool retryLevel = false;
    public Vector3[] pipes;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
            pipes = new Vector3[3];
        } else if (instance != this) {
            Destroy(this);
        }
    }

    void Update() {

    }
}