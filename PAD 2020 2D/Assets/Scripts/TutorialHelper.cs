using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    public static bool levelHasStarted;

    public void LevelHasStarted() {
        levelHasStarted = true;
    }
}
