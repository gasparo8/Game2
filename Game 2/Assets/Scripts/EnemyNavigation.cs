using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    // Reference to the stabbing object
    public GameObject stabbingObject;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure the stabbing object is disabled at the start
        if (stabbingObject != null)
        {
            stabbingObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the walker's destination to the player's position
        agent.destination = player.position;
    }

    // Detect collision with the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collided object is the player
        {
            // Enable the stabbing object when collision occurs
            if (stabbingObject != null)
            {
                stabbingObject.SetActive(true); // This will activate the stabbing animation
            }
        }
    }
}
