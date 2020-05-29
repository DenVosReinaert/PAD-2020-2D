using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTransit : MonoBehaviour
{
    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // When the win requirements are met, the scene changes from level to WinScreen
    }
}
