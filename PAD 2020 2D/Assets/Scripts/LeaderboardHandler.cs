using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardHandler : MonoBehaviour {
    private Dictionary<string, int> highScores = new Dictionary<string, int>();

    public void Home() {
        SceneManager.LoadScene("Menu"); // go to menu
    }

    public void Leaderboard() {
        SceneManager.LoadScene("Leaderboard"); // go to leaderboard scene
    }

    public void Awake() {
        GetData();
    }

    public void Start() {
        if (SceneManager.GetActiveScene().name.Equals("Leaderboard")) {
            FillBoard();
        }
    }

    public void Update() {
        if (SceneManager.GetActiveScene().name.Equals("Leaderboard")) {
            FillBoard();
        }
    }

    private void FillBoard() {
        string[] names = new string[highScores.Keys.Count];
        int[] scores = new int[highScores.Values.Count];
        int counter = 0;
        foreach (string name in highScores.Keys) {
            names[counter] = name;
            counter++;
        }
        for (int i = 0; i < highScores.Count; i++) {
            if (highScores.TryGetValue(names[i], out int value)) {
                scores[i] = value;
            }
        }
        for (int i = 0; i < names.Length; i++) {
            GameObject.Find("Name (" + i + ")").GetComponent<TextMeshProUGUI>().text = names[i];
            GameObject.Find("Score (" + i + ")").GetComponent<TextMeshProUGUI>().text = scores[i] + "";
        }
    }


    private void GetData() {
        StartCoroutine(Data());
    }

    private IEnumerator Data() {
        WWWForm form = new WWWForm();
        using (UnityWebRequest data = UnityWebRequest.Post("https://oege.ie.hva.nl/~baasdr/Leaderboard.php", form)) {
            yield return data.SendWebRequest();
            string response = data.downloadHandler.text;
            string[] scores = new string[7];
            string[] names = new string[7];
            int index = response.IndexOf("N", 2);
            string scoreTemp = response.Substring(0, index);
            string nameTemp = response.Substring(index);
            if (nameTemp.StartsWith("Names: ")) {
                nameTemp = nameTemp.Replace("Names: ", "");
                nameTemp = nameTemp.Replace(", ", " ");
                names = nameTemp.Split(' ');
            }
            if (scoreTemp.StartsWith("Scores: ")) {
                scoreTemp = scoreTemp.Replace("Scores: ", "");
                scoreTemp = scoreTemp.Replace(", ", " ");
                scores = scoreTemp.Split(' ');
            }
            for (int i = 0; i < names.Length; i++) {
                string toParse = scores[i].Replace(" ", "");
                int parsed = int.Parse(toParse);
                highScores.Add(names[i], parsed);
            }
        }
    }
}
