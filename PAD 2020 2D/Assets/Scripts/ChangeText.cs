using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    // A dictionary is similair to a list we used a dictionary to index the different text
    public Dictionary<int, string> allText = new Dictionary<int, string>();
    private string activeText;

    void Start()
    {
        // We say that 0 is our starting text and this can be changed in next en previous text
        activeText = "Gebruik de formule Y=aX+b om naar het volgende huisje te gaan. De huizen zijn omcirkeld in het geel.";

        allText.Add(0, activeText);
        allText.Add(1, "Voor de X as kan je de rode lijn op de pijp raadplegen en voor de Y as kan je bovenin aflezen.");
        allText.Add(2, "Als je de formule fout hebt gaat er 1 leven van af. De formule kan fout zijn als je de richtingscoeffiënt of de b fout hebt.");
        allText.Add(3, "Gebruik de pijltoetsen om naar de volgende huisje te gaan je kunt ook terug om je formule te bekijken!");

    }

    void Update()
    {
        // All the text is written inside of here but in unity we need to find and insert our text for it to show
        GameObject.Find("HintText").GetComponent<Text>().text = activeText;
    }

    public void NextText()
    {
        // Loop trough the different texts
        for (int i = 0; i < allText.Count; i++)
        {
            // We want to know which allText we are displaying so give us the index and string value
            if (allText.TryGetValue(i, out string value))
            {
                // The value should be the same as active text and may not overwrite our max otherwise it may cauyse an out of bounds
                if (value.Equals(activeText) && i != 3)
                {
                    // We now know which text is being displayed and replace it with the next in line
                    if (allText.TryGetValue(i+1, out string newValue))
                    {
                        // NewValue was our previous output and gets inserted into activeText
                        activeText = newValue;
                        // We break it to avoid looping this increases game performance
                        break;
                    }
                }
            }
        }
    }

    // The same code but then made to go to the previous text
    public void PreviousText()
    {
        for (int i = 0; i < allText.Count; i++)
        {
            if (allText.TryGetValue(i, out string value))
            {
                if (value.Equals(activeText) && i != 0)
                {
                    if (allText.TryGetValue(i-1, out string newValue))
                    {
                        activeText = newValue;
                        break;
                    }
                }
            }
        }
    }
}
