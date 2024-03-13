using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float distance=2;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (player == null)
        {
            // If the target is not assigned in the Inspector, you can set it to the player's transform
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // If the distance is greater than the stopping distance, set the destination
        if (distanceToPlayer > distance)
        {
            agent.destination = player.position;

            // Check if the enemy is moving
            if (agent.velocity.magnitude > 0.01f) // Adjust the threshold according to your needs
            {
                // If the enemy is moving, set IsRunning to true
                animator.SetBool("IsRunning", true);
            }
            else
            {
                // If the enemy is not moving, set IsRunning to false
                animator.SetBool("IsRunning", false);
            }
        }
        // If the distance is less than or equal to the stopping distance, stop the enemy
        else
        {
            

            // Set IsRunning to false, as the enemy is not moving
            animator.SetBool("canAttack", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("canAttack", true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("canAttack", false);
        }
    }
}
