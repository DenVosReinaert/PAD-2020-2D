using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial"); // When the button Play is pushed the scene loads to Level
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Conversation"); // We start at the conversation and then go tto the playable tutorial
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop"); // When the button Shop is pushed the scene loads to Shop
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu"); // Loads main menu
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit(); // when the Quit button is pushed the application closes
    }
}
