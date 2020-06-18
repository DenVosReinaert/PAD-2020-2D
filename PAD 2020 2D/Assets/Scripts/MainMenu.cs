using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level"); // When the button Play is pushed the scene loads to Level
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Conversation"); // Go to the conversation before tutorial starts
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop"); // When the button Shop is pushed the scene loads to Shop
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu"); // Loads main menu
    }

    public void Leaderboard() {
        SceneManager.LoadScene("Leaderboard"); // load leaderboard scene
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Interscene.instance.PutData(Interscene.instance.userName, Interscene.instance.money);
        Application.Quit(); // when the Quit button is pushed the application closes
    }
}
