using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class money : MonoBehaviour {

    public static string prefix = "Money: $";

    public Text moneyText;

    void Awake() { // replaces the buybutton for a select button when a pipe has been bought, and removes the price amount text
        updateMoneyUI(); 
        if (HasSilverPipe() && name.Equals("BuyButton1")) {
            GameObject.Find("BuyButton1").SetActive(false);
            GameObject.Find("Price1").SetActive(false);
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (go.name.Equals("SelectButton2")) {
                    go.SetActive(true);
                }
            }
        }
        if (HasBluePipe() && name.Equals("BuyButton2")) {
            GameObject.Find("BuyButton2").SetActive(false);
            GameObject.Find("Price2").SetActive(false);
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (go.name.Equals("SelectButton3")) {
                    go.SetActive(true);
                }
            }
        }
        if (HasRedPipe() && name.Equals("BuyButton3")) {
            GameObject.Find("BuyButton3").SetActive(false);
            GameObject.Find("Price3").SetActive(false);
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (go.name.Equals("SelectButton4")) {
                    go.SetActive(true);
                }
            }
        }
        if (HasGoldPipe() && name.Equals("BuyButton4")) {
            GameObject.Find("BuyButton4").SetActive(false);
            GameObject.Find("Price4").SetActive(false);
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (go.name.Equals("SelectButton5")) {
                    go.SetActive(true);
                }
            }
        }
    }

    void Update() { // bought pipes can be selected and the selected pipe button becomes green
        updateMoneyUI();
        switch (GetActive()) {
            case "Silver":
                if (HasSilverPipe()) {
                    var colors = GameObject.Find("SelectButton2").GetComponent<Button>().colors;
                    colors.selectedColor = Color.green;
                    GameObject.Find("SelectButton2").GetComponent<Button>().colors = colors;
                    GameObject.Find("SelectButton2").GetComponent<Button>().Select();
                }
                break;
            case "Blue":
                if (HasBluePipe()) {
                    var colors = GameObject.Find("SelectButton3").GetComponent<Button>().colors;
                    colors.selectedColor = Color.green;
                    GameObject.Find("SelectButton3").GetComponent<Button>().colors = colors;
                    GameObject.Find("SelectButton3").GetComponent<Button>().Select();
                }
                break;
            case "Red":
                if (HasRedPipe()) {
                    var colors = GameObject.Find("SelectButton4").GetComponent<Button>().colors;
                    colors.selectedColor = Color.green;
                    GameObject.Find("SelectButton4").GetComponent<Button>().colors = colors;
                    GameObject.Find("SelectButton4").GetComponent<Button>().Select();
                }
                break;
            case "Gold":
                if (HasGoldPipe()) {
                    var colors = GameObject.Find("SelectButton5").GetComponent<Button>().colors;
                    colors.selectedColor = Color.green;
                    GameObject.Find("SelectButton5").GetComponent<Button>().colors = colors;
                    GameObject.Find("SelectButton5").GetComponent<Button>().Select();
                }
                break;
            default:
                var colorz = GameObject.Find("SelectButton1").GetComponent<Button>().colors;
                colorz.selectedColor = Color.green;
                GameObject.Find("SelectButton1").GetComponent<Button>().colors = colorz;
                GameObject.Find("SelectButton1").GetComponent<Button>().Select();
                break;
        }
    }

    public void addMoney(int xAmount){ 
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + xAmount);
        updateMoneyUI();
    }

    public void ReduceMoney(int yAmount) {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - yAmount);
        updateMoneyUI();
    }

    void updateMoneyUI(){ //stores the money the players has earned
        moneyText.text = prefix + PlayerPrefs.GetInt("Money").ToString();
    }

    void Reset() {
        PlayerPrefs.DeleteAll();
    }

    // stores the pipes the player has already unlocked
    public void BoughtSilverPipe() {
        PlayerPrefs.SetString("Silver", "true");
    }

    bool HasSilverPipe() {
        string has = PlayerPrefs.GetString("Silver");
        return has.Equals("true") ? true : false;
    }

    public void BoughtBluePipe() {
        PlayerPrefs.SetString("Blue", "true");
    }

    bool HasBluePipe() {
        string has = PlayerPrefs.GetString("Blue");
        return has.Equals("true") ? true : false;
    }

    public void BoughtRedPipe() {
        PlayerPrefs.SetString("Red", "true");
    }

    bool HasRedPipe() {
        string has = PlayerPrefs.GetString("Red");
        return has.Equals("true") ? true : false;
    }

    public void BoughtGoldPipe() {
        PlayerPrefs.SetString("Gold", "true");
    }

    bool HasGoldPipe() {
        string has = PlayerPrefs.GetString("Gold");
        return has.Equals("true") ? true : false;
    }

    public void SetActive(string pipe) {
        PlayerPrefs.SetString("Active", pipe);
    }

    public string GetActive() {
        return PlayerPrefs.GetString("Active");
    }
}