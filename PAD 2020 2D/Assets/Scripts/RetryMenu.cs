﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour { 
    
    public void Retry() {
        SceneManager.LoadScene("Level");
        Interscene.retryLevel = true;
    }

    public void NextLevel() {
        SceneManager.LoadScene("Level");
        Interscene.retryLevel = false;
    }
       
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}