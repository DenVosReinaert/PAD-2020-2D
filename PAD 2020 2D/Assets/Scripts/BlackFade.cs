using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{

    public float fadespeed = 0.5f;

    // We use an IEnumerator because they are very excact and can stop at an individual frame if needed to stop an iteration
    public IEnumerator FadeOutObject()
    {
        // We keep checking if the alpha is more then zero is this true then we want to keep reducing the alpha
        while (this.GetComponent<Renderer>().material.color.a > 0)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            // 
            float fadeAmount = objectColor.a - (fadespeed * Time.deltaTime);

            // We need to specify the object color by RGB standards 
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
    }

    // The same but then to fade in
    public IEnumerator FadeInObject()
    {
        while (this.GetComponent<Renderer>().material.color.a < 1)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadespeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            // Wait
            yield return null;
        }
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Coroutines are a type that is used to create parallel actions returning an IEnumerator to do so
            StartCoroutine(FadeOutObject());
        }
        // We only want it to fade in for these two objects, we have a button that handles the rest
        if (gameObject.name.Equals("Bubble 1") || gameObject.name.Equals("Bowser0"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(FadeInObject());
            }
        }
    }
    
}
