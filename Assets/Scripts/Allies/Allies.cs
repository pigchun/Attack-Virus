using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;  // Bullet prefab
    [SerializeField] private Transform firingPoint;    // Bullet firing point
    [SerializeField] private float fireRate = 1f;      // Shooting interval time
    [SerializeField] private float bulletSpeed = 10f;  // Bullet speed
    [SerializeField] private float maxShootingDistance = 5f;  // Maximum shooting distance

    private float nextFireTime = 0f;   // Used to control the shooting interval

    private void Update()
    {
        // Regularly fire bullets
        if (Time.time >= nextFireTime)
        {
            // Check if there are enemies (viruses) present
            Transform closestVirus = FindClosestVirus();
            if (closestVirus != null)
            {
                // Calculate the distance to the closest virus
                float distanceToVirus = Vector3.Distance(transform.position, closestVirus.position);

                // Only shoot if the virus is within the maximum shooting distance
                if (distanceToVirus <= maxShootingDistance)
                {
                    Shoot(closestVirus);  // Call the shoot function if a virus exists within range
                }
            }
            nextFireTime = Time.time + fireRate;  // Set the time for the next shot
        }
    }

    private void Shoot(Transform virus)
    {
        // Instantiate the bullet and set the firing direction
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        // Calculate the firing direction based on the virus's position
        Vector3 shootDirection = (virus.position - firingPoint.position).normalized;

        // Set the bullet's speed
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootDirection * bulletSpeed;

        // Optional: Set the bullet's rotation to point towards the target
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg; // Calculate the angle
        float angleOffset = -90f; // You can adjust this value
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + angleOffset)); // Set the bullet's rotation
    }

    // Get the closest virus
    private Transform FindClosestVirus()
    {
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
        Transform closestVirus = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject virus in viruses)
        {
            float distanceToVirus = Vector3.Distance(transform.position, virus.transform.position);
            if (distanceToVirus < closestDistance)
            {
                closestDistance = distanceToVirus;
                closestVirus = virus.transform;
            }
        }

        return closestVirus;  // Return the closest virus (returns null if none are found)
    }
}



