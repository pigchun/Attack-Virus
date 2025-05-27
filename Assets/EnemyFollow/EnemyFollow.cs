using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target; // Class-level variable to hold the target's Transform
    public float speed; // Speed of the enemy

    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.FindWithTag("Player"); // Finds the first object with the tag "Player"

        if (targetObject != null)
        {
            target = targetObject.GetComponent<Transform>(); // Gets the Transform component and assigns it to the class-level variable
        }
        else
        {
            Debug.LogWarning("No object with tag 'Player' found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) // Check if the target is assigned
        {
            // Move the enemy towards the target's position
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}

