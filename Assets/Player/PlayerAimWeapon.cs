using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; // Assuming you're using CodeMonkey utilities for mouse position

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private AudioSource shootingAudioSource; // Reference to the AudioSource

    private Transform aimTransform;
    private float nextFireTime = 0f;
    private Coroutine fireRateCoroutine;
    [SerializeField] private float minFireRate = 0.1f;

    private void Awake()
    {
        // Find the Aim object (the weapon or gun)
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        // Calculate the aim direction based on the player's position and the mouse position
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rotate the weapon (aim object) to face the direction of the mouse
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Shooting logic: fire a bullet if the left mouse button is clicked and the fire rate allows
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();  // Call the shoot function
            nextFireTime = Time.time + fireRate;  // Set the next available fire time
        }
    }

    private void Shoot()
    {
        Vector3 firingRotation = firingPoint.rotation.eulerAngles;
        firingRotation.z -= 88f; 
        Quaternion bulletRotation = Quaternion.Euler(firingRotation);

        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, bulletRotation);

        // Play the shooting sound effect
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }

    public void ModifyFireRate(float multiplier, float duration)
    {
        if (fireRateCoroutine != null)
        {
            StopCoroutine(fireRateCoroutine);
        }
        fireRateCoroutine = StartCoroutine(ModifyFireRateCoroutine(multiplier, duration));
    }

    private IEnumerator ModifyFireRateCoroutine(float multiplier, float duration)
    {
        float originalFireRate = fireRate;
        fireRate = Mathf.Max(fireRate * multiplier, minFireRate);

        float startTime = Time.time;
        Debug.Log($"Fire rate boost started at: {startTime}");

        yield return new WaitForSeconds(duration);

        fireRate = originalFireRate;

        float endTime = Time.time;
        Debug.Log($"Fire rate boost ended at: {endTime}. Fire rate reverted to: {fireRate}.");
        Debug.Log($"Boost duration was: {endTime - startTime} seconds.");
    }
}
