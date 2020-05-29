using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition; // Takes the original position of the camera for later usage

        float elapsed = 0.0f; // Timer for the duration of the camera shake

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude; // Determines a random position between -1 and 1 times the magnitude of your choosing
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z); // The position gets updated according to the random position

            elapsed += Time.deltaTime; // Timer value goes up every frame

            yield return null;
        }

        transform.localPosition = originalPos; // position of the camera goes back to it's original position
    }
}
