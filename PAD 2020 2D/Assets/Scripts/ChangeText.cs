using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Dictionary<int, string> allText = new Dictionary<int, string>();
    private string activeText;

    void Start()
    {
        activeText = "Gebruik de formule Y=aX+b om naar het volgende huisje te gaan. De huizen zijn omcirkeld in het geel.";

        allText.Add(0, activeText);
        allText.Add(1, "Voor de X as kan je de rode lijn op de pijp raadplegen en voor de Y as kan je bovenin aflezen.");
        allText.Add(2, "Als je de formule fout hebt gaat er 1 leven van af. De formule kan fout zijn als je de richtingscoeffiënt of de b fout hebt.");

    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("HintText").GetComponent<Text>().text = activeText;
    }

    public void NextText()
    {
        for (int i = 0; i < allText.Count; i++)
        {
            if (allText.TryGetValue(i, out string value))
            {
                if (value.Equals(activeText) && i != 2)
                {
                    if (allText.TryGetValue(i++, out string newValue))
                    {
                        activeText = newValue;
                        break;
                    }
                }
            }
        }
    }

    public void PreviousText()
    {
        for (int i = 0; i < allText.Count; i++)
        {
            if (allText.TryGetValue(i, out string value))
            {
                if (value.Equals(activeText) && i != 0)
                {
                    if (allText.TryGetValue(i--, out string newValue))
                    {
                        activeText = newValue;
                        break;
                    }
                }
            }
        }
    }
    //Gebruik de formule Y=aX+b om naar het volgende huisje te gaan. De huizen zijn omcirkeld in het geel.
}
