using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_ProxBased : MonoBehaviour
{
    public float slowDown;

    // Start is called before the first frame update
    void Start()
    {
        if (slowDown <= 0)
        {
            slowDown = 3.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();

            //slow the player down via slowdown speed.
            if (character)
                character.speed -= slowDown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();

            //slow the player down via slowdown speed.
            if (character)
                character.speed += slowDown;
        }
    }
}
