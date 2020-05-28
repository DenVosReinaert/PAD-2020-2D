using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionChecker : MonoBehaviour {

    private ParticleSystem particle; // particle system
    private ParticleSystem spark; // particle system

    public CameraShake cameraShake;

    void Awake() {
        particle = gameObject.GetComponentInChildren<ParticleSystem>(); // retrieve the particles system
        spark = GameObject.Find("Sparks").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D other) { // this method is triggered when a collider enters a rigidbody
        string name = gameObject.name;
        if (!name.Equals(ActiveLineChecker.activeLine.name)) { // if the name of the object is not equal to the activeline, its the incorrect line so return
            return;
        }
        if (name.Equals("LineDrawer")) { 
            if (!other.gameObject.name.Equals("WP")) {
                return; // if collider is not equal to the first waypoint, return
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine); // add the line to the list that it hit their goal
            SetColor();
            if (ActiveLineChecker.hasBCorrect.TryGetValue(ActiveLineChecker.activeLine, out bool value)) {
                if (value) {
                    Explode(Waypoints.waypoints[0].position); // particles
                    StartCoroutine(cameraShake.Shake(.15f, .4f));
                }
            }
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(0, 1, 0); // set color for the text
        } else if (name.Equals("Waypoints1T2")) { // repeat
            if (!other.gameObject.name.Equals("WP (1)")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            SetColor();
            if (ActiveLineChecker.hasBCorrect.TryGetValue(ActiveLineChecker.activeLine, out bool value)) {
                if (value) {
                    Explode(Waypoints.waypoints[1].position); // particles
                    StartCoroutine(cameraShake.Shake(.15f, .4f));
                }
            }
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(0, 1, 0);
        } else if (name.Equals("Waypoints2T3")) {
            if (!other.gameObject.name.Equals("WP (2)")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            SetColor();
            if (ActiveLineChecker.hasBCorrect.TryGetValue(ActiveLineChecker.activeLine, out bool value)) {
                if (value) {
                    Explode(Waypoints.waypoints[2].position); // particles
                    StartCoroutine(cameraShake.Shake(.15f, .4f));
                }
            }
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(0, 1, 0);
        } else if (name.Equals("GoalLine")) {
            if (!other.gameObject.name.Equals("Goal")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            SetColor();
            if (ActiveLineChecker.hasBCorrect.TryGetValue(ActiveLineChecker.activeLine, out bool value)) {
                if (value) {
                    Explode(Objectives.objectives[1].position);
                    StartCoroutine(cameraShake.Shake(.15f, .4f));
                }
            }
            GameObject.Find("Formule").GetComponent<Text>().color = new Color(0, 1, 0);
        }

        void Explode(Vector2 position) {
            particle.transform.position = position; // set position
            particle.Play(); // play

            spark.transform.position = position;
            spark.Play();
        }
    }

    private void SetColor() {
        switch (PlayerPrefs.GetString("Active")) { // color according to pipe
            case "Silver":
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7169812f, 0.7169812f, 0.7169812f);
                break;
            case "Blue":
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7169812f, 0.7169812f, 0.7169812f);
                break;
            case "Red":
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7169812f, 0.7169812f, 0.7169812f);
                break;
            case "Gold":
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7169812f, 0.7169812f, 0.7169812f);
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 100f / 255f, 0);
                break;
        }
    }
}
