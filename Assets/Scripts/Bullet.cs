using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 5f; // Time before the bullet is destroyed
    private Rigidbody rb;
    private float force = 1f; // Adjust the force as needed
    public float searchRange = 10f; // The range within which the bullet can detect enemies
    private GameObject targetEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyDelay);
        FindTargetEnemy();
        MoveBullet();
    }

    void FindTargetEnemy()
    {
        // Find the closest enemy within the search range
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRange);

        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = collider.gameObject;
                }
            }
        }
    }

    void MoveBullet()
    {
        // Move the bullet towards the target enemy
        if (targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
            //rb.AddForce(direction * force, ForceMode.Impulse);
            rb.velocity = direction * 10;
        }
        else
        {
            // If no target is found, just move the bullet forward
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        FindTargetEnemy();
        MoveBullet();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Handle enemy hit logic
            Enemy enemy = collision.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Call the Die method on the Enemy component
                enemy.Die();
            }
            //Destroy(collision.collider.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
