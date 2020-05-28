using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lives : MonoBehaviour {

    private static GameObject[] hearts;// array of gameobjects hearts
    public static int life = 5;

    private void Awake() { 
        hearts = new GameObject[transform.childCount]; // array size = hearts/lifes
        for (int i = 0; i < hearts.Length; i++) { // get data from childs
            hearts[i] = transform.GetChild(i).gameObject;
        }
    }


    void Update() { // if life = -1 one heart disapears
        if (life < 1) {
            hearts[0].SetActive(false);
            SceneManager.LoadScene("LossScreen"); // at 0 lifes lossscreen 
            ResetLives(); // resets lifes and displayed hearts back to 5 for retry or next game
        } else if (life < 2) {
            hearts[1].SetActive(false);
        } else if (life < 3) {
            hearts[2].SetActive(false);
        } else if (life < 4) {
            hearts[3].SetActive(false);
        } else if (life < 5) {
            hearts[4].SetActive(false);
        }
    }

    public static void ResetLives() {
        life = 5; // restets lifes back to 5
        for (int i = 0; i < hearts.Length; i++) { // restets displayed hearts to 5
            hearts[i].SetActive(true);
        }
    }

    public void loselife(int d) // d = damage
    {
        life -= d;
    }
}
