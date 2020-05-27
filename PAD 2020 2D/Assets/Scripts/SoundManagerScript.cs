using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip rightSound, wrongSound;

    private static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rightSound = Resources.Load<AudioClip>("Right");
        wrongSound = Resources.Load<AudioClip>("Wrong");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "right":
                audioSource.PlayOneShot(rightSound);
                break;
            case "wrong":
                audioSource.PlayOneShot(wrongSound);
                break;
        }
    }
}
