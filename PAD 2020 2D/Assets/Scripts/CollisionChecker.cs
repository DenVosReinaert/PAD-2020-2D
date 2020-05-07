using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

    private ParticleSystem particle;

    private const float _Red = 0f;
    private const float _Green = 100f / 255f;
    private const float _Blue = 0f;

    void Awake()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        string name = gameObject.name;
        if (!name.Equals(ActiveLineChecker.activeLine.name)) {
            return;
        }
        if (name.Equals("LineDrawer")) {
            if (!other.gameObject.name.Equals("WP")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(_Red, _Green, _Blue);
            Explode(Waypoints.waypoints[0].position);
        } else if (name.Equals("Waypoints1T2")) {
            if (!other.gameObject.name.Equals("WP (1)")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(_Red, _Green, _Blue);
            Explode(Waypoints.waypoints[1].position);
        } else if (name.Equals("Waypoints2T3")) {
            if (!other.gameObject.name.Equals("WP (2)")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(_Red, _Green, _Blue);
            Explode(Waypoints.waypoints[2].position);
        } else if (name.Equals("GoalLine")) {
            if (!other.gameObject.name.Equals("Goal")) {
                return;
            }
            ActiveLineChecker.hitTheirGoal.Add(ActiveLineChecker.activeLine);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(_Red, _Green, _Blue);
            Explode(Objectives.objectives[1].position);
        }

        void Explode(Vector2 position)
        {
            particle.transform.position = position;
            particle.Play();
        }
    }
}
