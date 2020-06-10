using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour { 
    
    public void Retry() {
        SceneManager.LoadScene("Level"); // When the button Retry is pushed the scene loads to Level
        Interscene.instance.retryLevel = true; // Keeps the Position of the houses so the same level loads
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop"); // When the button Shop is pushed the scene loads to Shop
    }

    public void NextLevel() {
        SceneManager.LoadScene("Level"); // When the button Next Level is pushed the scene loads to Level
        Interscene.instance.retryLevel = false; // Doesn't keep the Position of the houses so a new level loads
    }

    public void FinishedLevelScene()
    {
        SceneManager.LoadScene("FinishedLevel"); // When the win requirement is met the scene changes to the Win Screen
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Interscene.instance.PutData(Interscene.instance.userName, Interscene.instance.money);
        Application.Quit(); // when the Quit button is pushed the application closes
    }
}
