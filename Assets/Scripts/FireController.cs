using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector
    public Transform firePoint; // Transform representing the point where bullets are spawned

    public float fireRate = 1f; // Adjust the fire rate as needed
    private float nextFireTime = 0f;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Time.time >= nextFireTime&& !gameManager.isGameOver)
        {
            AudioManager.instance.PlayShootSound();
            Shoot();
            
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + 1f / fireRate; // Update the next allowed fire time

        // Instantiate a new bullet at the fire point position and rotation
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Optionally, you can set the new bullet as a child of another GameObject for organization
        // newBullet.transform.parent = transform;
    }
}
