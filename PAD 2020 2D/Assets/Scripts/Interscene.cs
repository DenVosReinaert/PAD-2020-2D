using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interscene : MonoBehaviour {

    public static Interscene instance; // instance of this class
    public bool retryLevel = false;
    public Vector3[] waypoints; // waypoints, they are stored here as well because this script doesnt get destroyed and helps with retrying or going to the next level
    public string userName;
    public int money;

    private bool hasData;
    private bool checkData;
    private bool loginCheck;
    private bool checkedDB;
    private float time;

    void Awake() {
        if (instance == null) { // if instance is null, set it to this and make it so this script doesnt get destroyed on load. Also give size to waypoints
            instance = this;
            DontDestroyOnLoad(this);
            waypoints = new Vector3[3];
        } else if (instance != this) { // if instance is not equal to this, destroy.
            Destroy(gameObject);
        }
    }

    public void Update() {
        if (!checkData && loginCheck && !checkedDB) {
            CheckDB(userName);
        }
        if (Time.time - time >= 10) {
            //checkedDB = false;
        }
    }

    public void Login() {
        string name = GameObject.Find("Username").GetComponent<InputField>().text;
        if (string.IsNullOrEmpty(name)) {
            Debug.Log("Name was empty!");
            return;
        }
        userName = name;
        StartCoroutine(CheckForData(userName));
        loginCheck = true;
        SceneManager.LoadScene("Menu");
    }

    private void CheckDB(string userName) {
        if (hasData) {
            RetrieveData(userName);
        } else {
            PutData(userName, 0);
        }
        time = Time.time;
        checkedDB = true;
    }

    public void PutData(string userName, int money) {
        StartCoroutine(Score(userName, money));
    }

    IEnumerator Score(string userName, int money) {
        WWWForm form = new WWWForm();
        form.AddField("name", userName);
        form.AddField("score", money);
        using (UnityWebRequest tables = UnityWebRequest.Post("https://oege.ie.hva.nl/~baasdr/InputData.php", form)) {
            yield return tables.SendWebRequest();
            if (tables.result == UnityWebRequest.Result.ConnectionError || tables.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(tables.error);
            } else {
                string response = tables.downloadHandler.text;
                if (response.StartsWith("Successfully")) {
                    Debug.Log("Successfully updated MySQL score. (" + userName + ", " + money + ")");
                } else if (response.StartsWith("Could")) {
                    Debug.Log("Failed to update MySQL score. (" + userName + ", " + money + ")");
                }
            }
        }
    }

    IEnumerator CheckForData(string userName) {
        checkData = true;
        WWWForm form = new WWWForm();
        form.AddField("name", userName);
        using (UnityWebRequest moneySQL = UnityWebRequest.Post("https://oege.ie.hva.nl/~baasdr/HasData.php", form)) {
            yield return moneySQL.SendWebRequest();
            if (moneySQL.result == UnityWebRequest.Result.ConnectionError || moneySQL.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(moneySQL.error);
            } else {
                string response = moneySQL.downloadHandler.text;
                if (response.StartsWith("Successfully")) {
                    Debug.Log(userName + " has data!");
                    hasData = true;
                } else if (response.StartsWith("Could")) {
                    Debug.Log("Failed to check for data in MySQL. (" + userName + ")");
                    hasData = false;
                }
            }
        }
        checkData = false;
    }

    public void RetrieveData(string userName) {
        StartCoroutine(GetData(userName));
    }

    private IEnumerator GetData(string userName) {
        WWWForm form = new WWWForm();
        form.AddField("username", userName);
        using (UnityWebRequest data = UnityWebRequest.Post("https://oege.ie.hva.nl/~baasdr/GetData.php", form)) {
            yield return data.SendWebRequest();
            if (data.result == UnityWebRequest.Result.ConnectionError || data.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(data.error);
            } else {
                string response = data.downloadHandler.text;
                if (response.StartsWith("Data")) { // data is returned in the form: data {money}
                    string[] splitData = response.Split(' ');
                    if (CanParse(splitData[1])) {
                        money = Parse(splitData[1]);
                    }
                    Debug.Log(userName + " has moneyz (" + money + ")!");
                } else if (response.StartsWith("Could")) {
                    Debug.Log("Failed to check for data in MySQL. (" + userName + ")");
                }
            }
        }
    }

    private bool CanParse(string number) {
        foreach (char c in number) {
            if (c < '0' || c > '9') {
                return false;
            }
        }
        return true;
    }

    private int Parse(string number) {
        int parsed = 0;
        try {
            parsed = int.Parse(number);
        } catch (FormatException e) {
            Debug.Log("Could not parse: " + e);
        }
        return parsed;
    }
}
