﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour { 
    
    public void Retry() {
        SceneManager.LoadScene("Level");
        Interscene.instance.retryLevel = true;
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void NextLevel() {
        SceneManager.LoadScene("Level");
        Interscene.instance.retryLevel = false;
    }

    public void FinishedLevelScene()
    {
        SceneManager.LoadScene("FinishedLevel");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
