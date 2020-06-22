using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio instance;

    void Awake()
    {
        if (instance == null) { // if instance is null, set it to this and make it so this script doesnt get destroyed on load. Also give size to waypoints
            instance = this;
            DontDestroyOnLoad(this);
        } else if (instance != this) { // if instance is not equal to this, destroy.
            Destroy(gameObject);
        }
    }
}
